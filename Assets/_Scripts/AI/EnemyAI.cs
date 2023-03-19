using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{

    private static readonly int Idle = Animator.StringToHash("Movement.Idle");
    private static readonly int Run = Animator.StringToHash("Movement.Run");

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    };

    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    /*[SerializeField]
    private float detectionDelay = 0.01f, aiUpdateDelay = 0.01f, attackDelay = 1f;*/

    [SerializeField]
    private float attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    /*public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;*/

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    //bool following = false;

    [SerializeField] private float speed = 2;

    private Vector2 moveVector;

    private float lockedTill;

    private Rigidbody2D rb;

    private Animator animator;

    private int currentBaseState;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private EnemyState currentEnemyState;

    private void Start()
    {
        //Detecting Player and Obstacles around
        //InvokeRepeating("PerformDetection", 0, detectionDelay);

        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.currentEnemyState = EnemyState.Idle;
        this.currentBaseState = Idle;
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        PerformDetection();

        //Enemy AI movement based on Target availability
        
        //Moving the Agent
        //OnMovementInput?.Invoke(movementInput);

        switch(this.currentEnemyState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Chase:
                UpdateChase();
                break;
        }

        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
        {
            if (aiData.currentTarget.position.x < this.transform.position.x)
            {
                this.spriteRenderer.flipX = true;
            }
            else
            {
                this.spriteRenderer.flipX = false;
            }
        }

        var state = GetBaseState();

        if (state == currentBaseState) return;
        this.animator.CrossFade(state, 0);
        this.currentBaseState = state;
    }

    private void FixedUpdate()
    {
        this.rb.velocity = this.moveVector;
    }

    private void UpdateChase()
    {
        if (aiData.currentTarget == null)
        {
            //Stopping Logic
            Debug.Log("Stopping");
            moveVector = Vector2.zero;
            this.currentEnemyState = EnemyState.Idle;
            return;
            //yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < attackDistance)
            {
                //Attack logic
                //movementInput = Vector2.zero;
                //OnAttackPressed?.Invoke();
                //yield return new WaitForSeconds(attackDelay);
                //StartCoroutine(ChaseAndAttack());

                this.currentEnemyState = EnemyState.Attack;
            }
            else
            {
                //Chase logic
                moveVector = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData) * speed;
                //yield return new WaitForSeconds(aiUpdateDelay);
                //StartCoroutine(ChaseAndAttack());
            }

        }
    }

    private void UpdateIdle()
    {
        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            //OnPointerInput?.Invoke(aiData.currentTarget.position);
            if (this.currentEnemyState != EnemyState.Chase)
            {
                this.currentEnemyState = EnemyState.Chase;
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
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