using UnityEngine;

public abstract partial class BaseTank : BaseObject
{
    public void OpenShootingMode()
    {
        CloseMovementMode();

        actionMode = ActionModes.SHOOT;
        Action.isActionMode = true;
        SetOverWatch(true);
    }

    public void OpenMovementMode()
    {
        CloseShootingMode();
        Action.isActionMode = true;

        GameAreas.MovementPointArea.SetActive(true);
        actionMode = ActionModes.MOVEMENT;

        if (Action.weight > 0)
        {
            SecondMovementAction();
            return;
        }

        GameAreas.FullTankArea.Activate(transform.position, movementdistance);
        GameAreas.HalfTankArea.Activate(transform.position, movementdistance / 2, MovementAreaColors.HALF);

        GameCamera.CameraController.SetDistance(DistanceWithCamera);
    }

    public void CloseMovementMode()
    {
        GameAreas.FullTankArea.Deactivate();
        GameAreas.HalfTankArea.Deactivate();
    }

    public void CloseShootingMode()
    {
     
        overwatch.SetActive(false);
    }

}
