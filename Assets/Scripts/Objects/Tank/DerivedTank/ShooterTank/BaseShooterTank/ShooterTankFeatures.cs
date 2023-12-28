
using UnityEngine;

public abstract partial class ShooterTank : BaseTank
{
    protected abstract void CalculateTargetPos();
    protected abstract void Shoot();

    protected GameObject catchedObject;

    void UpdateHitPoint()
    {
        

        if (actionMode != ActionModes.SHOOT) return;

        if (RayCatch.CatchObject("Enemy") == null) return;

        catchedObject = RayCatch.CatchObject("Enemy");

        Action.SetWeight(1);

        focusOnTarget = true;
        isShooting = false;

        SetOverWatch(false);

        CalculateTargetPos();
    }

    void HorizontalRotateTurret(Vector3 TargetPosition)
    {
        Quaternion targetRot;

        targetRot = Quaternion.LookRotation(TargetPosition - Turret.transform.position);
        Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    void VerticalRotateBarrel()
    {
        barrel.rotation = Quaternion.Slerp(barrel.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        if (Aproxiemate(barrel.rotation.eulerAngles.x, targetRot.eulerAngles.x, 1f)) barrel.rotation = targetRot;
        else return;
    }

    void CheckDirectedToTarget(Vector3 target)
    {
        if (Vector3.Angle(Turret.transform.forward, target - Turret.transform.position) > 1) return;

        if (!isShooting) readyToShoot = true;
        else
        {
            focusOnTarget = false;
            readyToShoot = false;
            isShooting = false;

        }
    }

    void Rotate()
    {
        if (focusOnTarget && catchedObject != null)
        {
            VerticalRotateBarrel();
            HorizontalRotateTurret(catchedObject.transform.position);
            CheckDirectedToTarget(catchedObject.transform.position);
            return;
        }

        if (actionMode == ActionModes.SHOOT) HorizontalRotateTurret(GamePoints.ShootPoint.Point);
    }

    bool Aproxiemate(float a, float b, float range) => Mathf.Abs(a - b) <= range;
}

