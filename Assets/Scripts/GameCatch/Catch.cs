using UnityEngine;
using UnityEngine.EventSystems;

public class RayCatch
{
    public static GameObject CatchGameObject
    {
        get
        {
            if (MouseInput.isClickLeft)
            {
                return GamePhysics.RayFromScreen.Hit.collider.gameObject;
            }
            return null;
        }
    }

    public static GameObject CatchObject(string tag)
    {
        if (MouseInput.isClickLeft && CatchGameObject.CompareTag(tag)) return CatchGameObject;

        return null;
    }

    public  static T CatchComponent<T>() where T : MonoBehaviour
    {
        if (MouseInput.isClickLeft) return CatchGameObject.GetComponent<T>();

        return null;
    }

    public static bool CatchUI
    {
        get
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
