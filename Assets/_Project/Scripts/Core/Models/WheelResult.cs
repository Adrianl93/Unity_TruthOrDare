using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WheelResult
{
    public WheelType Type;
    public Difficulty Difficulty;

    public WheelResult(WheelType type, Difficulty difficulty)
    {
        Type = type;
        Difficulty = difficulty;
    }
}

