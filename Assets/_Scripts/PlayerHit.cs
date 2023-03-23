using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string hitboxTag;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float slowDownTimeScale = 0.25f;
    [SerializeField] private float slowDownTimeRealtime = 0.25f;
    [SerializeField, Tooltip("Does not include initial white flash")] 
    private int numberOfFlashes = 4;
    [SerializeField, Tooltip("Does not include initial white flash")] 
    private float invincibilityTimeSeconds = 1f;
    [SerializeField] private float screenShakeIntensity = 4f;
    [SerializeField] private float screenShakeTime = 0.25f;
    private Color originalColor;
    private Material originalMaterial;
    private float lockedTill;

    private void Start()
    {
        //this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.originalMaterial = new Material(this.spriteRenderer.material);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time < lockedTill) return;
        if (collision.CompareTag(hitboxTag))
        {
            this.StartCoroutine(this.SlowDownTimeCo());
            this.StartCoroutine(HitFlashCo());
            GameManager.Instance.ShakeCamera(this.screenShakeIntensity, this.screenShakeTime);
        }
    }

    private IEnumerator HitFlashCo()
    {
        this.spriteRenderer.material = this.hitMaterial;
        this.lockedTill = Time.time + this.invincibilityTimeSeconds + (slowDownTimeRealtime * slowDownTimeScale);
        yield return Helpers.WaitForSecondsRealtime(this.slowDownTimeRealtime);
        this.spriteRenderer.material = this.originalMaterial;
        this.originalColor = this.spriteRenderer.color;
        Color transparent = new Color(0, 0, 0, 0);
        float timeBetweenChanges = invincibilityTimeSeconds / (numberOfFlashes * 2f);
        for (int i = 0; i < numberOfFlashes; i += 1)
        {
            this.spriteRenderer.color = transparent;
            yield return Helpers.WaitForSeconds(timeBetweenChanges);
            this.spriteRenderer.color = originalColor;
            yield return Helpers.WaitForSeconds(timeBetweenChanges);
        }
    }

    private IEnumerator SlowDownTimeCo()
    {
        float originalScale = Time.timeScale;
        Time.timeScale = this.slowDownTimeScale;
        yield return Helpers.WaitForSecondsRealtime(this.slowDownTimeRealtime);
        Time.timeScale = originalScale;
    }
}
