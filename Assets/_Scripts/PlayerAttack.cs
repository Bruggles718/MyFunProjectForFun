using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private UIButtonPrompt prompt;
    [SerializeField] private Transform handTransform;

    private Animator animator;

    private static readonly int Idle = Animator.StringToHash("Weapons.Idle");

    //[SerializeField] private Weapon sword;
    //[SerializeField] private Weapon bow;

    [SerializeField] private Weapon currentWeapon;

    private bool attacking;

    private int currentBaseState;
    private float lockedTill;
    
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.attacking = false;
        this.lockedTill = 0f;
        //this.sword.SetAnimationHash(Animator.StringToHash(this.sword.AnimationName()));
        //this.bow.SetAnimationHash(Animator.StringToHash(this.bow.AnimationName()));
        //this.currentWeapon.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentWeapon == null) return;
        if (Time.time > this.lockedTill)
        {
            this.handTransform.right = (Helpers.MousePos2D() - this.handTransform.position).normalized;
            this.attacking = false;
        }
        else if (!this.currentWeapon.StopTurningHand())
        {
            this.handTransform.right = (Helpers.MousePos2D() - this.handTransform.position).normalized;
        }
        if (Input.GetMouseButtonDown(0) && !this.attacking && this.currentBaseState == Idle)
        {
            this.attacking = true;
            this.StopAllCoroutines();
            this.StartCoroutine(this.currentWeapon.DoAttack());
        }
        var baseState = this.GetBaseState();
        if (baseState == this.currentBaseState)
        {
            return;
        }
        this.animator.CrossFade(baseState, 0);
        this.currentBaseState = baseState;
    }

    private int GetBaseState()
    {
        if (Time.time < this.lockedTill)
        {
            return this.currentBaseState;
        }
        if (this.attacking)
        {
            return LockState(this.currentWeapon.GetHashedAnimation(), this.currentWeapon.GetAttackTime());
        }
        else
        {
            return Idle;
        }
    }

    int LockState(int state, float duration)
    {
        this.lockedTill = Time.time + duration;
        return state;
    }

    public void SetCurrentWeapon(WeaponDrop weaponDrop)
    {
        if (this.currentWeapon != null)
        {
            this.currentWeapon.gameObject.SetActive(false);
            weaponDrop.DropWeapon(this.currentWeapon.GetWeaponDrop(), this.currentWeapon, this, this.prompt);
        }
        this.currentWeapon = weaponDrop.GetWeapon();
        this.currentWeapon.gameObject.SetActive(true);
        this.currentWeapon.SetAnimationHash(Animator.StringToHash(this.currentWeapon.AnimationName()));
        Destroy(weaponDrop.gameObject);
        
    }
}
