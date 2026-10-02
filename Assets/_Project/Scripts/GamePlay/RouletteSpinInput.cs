using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct SpinSensitivityProfile
{
    public float dragMultiplier;
    public float velocityAmplifier;
    public float spinThreshold;
}

public class RouletteSpinInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform wheel;
    [SerializeField] private SpinController spinController;
    [SerializeField] private GameFlowController gameFlow;

    [Header("Inertia")]
    [SerializeField] private float inertiaDamping = 5f;
    [SerializeField] private float maxAngularVelocity = 2000f;

    [Header("Sensitivity Profiles")]
    [SerializeField] private SpinSensitivityProfile windowsProfile;
    [SerializeField] private SpinSensitivityProfile androidProfile;

    // Valores activos (se cargan según plataforma)
    private float dragRotationMultiplier;
    private float velocityAmplifier;
    private float spinVelocityThreshold;

    private PlayerInputActions actions;

    private bool isDragging;
    private float angularVelocity;

    private Vector2 lastPointerPos;
    private float lastDragTime;

    private void Awake()
    {
        actions = new PlayerInputActions();
        ApplyPlatformSensitivity();
    }

    private void OnEnable()
    {
        actions.Gameplay.Enable();
        actions.Gameplay.PointerPress.started += OnPressStarted;
        actions.Gameplay.PointerPress.canceled += OnPressCanceled;
    }

    private void OnDisable()
    {
        actions.Gameplay.PointerPress.started -= OnPressStarted;
        actions.Gameplay.PointerPress.canceled -= OnPressCanceled;
        actions.Gameplay.Disable();
    }

    private void Update()
    {
        if (!wheel)
            return;

        if (isDragging)
        {
            UpdateDrag();
        }
        else if (Mathf.Abs(angularVelocity) > 0.1f)
        {
            ApplyInertia();
        }
    }

    // =========================
    // PLATFORM PROFILES
    // =========================
    private void ApplyPlatformSensitivity()
    {
        SpinSensitivityProfile profile;

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                profile = androidProfile;
                break;

            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
            default:
                profile = windowsProfile;
                break;
        }

        dragRotationMultiplier = profile.dragMultiplier;
        velocityAmplifier = profile.velocityAmplifier;
        spinVelocityThreshold = profile.spinThreshold;
    }

    // =========================
    // DRAG LOGIC
    // =========================
    private void UpdateDrag()
    {
        Vector2 currentPos = actions.Gameplay.PointerPosition.ReadValue<Vector2>();

        if (currentPos == lastPointerPos)
            return;

        Vector2 center = RectTransformUtility.WorldToScreenPoint(
            null,
            wheel.TransformPoint(wheel.rect.center)
        );

        Vector2 from = lastPointerPos - center;
        Vector2 to = currentPos - center;

        if (from.sqrMagnitude < 25f || to.sqrMagnitude < 25f)
            return;

        float angleDelta = Vector2.SignedAngle(from, to);

        if (float.IsNaN(angleDelta) || float.IsInfinity(angleDelta))
            return;

        // Rotación visual inmediata
        wheel.Rotate(0f, 0f, angleDelta * dragRotationMultiplier);

        // ===== VELOCIDAD REAL =====
        float deltaTime = Time.time - lastDragTime;

        if (deltaTime > 0f)
        {
            angularVelocity = (angleDelta / deltaTime) * velocityAmplifier;
            angularVelocity = Mathf.Clamp(
                angularVelocity,
                -maxAngularVelocity,
                maxAngularVelocity
            );
        }

        lastDragTime = Time.time;
        lastPointerPos = currentPos;
    }

    // =========================
    // INERTIA
    // =========================
    private void ApplyInertia()
    {
        wheel.Rotate(0f, 0f, angularVelocity * Time.deltaTime);

        angularVelocity = Mathf.Lerp(
            angularVelocity,
            0f,
            inertiaDamping * Time.deltaTime
        );
    }

    // =========================
    // INPUT EVENTS
    // =========================
    private void OnPressStarted(InputAction.CallbackContext ctx)
    {
        if (gameFlow != null && !gameFlow.CanSpin)
            return;

        isDragging = true;
        angularVelocity = 0f;

        lastPointerPos = actions.Gameplay.PointerPosition.ReadValue<Vector2>();
        lastDragTime = Time.time;
    }

    private void OnPressCanceled(InputAction.CallbackContext ctx)
    {
        if (!isDragging)
            return;

        isDragging = false;

        if (Mathf.Abs(angularVelocity) >= spinVelocityThreshold)
        {
            spinController.SendMessage(
                "HandleSpin",
                SendMessageOptions.DontRequireReceiver
            );
        }

        angularVelocity = 0f;
    }
}
