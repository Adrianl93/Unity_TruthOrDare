using UnityEngine;
using UnityEngine.UI;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event Action OnSpin;

    [SerializeField] private Button spinButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Debug.Log("InputManager inicializado correctamente (scene-bound)");
    }

    private void OnEnable()
    {
        if (spinButton == null)
        {
            Debug.LogError("InputManager: SpinButton no asignado en inspector");
            return;
        }

        spinButton.onClick.RemoveAllListeners();
        spinButton.onClick.AddListener(SpinPressed);
    }

    private void OnDisable()
    {
        if (spinButton != null)
            spinButton.onClick.RemoveListener(SpinPressed);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void SpinPressed()
    {
        Debug.Log("InputManager: SpinPressed (UI Button)");
        OnSpin?.Invoke();
    }
}
