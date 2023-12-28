using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public abstract partial class ShooterTank : BaseTank
{
    [SerializeField] protected GameObject Bullet;
    protected Transform barrel, headOfBarrel;
    protected Quaternion targetRot;
    protected bool focusOnTarget, readyToShoot, isShooting;
    protected float rotationSpeed;
    [HideInInspector] public bool hitOnTarget; //Set From Input Manager

    protected override void DerivedAwake()
    {
        barrel = Turret.transform.GetChild(0).transform;
        headOfBarrel = barrel.GetChild(0);
        rotationSpeed = .5f;
    }

    protected override void DerivedUpdate()
    {
        if (freezeDerivedLayer) return;

        UpdateHitPoint();
        Rotate();
        Shoot();
    }
}
