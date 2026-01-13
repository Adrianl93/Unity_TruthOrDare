using UnityEngine;
using UnityEngine.UI;

public class PlayerCountSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerSetupController playerSetup;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    private void Start()
    {
        // Inicializa el setup con el valor actual del slider
        OnSliderValueChanged(slider.value);
    }

    public void OnSliderValueChanged(float value)
    {
        
        if (playerSetup == null)
        {
            Debug.LogError("PlayerSetupController no asignado");
            return;
        }

        int playerCount = Mathf.RoundToInt(value);
        playerSetup.SetPlayerCount(playerCount);
    }
}
