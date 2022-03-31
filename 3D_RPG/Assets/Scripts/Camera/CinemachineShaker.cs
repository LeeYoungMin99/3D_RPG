using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShaker
{
    public static readonly CinemachineShaker Instance = new CinemachineShaker();

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private bool _hasVirtualCamera = false;

    public IEnumerator ShakeCamera(float amplitueGain, float shakeTime)
    {
        if (false == _hasVirtualCamera)
        {
            _hasVirtualCamera = true;

            _cinemachineBasicMultiChannelPerlin = GameObject.Find("Field").
                transform.Find("Follow Camera").GetComponent<CinemachineVirtualCamera>().
                GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        }

        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitueGain;

        yield return new WaitForSeconds(shakeTime);

        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
