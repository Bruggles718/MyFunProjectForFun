using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Behavior Data")]
    [SerializeField] private string animationName;
    [SerializeField] private float attackTime;
    [SerializeField] private bool stopTurningHand;
    private int hashedAnimationName;
    // Start is called before the first frame update
    void Start()
    {
        this.hashedAnimationName = Animator.StringToHash(this.animationName);
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
        this.hashedAnimationName = value;
    }

    public int GetHashedAnimation()
    {
        return this.hashedAnimationName;
    }

    public bool StopTurningHand()
    {
        return this.stopTurningHand;
    }
}
