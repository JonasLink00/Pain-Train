using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;


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
    private const string WalkString = "isWalking";
    private const string WorkString = "isWorking";


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
        EnemyAttackState attackState = new EnemyAttackState(this, agent, trainManager);


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
        if (idleTimer <=0)
        {
            Debug.Log("Animation true");

            enemyAnimator.SetBool(WalkString, true);
        }
        else
        {
            Debug.Log("Animation false");

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
    

    //verhindert das mehrere Gegner gleichzeitig spawnen
    private IEnumerator FreightCheck()
    {
        freightCheck = true;
        Debug.Log("Working true");

        enemyAnimator.SetBool(WorkString, true);
        yield return new WaitForSeconds(2);
        freightCheck = false;
    }
}