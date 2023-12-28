using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegToPositive
{

    public static float Degrees(float degrees)
    {
        if (degrees >= 0) return degrees;

        return 360 + degrees;
    }

}
