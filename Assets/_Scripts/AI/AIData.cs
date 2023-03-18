using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    [SerializeField] public List<Transform> targets = new List<Transform>();
    [SerializeField] public Collider2D[] obstacles = { };

    [SerializeField] public Transform currentTarget;

    public int GetTargetsCount()
    {
        return targets.Count;
    }

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
