using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : MonoBehaviour
{

    [HideInInspector] public RegularAttributes _regularAttributes;

    private void FixedUpdate()
    {
        CheckVisible();
        Move();
    }

    void CheckVisible()
    {
        Vector3 viewportPoint;

        viewportPoint = Camera.main.WorldToViewportPoint(transform.position);

        if (!(viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1)) Destroy(gameObject);
    }

    void Move()
    {
        transform.localPosition += transform.forward * Time.fixedDeltaTime * _regularAttributes.Speed;
    }


    private void OnDestroy()
    {
        Action.FinishAction = true;
    }
}
