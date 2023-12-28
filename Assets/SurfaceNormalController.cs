using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceNormalController : MonoBehaviour
{

    public RaycastHit hit;

    [SerializeField] LayerMask layer;
    void Update()
    {

        if (Physics.Raycast(transform.position, -transform.up,out hit,100,layer))
        {

            //Debug.DrawRay(transform.position, -transform.up * hit.distance,Color.blue);
        }

    }
}
