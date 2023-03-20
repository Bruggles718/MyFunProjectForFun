using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float invincibilityTimeSeconds = 0.25f;
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;
    private float lockedTill;

    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.originalMaterial = new Material(this.spriteRenderer.material);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time < lockedTill) return;
        if (collision.CompareTag("PlayerHitbox"))
        {
            this.StartCoroutine(this.HitCo());
        }
    }

    private IEnumerator HitCo()
    {
        this.spriteRenderer.material = this.hitMaterial;
        this.lockedTill = Time.time + this.invincibilityTimeSeconds;
        GameManager.Instance.ShakeCamera(4, this.invincibilityTimeSeconds);
        yield return Helpers.WaitForSeconds(this.invincibilityTimeSeconds);
        this.spriteRenderer.material = this.originalMaterial;
    }
}
