using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private float spawnSwipePrefabTime;
    [SerializeField] private GameObject swipePrefab;
    [SerializeField] private Transform handTransform;
    [SerializeField] private float distanceInfrontToSpawnSwipePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator DoAttack()
    {
        yield return Helpers.WaitForSeconds(this.spawnSwipePrefabTime);
        float swipeAnimDuration = 1.0f / 6.0f;
        Destroy(Instantiate(this.swipePrefab, this.handTransform.position + this.handTransform.right * this.distanceInfrontToSpawnSwipePrefab, this.handTransform.rotation),
                swipeAnimDuration);
    }
}
