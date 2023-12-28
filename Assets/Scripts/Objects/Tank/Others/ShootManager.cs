using System;
using UnityEngine;
[Serializable] public struct ObuseAttributes
{
    public float prefferedHeight;
    public float Speed;

    [HideInInspector] public float height;
    [HideInInspector] public float time;
    [HideInInspector] public float v0;
    [HideInInspector] public float angle;
    [HideInInspector] public Vector3 target_point;
}

[Serializable] public struct RegularAttributes
{
    public float Speed;
    public float rangeAngle; // set flexibility angle of barrel from inspector
}

public class ShootManager : MonoBehaviour
{
    /******************* ~ATTRIBUTES~ *******************/

    //OverWatch Attributes
    [SerializeField] GameObject overWatch;
    //Tank Attributes
    //[SerializeField] Tank tank; // sonradan degisir.

    //Obuse Attributes
    [SerializeField] ObuseAttributes _obuseAttributes;
    [SerializeField] GameObject _ObuseBulletObject;

    //Regular Attributes
    public RegularAttributes _regularAttributes;
    public GameObject _RegularBulletObject;

    //Transform Attributes
    Transform _barrel, _headOfBarrel;

    //Position Attributes
    Quaternion _targetRot;

    //Control Attributes
    [SerializeField] bool _IsObuseShoot;
    public bool hitOnTarget; //Set From Input Manager
    bool _focusOnTarget, _readyToShoot, _isShooting;

    //Speed Attributes
    [SerializeField] float rotationSpeed; // set Rotation speed of barrel from inspector
    /****************************************************/

    /******************* ~MONOBEHAVIOUR METHODS~ *******************/
    void Start()
    {
        //tank = transform.GetComponent<Tank>();

        setRotationAttributes();
    }

    void Update()
    {
        UpdateHitPoint();
    }

    private void FixedUpdate()
    {
        RotateBarrel();
        Shoot();
    }

    /***************************************************************/

    /******************* ~COSTUMIZE METHODS~ *******************/

    void setRotationAttributes()
    {
        _barrel = transform.GetChild(0).GetChild(0).transform;
        _headOfBarrel = _barrel.GetChild(0);
        _focusOnTarget = false;
        hitOnTarget = false;
    }

    void RotateBarrel()
    {
        if (!_focusOnTarget) return;

        _barrel.rotation = Quaternion.Slerp(_barrel.rotation, _targetRot, rotationSpeed * Time.fixedDeltaTime);

        if (Aproxiemate(_barrel.rotation.eulerAngles.x, _targetRot.eulerAngles.x, 1f))
            _barrel.rotation = _targetRot;
        if (!Aproxiemate(_barrel.rotation.eulerAngles.x, _targetRot.eulerAngles.x, 1f)) return;

        if (!_isShooting) _readyToShoot = true;
        else
        {
            _focusOnTarget = false;
            _readyToShoot = false;
            _isShooting = false;
        }
    }

    void Shoot()
    {
        GameObject new_bullet;

        if (!_readyToShoot || !_focusOnTarget) return;

        if (_IsObuseShoot) {
            new_bullet =  Instantiate(_ObuseBulletObject, _headOfBarrel.position, _barrel.rotation);
            new_bullet.GetComponent<ObuseBullet>()._obuseAttributes = _obuseAttributes;
        } else {
            new_bullet =  Instantiate(_RegularBulletObject, _headOfBarrel.position, _barrel.rotation);
            new_bullet.GetComponent<RegularBullet>()._regularAttributes = _regularAttributes;
        }

        _targetRot = Quaternion.Euler(0, _targetRot.eulerAngles.y, _targetRot.eulerAngles.z);
        _readyToShoot = false;
        _isShooting = true;
    }

    void UpdateHitPoint()
    {
        if (!hitOnTarget) return;

        _focusOnTarget = true;
        hitOnTarget = false;
        _isShooting = false;

        if (_IsObuseShoot)
            CalculateObuseTargetPos();
        else
            CalculateRegularTargetPos();
    }

    void CalculateObuseTargetPos()
    {
        Vector3 pos;
        Vector3 groundPoint;

        float a, b, c;
        float t1, t2;
        float g, h;
        float horizontalDistance;

        pos = GamePhysics.RayFromScreen.Hit.collider.transform.position - _headOfBarrel.position;
        groundPoint = new Vector3(pos.x, 0, pos.z);
        horizontalDistance = groundPoint.magnitude;
        _obuseAttributes.target_point = groundPoint.normalized;

        g = -Physics.gravity.y;
        h = Mathf.Max(pos.y + pos.magnitude / 2, _obuseAttributes.prefferedHeight);

        a = -0.5f * g;
        b = Mathf.Sqrt(2 * g * h);
        c = -pos.y;

        t1 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        t2 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        _obuseAttributes.time = Mathf.Max(t1, t2);
        _obuseAttributes.angle = Mathf.Atan(b * _obuseAttributes.time / horizontalDistance);
        _obuseAttributes.v0 = b / Mathf.Sin(_obuseAttributes.angle);

        _targetRot = Quaternion.Euler(-_obuseAttributes.angle * Mathf.Rad2Deg, _barrel.rotation.eulerAngles.y, _barrel.rotation.eulerAngles.z);
    }

    void CalculateRegularTargetPos()
    {
        Vector3 _hitPosition;
        float angle_diff;
        float height;
        float distance;

        _hitPosition = GamePhysics.RayFromScreen.Hit.collider.transform.position;

        height = _hitPosition.y - _barrel.position.y;
        distance = Vector2.Distance(new Vector2(_hitPosition.x, _hitPosition.z), new Vector2(_barrel.position.x, _barrel.position.z));
        angle_diff = Mathf.Atan2(height, distance) * Mathf.Rad2Deg;

        //if target rotation angle bigger than goal target then set target as limit.
        if (Mathf.Abs(angle_diff) <= _regularAttributes.rangeAngle)
            _targetRot = Quaternion.Euler(-angle_diff, _barrel.rotation.eulerAngles.y, _barrel.rotation.eulerAngles.z);
        else
            _targetRot = Quaternion.Euler(Mathf.Sign(-angle_diff) * _regularAttributes.rangeAngle, _barrel.rotation.eulerAngles.y, _barrel.rotation.eulerAngles.z);
    }

    bool Aproxiemate(float a, float b, float range) => Mathf.Abs(a - b) <= range;
    /**********************************************************/

    /***************OverWatch*****************/

    public void SetOverWatch(bool active)
    {
        overWatch.SetActive(active);
    }

    /****************************************/
}
