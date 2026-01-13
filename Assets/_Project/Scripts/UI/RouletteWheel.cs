using UnityEngine;

public class RouletteWheel : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private int totalSectors = 8;

    // Offset para alinear el centro del sector 0 con el puntero superior
    // 90° = pasar de eje X (derecha) a eje Y (arriba)
    private const float POINTER_OFFSET = 90f;

    public WheelResult GetResult()
    {
        if (wheel == null)
        {
            Debug.LogError("RouletteWheel: wheel NO asignado");
            return new WheelResult(WheelType.Truth, Difficulty.VeryEasy);
        }

        // 1. Tomamos rotación Z del disco
        float rawAngle = wheel.eulerAngles.z;

        // 2. Como gira el disco, invertimos el sentido
        float invertedAngle = 360f - rawAngle;

        // 3. Ajustamos al puntero (12 en punto)
        float pointerAngle = (invertedAngle + POINTER_OFFSET) % 360f;

        // 4. Calculamos sector
        float sectorSize = 360f / totalSectors;
        int sectorIndex = Mathf.FloorToInt(pointerAngle / sectorSize);

        Debug.Log($"Ruleta  Ángulo:{pointerAngle:F1} | Sector:{sectorIndex}");

        return SectorToResult(sectorIndex);
    }

    private WheelResult SectorToResult(int index)
    {
        return index switch
        {
            0 => new WheelResult(WheelType.Truth, Difficulty.VeryEasy),
            1 => new WheelResult(WheelType.Dare, Difficulty.Hard),
            2 => new WheelResult(WheelType.Truth, Difficulty.Medium),
            3 => new WheelResult(WheelType.Dare, Difficulty.VeryEasy),
            4 => new WheelResult(WheelType.Truth, Difficulty.Easy),
            5 => new WheelResult(WheelType.Dare, Difficulty.Medium),
            6 => new WheelResult(WheelType.Truth, Difficulty.Hard),
            7 => new WheelResult(WheelType.Dare, Difficulty.Easy),
            _ => new WheelResult(WheelType.Truth, Difficulty.VeryEasy),
        };
    }
}
