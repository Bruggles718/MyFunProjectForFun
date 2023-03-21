using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttack : EnemyAttack
{
    [SerializeField] private GameObject swordSwipePrefab;
    [SerializeField] private Transform handTransform;
    [SerializeField] private float distanceInFrontToSpawnSwipe;
    [SerializeField] private float distanceForward;
    public override IEnumerator AttackCo(EnemyAI enemy)
    {
        enemy.SetMoveVector(Vector3.zero);
        yield return Helpers.WaitForSeconds(0.5f);
        var direction = (enemy.GetAIData().currentTarget.position - transform.position).normalized;
        enemy.SetMoveVector(direction * distanceForward);
        yield return Helpers.WaitForSeconds(1f / 3f);
        float swipeAnimDuration = 1.0f / 6.0f;
        var swipeObject = Instantiate(this.swordSwipePrefab,
            this.handTransform.position - (this.handTransform.right * this.distanceInFrontToSpawnSwipe),
            Quaternion.identity);
        swipeObject.transform.right = -this.handTransform.right;
        Destroy(swipeObject,
                swipeAnimDuration);

        enemy.SetMoveVector(Vector3.zero);
    }
}
