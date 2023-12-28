using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public partial class CameraController
{

    public CameraController()
    {
        VirtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();

        transposer = VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        screenOrigin = new Vector2(Screen.width / 2, Screen.height / 2);
    }

}
