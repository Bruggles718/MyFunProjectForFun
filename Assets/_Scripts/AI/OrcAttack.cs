using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttack : EnemyAttack
{
    [SerializeField] private float distanceForward;
    public override IEnumerator AttackCo(EnemyAI enemy)
    {
        enemy.SetMoveVector(Vector3.zero);
        yield return Helpers.WaitForSeconds(0.5f);
        enemy.SetMoveVector((enemy.GetAIData().currentTarget.position - transform.position).normalized * distanceForward);
        yield return Helpers.WaitForSeconds(1f / 3f);
        enemy.SetMoveVector(Vector3.zero);
    }
}
