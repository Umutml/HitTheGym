using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeAmplitude = 5f;
    public float shakeFrequency = 2f;

   

    public CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin virtualCamNoise;


    // TRIED SOMETHING BUT NOT WORKING FOR NOW!!
    // TRIED SOMETHING BUT NOT WORKING FOR NOW!!
    // TRIED SOMETHING BUT NOT WORKING FOR NOW!!
    // TRIED SOMETHING BUT NOT WORKING FOR NOW!!
    // TRIED SOMETHING BUT NOT WORKING FOR NOW!!
    void Start()
    {
        if (virtualCam != null)
        {
            virtualCamNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void CamShake()
    {
        if (shakeDuration > 0)
        {
            virtualCamNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCamNoise.m_FrequencyGain = shakeFrequency;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            virtualCamNoise.m_AmplitudeGain = 0f;
            shakeDuration = 0;
        }
    }
}
