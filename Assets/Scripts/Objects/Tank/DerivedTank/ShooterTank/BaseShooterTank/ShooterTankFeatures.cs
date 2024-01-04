
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public abstract partial class ShooterTank : BaseTank
{
    protected abstract void CalculateTargetPos();
    protected abstract void Shoot();

    protected GameObject catchedObject;

    void UpdateHitPoint()
    {
        GameObject hitObject;

        if (actionMode != ActionModes.SHOOT) return;

        hitObject = RayCatch.CatchGameObject;
        if (!team.IsEnemy(hitObject)) return;

        catchedObject = hitObject;
        focusOnTarget = true;
        isShooting = false;

        SetOverWatch(false);
        CalculateTargetPos();// fix here
        Action.SetWeight(1);
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

        if (!isShooting)
        {
            //CalculateTargetPos(); // fix here
            readyToShoot = true;
        }
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

