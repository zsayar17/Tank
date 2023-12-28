using UnityEngine;

public struct GamePhysics
{
    public static ThrowRayFromScreen RayFromScreen;
    public void Awake()
    {
        RayFromScreen = new ThrowRayFromScreen();
    }

    public void Update()
    {
        RayFromScreen.Update();
    }
}


public class ThrowRayFromScreen
{
    [HideInInspector] public RaycastHit Hit;   // carptigi yeri doner

    public void Update()
    {
        ThrowRay();
    }
    private void ThrowRay()
    {
        Vector3 mouse_pos;
        Ray ray;

        mouse_pos = Input.mousePosition;

        ray = Camera.main.ScreenPointToRay(mouse_pos);
        Physics.Raycast(ray, out Hit);
    }
}




