using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    [SerializeField] public List<Transform> targets = null;
    [SerializeField] public Collider2D[] obstacles = null;

    [SerializeField] public Transform currentTarget;

    public int GetTargetsCount() => targets == null ? 0 : targets.Count;

    public void SetObstacles(Collider2D[] obstacles)
    {
        this.obstacles = obstacles;
    }

    public void SetTargets(List<Transform> targets)
    {
        this.targets = targets;
    }

    public Transform GetCurrentTarget()
    {
        return this.currentTarget;
    }
}
