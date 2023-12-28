using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public struct GameCamera
{

    public static CameraController CameraController;
    public void Awake()
    {
        CameraController = new CameraController();
    }

    public void Update()
    {
        CameraController.Update();
    }

}



public partial class CameraController
{
    private CinemachineFramingTransposer transposer;

    public static CinemachineVirtualCamera VirtualCamera;

    private float scrollSpeed = 1;

    private Vector2 screenOrigin;

    public void Update()
    {
        MouseScroll();
        Movement();
    }

    void MouseScroll()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        if (scrollValue > 0) CameraDistance -= scrollSpeed;
        else if (scrollValue < 0) CameraDistance += scrollSpeed;
    }


    public void Focus(Transform transform)
    {
        VirtualCamera.Follow = transform;
        VirtualCamera.LookAt = transform;

    }
    
    public void SetDistance(float distance)
    {
        CameraDistance = distance;
					
    }
	
    public float CameraDistance
    {
        get => Mathf.Clamp(transposer.m_CameraDistance,10,120);

        set
        {
            transposer.m_CameraDistance = value;

        }

    }
}
