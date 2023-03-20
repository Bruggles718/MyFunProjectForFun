using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float lockedInAttackTime;
    public virtual IEnumerator AttackCo(EnemyAI enemy)
    {
        yield return null;
    }

    public float AttackTime()
    {
        return lockedInAttackTime;
    }
}
