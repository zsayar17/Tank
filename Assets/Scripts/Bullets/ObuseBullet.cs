using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObuseBullet : MonoBehaviour
{

    [HideInInspector] public ObuseAttributes _obuseAttributes;
    LineRenderer line;
    Vector3 beginPoint;
    float passed_time;

    void Start()
    {
        beginPoint = transform.position;
        line = transform.GetChild(0).GetComponent<LineRenderer>();

        _obuseAttributes.Speed = 1;
    }

    void Update()
    {
        CheckVisible();
        setPath();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    void CheckVisible()
    {
        Vector3 viewportPoint;

        viewportPoint = Camera.main.WorldToViewportPoint(transform.position);

        if (!(viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0)) Destroy(gameObject);
    }

    void Move()
    {
        float x, y;

        x = _obuseAttributes.v0 * Mathf.Cos(_obuseAttributes.angle) * passed_time;
        y = _obuseAttributes.v0 * Mathf.Sin(_obuseAttributes.angle) * passed_time - (0.5f * -Physics.gravity.y * Mathf.Pow(passed_time, 2));

        transform.position = beginPoint + _obuseAttributes.target_point * x + Vector3.up * y;
        passed_time += Time.deltaTime * _obuseAttributes.Speed;
    }

    void setPath()
    {
        float step = 0.1f;
        int count = 0;
        float x, y;

        line.positionCount = (int)(_obuseAttributes.time / step) + 2;

        for (float i = 0; i < _obuseAttributes.time; i += step)
        {
            x = _obuseAttributes.v0 * Mathf.Cos(_obuseAttributes.angle) * i;
            y = _obuseAttributes.v0 * Mathf.Sin(_obuseAttributes.angle) * i - (0.5f * -Physics.gravity.y * Mathf.Pow(i, 2));

            line.SetPosition(count, beginPoint + _obuseAttributes.target_point * x + Vector3.up * y);
            count++;
        }

        x = _obuseAttributes.v0 * Mathf.Cos(_obuseAttributes.angle) * _obuseAttributes.time;
        y = _obuseAttributes.v0 * Mathf.Sin(_obuseAttributes.angle) * _obuseAttributes.time - (0.5f * -Physics.gravity.y * Mathf.Pow(_obuseAttributes.time, 2));
        line.SetPosition(count, beginPoint + _obuseAttributes.target_point * x + Vector3.up * y);
    }

    private void OnDestroy()
    {
        Action.FinishAction = true;        
    }
}
