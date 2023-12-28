using UnityEngine;

public class MouseInput
{
    public static bool isClickLeft
    {
        get => Input.GetMouseButtonDown(0);
    }
}
