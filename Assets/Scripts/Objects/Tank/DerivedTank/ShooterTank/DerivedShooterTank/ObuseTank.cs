using UnityEngine;

public abstract class ObuseTank : ShooterTank
{
    [SerializeField] ObuseAttributes obuseAttributes;

    protected  override void CalculateTargetPos()
    {
        Vector3 pos;
        Vector3 groundPoint;

        float a, b, c;
        float t1, t2;
        float g, h;
        float horizontalDistance;

        pos = GamePhysics.RayFromScreen.Hit.collider.transform.position - headOfBarrel.position;
        groundPoint = new Vector3(pos.x, 0, pos.z);
        horizontalDistance = groundPoint.magnitude;
        obuseAttributes.target_point = groundPoint.normalized;

        g = -Physics.gravity.y;
        h = Mathf.Max(pos.y + pos.magnitude / 2, obuseAttributes.prefferedHeight);

        a = -0.5f * g;
        b = Mathf.Sqrt(2 * g * h);
        c = -pos.y;

        t1 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        t2 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        obuseAttributes.time = Mathf.Max(t1, t2);
        obuseAttributes.angle = Mathf.Atan(b * obuseAttributes.time / horizontalDistance);
        obuseAttributes.v0 = b / Mathf.Sin(obuseAttributes.angle);

        targetRot = Quaternion.Euler(-obuseAttributes.angle * Mathf.Rad2Deg, barrel.rotation.eulerAngles.y, barrel.rotation.eulerAngles.z);
    }

    protected override void Shoot()
    {
        GameObject new_bullet;

        if (!readyToShoot || !focusOnTarget) return;

        new_bullet =  Instantiate(Bullet, headOfBarrel.position, barrel.rotation);
        new_bullet.GetComponent<ObuseBullet>()._obuseAttributes = obuseAttributes;

        targetRot = Quaternion.Euler(0, targetRot.eulerAngles.y, targetRot.eulerAngles.z);
        readyToShoot = false;
        isShooting = true;
        team.SetDone(this); //simdilik burada
    }
}
