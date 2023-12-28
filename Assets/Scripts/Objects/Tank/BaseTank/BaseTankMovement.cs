using UnityEngine;

public abstract partial class BaseTank : BaseObject
{
    void SetMovementVariables()
    {

    }

    virtual protected void Movement()
    {
        TriggerMovement();

        if (!freezeBaseLayer) return;
        agent.destination = GamePoints.MovementPoint.Point;
    }

}