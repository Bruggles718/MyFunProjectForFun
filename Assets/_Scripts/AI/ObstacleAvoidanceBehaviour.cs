using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    [SerializeField] private TargetDetector targetDetector;

    [SerializeField]
    private float radius = 2f, agentColliderSize = 0.6f;

    [SerializeField]
    private bool showGizmo = true;

    //gizmo parameters
    float[] dangersResultTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        foreach (Collider2D obstacleCollider in aiData.obstacles)
        {
            Vector2 directionToObstacle
                = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            //calculate weight based on the distance Enemy<--->Obstacle
            float weight
                = distanceToObstacle <= agentColliderSize
                ? 1
                : (radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            float checkDistance = 1f;

            bool visionCheck = false;

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
                    visionCheck = true;
                }
            }

            //Add obstacle parameters to the danger array
            for (int i = 0; i < Directions.eightDirections.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);
                if (!visionCheck && obstacleCollider.CompareTag("Enemy"))
                {
                    result = Vector2.Dot(directionToObstacleNormalized.Vec3().RotateAroundZ(45), Directions.eightDirections[i]);
                }

                float valueToPutIn = result * weight;

                //override value only if it is higher than the current one stored in the danger array
                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == false)
            return;

        if (Application.isPlaying && dangersResultTemp != null)
        {
            if (dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(
                        transform.position,
                        Directions.eightDirections[i] * dangersResultTemp[i] * 2
                        );
                }
            }
        }

    }
}