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
        freezeBaseLayer = true; //Layer freezleme islemleri sadece en ust katmanda yapilir.
        CloseMovementMode();
    }

    void FinishMovement()
    {
        if (freezeBaseLayer && !GamePoints.MovementPoint.IsDistancePointBetweenTank)
        {
            freezeBaseLayer = false; //Layer freezleme islemleri sadece en ust katmanda yapilir.
            Action.FinishAction = true;

            team.SetDone(this);
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
