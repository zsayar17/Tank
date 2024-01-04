using UnityEngine.AI;
using UnityEngine;

public abstract partial class BaseTank : BaseObject
{
    [HideInInspector] public int health;
    [HideInInspector] public int hitrate;
    [HideInInspector] public int shootpower;
    [HideInInspector] public int movementdistance;

    [HideInInspector] public ActionModes actionMode = ActionModes.NONE;
    NavMeshAgent agent;
    [SerializeField] GameObject overwatch;
    [SerializeField] protected GameObject Turret;
    protected bool freezeBaseLayer, freezeDerivedLayer;
    protected abstract void SetTankFeautures();

    void SetBaseVariables()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        Turret = transform.GetChild(0).gameObject;
        objectType = ObjectType.Tank;

        SetOverWatch(false);
    }

    public float DistanceWithCamera
    {
        get => movementdistance * 2.14f;
    }

    public void SetOverWatch(bool active)
    {
        overwatch.SetActive(active);
    }

    public void CloseMode()
    {
        Action.isActionMode = false;
        actionMode = ActionModes.NONE;
    }
}
