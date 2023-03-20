using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    private float targetDetectionRange = 5;

    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;

    [SerializeField]
    private bool showGizmos = false;

    //gizmo parameters
    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        /*//Find out if player is near
        Collider2D playerCollider =
            Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            //Check if you see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask);

            //Make sure that the collider we see is on the "Player" layer
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            //Enemy doesn't see the player
            colliders = null;
        }*/
        this.colliders = DetectHelper(this.transform.position);
        aiData.SetTargets(this.colliders);
    }

    public List<Transform> DetectHelper(Vector3 position)
    {
        List<Transform> collidersTemp = new List<Transform>();

        //Find out if player is near
        Collider2D playerCollider =
            Physics2D.OverlapCircle(position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            //Check if you see the player
            Vector2 direction = (playerCollider.transform.position - position).normalized;
            RaycastHit2D hit =
                Physics2D.Raycast(position, direction, targetDetectionRange, obstaclesLayerMask);

            //Make sure that the collider we see is on the "Player" layer
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                //Debug.DrawRay(position, direction * targetDetectionRange, Color.magenta);
                collidersTemp = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                collidersTemp = null;
            }
        }
        else
        {
            //Enemy doesn't see the player
            collidersTemp = null;
        }

        return collidersTemp;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}