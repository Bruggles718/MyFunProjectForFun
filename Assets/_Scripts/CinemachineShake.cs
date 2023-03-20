using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private float time;
    private float shakeTill;
    private float startingIntensity;

    private void Awake()
    {
        this.vCam = GetComponent<CinemachineVirtualCamera>();
        this.shakeTill = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetActiveCameraShaker(this);
    }

    // Update is called once per frame
    void Update()
    {
        var perlin = this.vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (Time.time >= shakeTill)
        {
            perlin.m_AmplitudeGain = 0;
            return;
        }
        
        perlin.m_AmplitudeGain = Mathf.Lerp(0, this.startingIntensity, (this.shakeTill - Time.time) / this.time);
    }

    public void ShakeCamera(float intensity, float time)
    {
        var perlin = this.vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        this.startingIntensity = intensity;
        this.time = time;
        perlin.m_AmplitudeGain = intensity;

        this.shakeTill = Time.time + time;
    }
}
