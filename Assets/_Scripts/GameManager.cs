using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private UIButtonPrompt buttonPrompt;
    private CinemachineShake cameraShaker;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButtonPromptActive(bool value, string text, string buttonText)
    {
        this.buttonPrompt.SetActive(value, text, buttonText);
    }

    public void FreezeTime(float duration)
    {
        this.StartCoroutine(this.FreezeTimeCo(duration));
    }

    private IEnumerator FreezeTimeCo(float duration)
    {
        float initialScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return Helpers.WaitForSecondsRealtime(duration);
        Time.timeScale = initialScale;
    }

    public void SetActiveCameraShaker(CinemachineShake cameraShaker)
    {
        this.cameraShaker = cameraShaker;
    }
    
    public void ShakeCamera(float intensity, float time)
    {
        if (this.cameraShaker == null) return;
        this.cameraShaker.ShakeCamera(intensity, time);
    }
}
