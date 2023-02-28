using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private GameObject arrowProjectilePrefab;
    [SerializeField] private float arrowSpawnDelay;
    [SerializeField] private Transform handTransform;
    [SerializeField] private float arrowDestroyDelay;
    [SerializeField] private float arrowSpeed;

    public override IEnumerator DoAttack()
    {
        yield return Helpers.WaitForSeconds(this.arrowSpawnDelay);
        GameObject arrow = Instantiate(this.arrowProjectilePrefab,
            this.handTransform.position + this.handTransform.right * Vector3.Distance(this.handTransform.position, this.transform.position),
            this.handTransform.rotation);
        arrow.transform.up = this.handTransform.right;
        arrow.GetComponent<Rigidbody2D>().velocity = this.handTransform.right * this.arrowSpeed;
        Destroy(arrow, this.arrowDestroyDelay);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
