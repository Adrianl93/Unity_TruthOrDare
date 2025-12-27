using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions input;

    // Eventos públicos (no referencias directas)
    public event Action OnSpin;
    public event Action OnConfirm;
    public event Action OnCancel;

    private void Awake()
    {
        // Singleton robusto
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        if (input == null)
            input = new PlayerInputActions();

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableGameplayInput();
    }

    private void OnDestroy()
    {
        DisableGameplayInput();
    }

    // -----------------------------
    // INPUT MAP CONTROL
    // -----------------------------

    public void EnableGameplayInput()
    {
        input.Gameplay.Enable();

        input.Gameplay.Spin.performed += OnSpinPerformed;
        input.Gameplay.Confirm.performed += OnConfirmPerformed;
        input.Gameplay.Cancel.performed += OnCancelPerformed;
    }

    public void DisableGameplayInput()
    {
        if (input == null) return;

        input.Gameplay.Spin.performed -= OnSpinPerformed;
        input.Gameplay.Confirm.performed -= OnConfirmPerformed;
        input.Gameplay.Cancel.performed -= OnCancelPerformed;

        input.Gameplay.Disable();
    }

    // -----------------------------
    // CALLBACKS INTERNOS
    // -----------------------------

    private void OnSpinPerformed(InputAction.CallbackContext context)
    {
        OnSpin?.Invoke();
    }

    private void OnConfirmPerformed(InputAction.CallbackContext context)
    {
        OnConfirm?.Invoke();
    }

    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        OnCancel?.Invoke();
    }
}
