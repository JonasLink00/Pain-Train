using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Jobs;

public class EnemyController : BaseController
{
    [Header("Enemy")]
    public NavMeshAgent agent;
    public WagonTrigger trigger;

    [SerializeField] private TrainManager trainManager;
    [SerializeField] private bool isWorkingEnemy;

    [Header("WorkingEnemy")]
    [SerializeField]
    private float idleTimer;
    public float IdleTimer { get => idleTimer; set => idleTimer = value; }
    
    [SerializeField] private float Work;
    [SerializeField] private float WorkThreshHold;
    [SerializeField] private LayerMask _FreightLayer;

    public Freight currendFreight;

    [SerializeField]
    Animator enemyAnimator;

    bool AttackAnimation = false;

    private const string WalkString = "isWalking";
    private const string WorkString = "isWorking";

    private const string Attacking = "isAttacking";
    private const string rightPunch = "RightPunch";
    private const string leftPunch = "LeftPunch";


#if UNITY_EDITOR
    [SerializeField] private string currentEnemyState;
    [SerializeField] private string previousEnemyState;
   
    
#endif
    public EnemyBaseState currentState;

    //private StateMachineDelegate stateMachineDelegate;

    private bool freightCheck;

    protected override void Start()
    {
        InitFSM();
    }

    protected override void InitFSM()
    {
        EnemyIdleState idleState = new EnemyIdleState(this);
        EnemyWalkState walkState = new EnemyWalkState(this, agent);
        EnemySearchWorkState searchWorkState = new EnemySearchWorkState(this, agent, _FreightLayer);
        EnemyWorkingState workingState = new EnemyWorkingState(this, agent);
        EnemyAttackState attackState = new EnemyAttackState(this, agent, trainManager, enemyAnimator);


        currentState = idleState;
        currentState.Enter();

        stateDictionary = new Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>>
        {
            {
                idleState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {() => trigger.GetAttacked == true, attackState},
                    {() => !isWorkingEnemy, idleState },
                    {() => Work >= WorkThreshHold, searchWorkState },
                    { CheckIdleTimer, walkState },
                }
            },

            {
                walkState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {

                    {() => trigger.GetAttacked == true, attackState},
                    {() => agent.remainingDistance <= 0.2f, idleState }
                }

            },

            {
                searchWorkState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {() => trigger.GetAttacked, attackState},
                    {() => currendFreight != null, workingState }
                }
            },

            {
                workingState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {() => Work < WorkThreshHold, walkState }
                }
            },
            {
                attackState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    
                }
            },

        };
    }

    public bool CheckIdleTimer()
    {
        ApplyWalkAnimation();
        return idleTimer <= 0;
    }

    private void IncreaseWork()
    {
        Work += Time.deltaTime;
    }

    public void ResetWork()
    {
        CancelWorkAnimation();
        Work = 0;
    }


    protected override void Update()
    {
        UpdateFSM();
        IncreaseWork();
        ApplyWorkAnimation();
        ApplyAttackAnimation();

    }

    protected override void UpdateFSM()
    {
        currentState.Update();

        foreach (var transition in stateDictionary[currentState])
        {
            if(transition.Key() == true) //transition condition is met!
            {
                currentState.Exit();
                currentState = transition.Value;
                currentState.Enter();

#if UNITY_EDITOR
                previousEnemyState = currentEnemyState;
                currentEnemyState = currentState.GetType().Name;
#endif
                break;
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawWireSphere(transform.position, 5);
    //}

    private void ApplyWalkAnimation()
    {
        if (isWorkingEnemy && idleTimer <=0)
        {
            Debug.Log("Walking true");

            enemyAnimator.SetBool(WalkString, true);
        }
        else if (!isWorkingEnemy && trigger.GetAttacked)
        {
            Debug.Log("Walking true");

            enemyAnimator.SetBool(WalkString, true);
        }
        else
        {
            Debug.Log("Walking false");

            enemyAnimator.SetBool(WalkString, false);
        }

    }

    private void ApplyWorkAnimation()
    {
        if (!freightCheck && currendFreight != null)
        {
            StopCoroutine(nameof(FreightCheck));
            StartCoroutine(nameof(FreightCheck));
        }
    }

    private void CancelWorkAnimation()
    {
        if(Work == 0)
        {
            Debug.Log("Working false");

            enemyAnimator.SetBool(WorkString, false);
        }
       
    }
    

    
    private IEnumerator FreightCheck()
    {
        freightCheck = true;
        Debug.Log("Working true");

        enemyAnimator.SetBool(WorkString, true);
        yield return new WaitForSeconds(0.5f);
        freightCheck = false;
    }

    private void ApplyAttackAnimation()
    {
        if (trigger.GetAttacked)
        {
            if(!AttackAnimation)
            {
                Debug.Log("Attacking true");
                enemyAnimator.SetBool(Attacking, true);
                AttackAnimation = true;
            }
            
        }
    }
    public void EnemyAttackAnimation()
    {
        if (agent.remainingDistance <= 0.2f)
        {
            enemyAnimator.SetBool(Attacking, false);
            StopCoroutine(nameof(EnenmyAttacking));
            StartCoroutine(nameof(EnenmyAttacking));
        }
        else if (agent.remainingDistance >= 0.2f)
        {
            Debug.Log("Attacking false");

            enemyAnimator.SetBool(Attacking, false);
            AttackAnimation = false;
        }
    }

    private IEnumerator EnenmyAttacking()
    {


        if(Random.Range(0,2) >= 1)
        {
            Debug.Log("RightAttack true");
            enemyAnimator.SetBool(rightPunch, true);

        }
        else
        {
            Debug.Log("LeftAttack true");
            enemyAnimator.SetBool(leftPunch, true);
        }

        yield return new WaitForSeconds(10);
    }

    private void ResetPunchAnimation()
    {
        enemyAnimator.SetBool(rightPunch, false);

        enemyAnimator.SetBool(leftPunch, false);
    }
}