using UnityEngine;

public enum ObjectType
{
    Tank
}

public class BaseObject : MonoBehaviour
{
    [HideInInspector] public bool isSelected;
    [HideInInspector] public ObjectType objectType;
}