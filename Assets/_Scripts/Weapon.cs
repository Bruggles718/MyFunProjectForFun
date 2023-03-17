using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Drop Prefab")]
    [SerializeField] private GameObject weaponDropPrefab;
    [Header("Behavior Data")]
    [SerializeField] private string animationName;
    [SerializeField] private float attackTime;
    [SerializeField] private bool stopTurningHand;
    private int hashedAnimationName;
    private bool hashedNameSet = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator DoAttack();

    public float GetAttackTime()
    {
        return this.attackTime;
    }

    public string AnimationName()
    {
        return this.animationName;
    }

    public void SetAnimationHash(int value)
    {
        if (!this.hashedNameSet)
        {
            this.hashedAnimationName = value;
            this.hashedNameSet = true;
        }
    }

    public int GetHashedAnimation()
    {
        return this.hashedAnimationName;
    }

    public bool StopTurningHand()
    {
        return this.stopTurningHand;
    }

    public GameObject GetWeaponDrop()
    {
        return this.weaponDropPrefab;
    }
}
