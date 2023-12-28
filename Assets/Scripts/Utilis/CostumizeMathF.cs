using System.Collections;

namespace Utilis
{
    static class CostumMath
    {
        /*
        * That function takes 2 angle and return degree of difference
        */
        public static float angleDiff(float v1, float v2)
        {
            float angle_diff;

            angle_diff = v1 - v2;
            if (angle_diff < 0) angle_diff += 360;
            return angle_diff;
        }
    }
}
   
