using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class BaseTank : BaseObject
{
    public void TriggerMovement()
    {
        if (actionMode == ActionModes.MOVEMENT)
        {
            GameAreas.MovementPointArea.Update();
            if (GameAreas.FullTankArea.IsClickOnField(GameAreas.FullTankArea.RADIUS)) AreaControl();
            return;
        }
        FinishMovement();
    }

    void AreaControl()
    {
        if (GamePoints.MovementPoint.CheckMovementArea(GameAreas.FullTankArea.RADIUS / 2)) Action.SetWeight(1);
        else Action.SetWeight(0.5f);

        freezeBaseLayer = true;
        CloseMovementMode();
    }

    void FinishMovement()
    {
        if (freezeBaseLayer && !GamePoints.MovementPoint.IsDistancePointBetweenTank)
        {
            freezeBaseLayer = false;
            Action.FinishAction = true;
        }
    }

    void SecondMovementAction()
    {
        BaseTank tank;

        tank = GameSelect.SelectedTanks.currentTank;
        GameAreas.FullTankArea.Activate(tank.transform.position, tank.movementdistance / 2);
        GameCamera.CameraController.SetDistance(DistanceWithCamera);
    }

}