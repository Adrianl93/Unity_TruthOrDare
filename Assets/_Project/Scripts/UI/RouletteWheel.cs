using System.Collections;
using UnityEngine;

public class RouletteWheel : MonoBehaviour
{
    [Header("Spin Settings")]
    [Tooltip("Cantidad mínima de vueltas completas")]
    [SerializeField] private int minSpins = 2;

    [Tooltip("Duración del giro en segundos")]
    [SerializeField] private float spinDuration = 2f;

    private bool isSpinning = false;

    public void Spin()
    {
        if (isSpinning) return;

        float minRotation = minSpins * 360f;
        float extraRotation = Random.Range(0f, 360f);
        float targetRotation = minRotation + extraRotation;

        StartCoroutine(SpinCoroutine(targetRotation));
    }

    private IEnumerator SpinCoroutine(float rotationAmount)
    {
        isSpinning = true;

        float elapsed = 0f;
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + rotationAmount;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / spinDuration;

            // Ease Out Cubic
            t = 1f - Mathf.Pow(1f - t, 3);

            float zRotation = Mathf.Lerp(startRotation, endRotation, t);
            transform.eulerAngles = new Vector3(0f, 0f, zRotation);

            yield return null;
        }

        transform.eulerAngles = new Vector3(0f, 0f, endRotation);
        isSpinning = false;
    }


    [SerializeField] private int totalSectors = 8;

    public WheelResult GetResult()
    {
        float zRotation = transform.eulerAngles.z;
        float normalizedAngle = zRotation % 360f;

        float sectorSize = 360f / totalSectors;
        int sectorIndex = Mathf.FloorToInt(normalizedAngle / sectorSize);

        return SectorToResult(sectorIndex);
    }

    private WheelResult SectorToResult(int index)
    {
        switch (index)
        {
            case 0: return new WheelResult(WheelType.Truth, Difficulty.VeryEasy);
            case 1: return new WheelResult(WheelType.Dare, Difficulty.VeryEasy);
            case 2: return new WheelResult(WheelType.Truth, Difficulty.Easy);
            case 3: return new WheelResult(WheelType.Dare, Difficulty.Easy);
            case 4: return new WheelResult(WheelType.Truth, Difficulty.Medium);
            case 5: return new WheelResult(WheelType.Dare, Difficulty.Medium);
            case 6: return new WheelResult(WheelType.Truth, Difficulty.Hard);
            case 7: return new WheelResult(WheelType.Dare, Difficulty.Hard);
            
            default:
                return new WheelResult(WheelType.Truth, Difficulty.VeryEasy);
        }
    }


}


