using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraRoot : MonoBehaviour
{
    private GameObject _followCamera;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CameraRotator _cameraRotator;

    private void Awake()
    {
        _followCamera = GameObject.Find("Follow Camera");
        _cinemachineVirtualCamera = _followCamera.GetComponent<CinemachineVirtualCamera>();
        _cameraRotator = _followCamera.GetComponent<CameraRotator>();
    }

    private void OnEnable()
    {
        _cameraRotator.CinemachineCameraTarget = transform;
        _cinemachineVirtualCamera.Follow = transform;
    }
}
