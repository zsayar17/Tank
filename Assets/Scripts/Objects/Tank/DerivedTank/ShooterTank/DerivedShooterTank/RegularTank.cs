using UnityEngine;

public abstract class RegularTank : ShooterTank
{
    [SerializeField] RegularAttributes regularAttributes;

    protected override void CalculateTargetPos()
    {
        Vector3 _hitPosition;
        float angle_diff;
        float height;
        float distance;

        _hitPosition = GamePhysics.RayFromScreen.Hit.collider.transform.position;

        height = _hitPosition.y - barrel.position.y;
        distance = Vector2.Distance(new Vector2(_hitPosition.x, _hitPosition.z), new Vector2(barrel.position.x, barrel.position.z));
        angle_diff = Mathf.Atan2(height, distance) * Mathf.Rad2Deg;

        //if target rotation angle bigger than goal target then set target as limit.
        if (Mathf.Abs(angle_diff) <= regularAttributes.rangeAngle)
            targetRot = Quaternion.Euler(-angle_diff, barrel.rotation.eulerAngles.y, barrel.rotation.eulerAngles.z);
        else
            targetRot = Quaternion.Euler(Mathf.Sign(-angle_diff) * regularAttributes.rangeAngle, barrel.rotation.eulerAngles.y, barrel.rotation.eulerAngles.z);
    }

    protected override void Shoot()
    {
        GameObject new_bullet;

        if (!readyToShoot || !focusOnTarget) return;

        new_bullet = Instantiate(Bullet, headOfBarrel.position, barrel.rotation);
        new_bullet.GetComponent<RegularBullet>()._regularAttributes = regularAttributes;

        targetRot = Quaternion.Euler(0, targetRot.eulerAngles.y, targetRot.eulerAngles.z);
        readyToShoot = false;
        isShooting = true;
        team.SetDone(this); //simdilik burada
    }
}
