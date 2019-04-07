using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LaneManager
{
    private static float[] lanesXCoords = { -3, -1, 1, 3 };

    public static readonly int minLane = 0;
    public static readonly int maxLane = 3;

    public static float GetX(int lane) {
        return lanesXCoords[lane];
    }
}
