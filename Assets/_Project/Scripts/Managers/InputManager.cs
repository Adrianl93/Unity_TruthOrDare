public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputActions input;

    public event Action OnSpin;
    public event Action OnConfirm;
    public event Action OnCancel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Spin.performed += ctx => OnSpin?.Invoke();
        input.Gameplay.Confirm.performed += ctx => OnConfirm?.Invoke();
        input.Gameplay.Cancel.performed += ctx => OnCancel?.Invoke();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
