using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StrafeBehaviour : SteeringBehaviour
{
    [SerializeField] private TargetDetector targetDetector;

    [SerializeField] private float strafeDistance = 4f;

    [SerializeField] private float strafeDirection = 1;

    [SerializeField]
    private float targetRechedThreshold = 0.5f;

    [SerializeField]
    private bool showGizmo = true;

    bool reachedLastTarget = true;

    //gizmo parameters
    private Vector2 targetPositionCached;
    private float[] interestsTemp;

    private void Start()
    {
        //strafeDirection = Helpers.RandomChoice(1, -1);
    }

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        //if we don't have a target stop seeking
        //else set a new target
        if (reachedLastTarget)
        {
            if (aiData.targets == null || aiData.targets.Count <= 0)
            {
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                aiData.currentTarget = aiData.targets.OrderBy
                    (target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }

        }

        //cache the last position only if we still see the target (if the targets collection is not empty)
        if (!(aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget)))
            return (danger, interest);
        else
            targetPositionCached = aiData.currentTarget.position;

        //First check if we have reached the target
        if (Vector2.Distance(transform.position, targetPositionCached) < targetRechedThreshold)
        {
            reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        //If we havent yet reached the target do the main logic of finding the interest directions
        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);

        float checkDistance = 1f;

        for (int i = 0; i < interest.Length; i += 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Directions.eightDirections[i], checkDistance, LayerMask.GetMask("Obstacles"));

            var visionCheckList = this.targetDetector.DetectHelper(this.transform.position.Vec2() + Directions.eightDirections[i] * checkDistance);

            if (hit)
            {
                this.targetDetector.DetectHelper(hit.point);
            }

            if (visionCheckList == null || visionCheckList.Count <= 0)
            {
                return (danger, interest);
            }
        }

        
        for (int i = 0; i < interest.Length; i++)
        {

            float result = 0;

            result = Vector2.Dot(directionToTarget.normalized.Vec3().RotateAroundZ(this.strafeDirection * 90), Directions.eightDirections[i]) * (this.strafeDistance / Vector3.Distance(targetPositionCached, this.transform.position));

            if (Vector3.Distance(targetPositionCached, this.transform.position) < this.strafeDistance)
            {
                var dangerValue = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]) * (Vector3.Distance(targetPositionCached - (directionToTarget.normalized * this.strafeDistance), transform.position));
                if (dangerValue > danger[i])
                {
                    danger[i] = dangerValue;
                }
                //result -= (Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]));
                result -= dangerValue;
            }

            //accept only directions at the less than 90 degrees to the target direction
            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }

            }
        }
        interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {

        if (showGizmo == false)
            return;
        Gizmos.DrawSphere(targetPositionCached, 0.2f);

        if (Application.isPlaying && interestsTemp != null)
        {
            if (interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i] * 2);
                }
                if (reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositionCached, 0.1f);
                }
            }
        }
    }
}