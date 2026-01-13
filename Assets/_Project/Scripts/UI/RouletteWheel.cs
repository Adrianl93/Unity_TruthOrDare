using UnityEngine;

public class RouletteWheel : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private int totalSectors = 8;

    // 90° porque el puntero está arriba (12 en punto)
    [SerializeField] private float pointerOffset = 45f;


    public WheelResult GetResult()
    {
        if (wheel == null)
        {
            Debug.LogError("RouletteWheel: wheel NO asignado");
            return new WheelResult(WheelType.Truth, Difficulty.VeryEasy);
        }

        float sectorSize = 360f / totalSectors;

        // 1. Rotación actual del disco
        float rawAngle = wheel.eulerAngles.z;

        // 2. Invertimos porque gira el disco, no el puntero
        float invertedAngle = 360f - rawAngle;

        // 3. Alineamos con el puntero superior
        float pointerAngle = invertedAngle + pointerOffset;

        // 4. Compensamos para que el puntero caiga en el CENTRO del sector
        pointerAngle += sectorSize / 2f;

        // 5. Normalizamos
        pointerAngle %= 360f;

        // 6. Calculamos sector
        int sectorIndex = Mathf.FloorToInt(pointerAngle / sectorSize);

        Debug.Log($"Ruleta | Ángulo:{pointerAngle:F1} | Sector:{sectorIndex}");

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
