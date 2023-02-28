using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector3 moveVector;

    private static readonly int Idle = Animator.StringToHash("Movement.Idle");
    private static readonly int Run = Animator.StringToHash("Movement.Run");

    private int currentBaseState;
    private float lockedTill;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();

        this.moveVector = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        this.moveVector.x = Input.GetAxisRaw("Horizontal");
        this.moveVector.y = Input.GetAxisRaw("Vertical");
        this.moveVector.Normalize();
        this.moveVector *= this.movementSpeed;

        if (Helpers.MousePos2D().x < this.transform.position.x)
        {
            this.spriteRenderer.flipX = true;
        }
        else
        {
            this.spriteRenderer.flipX = false;
        }

        var baseState = this.GetBaseState();
        if (baseState == this.currentBaseState)
        {
            return;
        }
        this.animator.CrossFade(baseState, 0);
        this.currentBaseState = baseState;
    }

    private void FixedUpdate()
    {
        this.rb.velocity = this.moveVector;
    }

    private int GetBaseState()
    {
        if (Time.time < this.lockedTill)
        {
            return this.currentBaseState;
        }
        if (this.moveVector.x == 0 && this.moveVector.y == 0)
        {
            return Idle;
        }
        else
        {
            return Run;
        }
    }

    int LockState(int state, float duration)
    {
        this.lockedTill = Time.time + duration;
        return state;
    }
}