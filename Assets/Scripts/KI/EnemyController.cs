using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public delegate bool StateMachineDelegate();
public class EnemyController : BaseController
{
    [SerializeField]
    private float idleTimer;

    public float IdleTimer { get => idleTimer; set => idleTimer = value; }

    [SerializeField] private float Work;
    [SerializeField] private float WorkThreshHold;
    [SerializeField] private LayerMask _FreightLayer;

    public WagonTrigger trigger;

#if UNITY_EDITOR
    [SerializeField] private string currentEnemyState;
    [SerializeField] private string previousEnemyState;
   
    
#endif

    [SerializeField]
    private NavMeshAgent agent;

    public EnemyBaseState currentState;

    private StateMachineDelegate stateMachineDelegate;


    public Freight currendFreight;

    

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
        EnemyAttackState attackState = new EnemyAttackState(this, agent);


        currentState = idleState;
        currentState.Enter();

        stateDictionary = new Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>>
        {
            {
                idleState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {() => Work >= WorkThreshHold, searchWorkState },
                    { CheckIdleTimer, walkState },
                }
            },

            {
                walkState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    
                    //{() =>  trigger.GetAttacked = true, attackState},
                    { () => agent.remainingDistance <= 0.2f, idleState }
                }

            },

            {
                searchWorkState,
                new Dictionary<StateMachineDelegate, EnemyBaseState> 
                {
                    //{() =>  trigger.GetAttacked = true, attackState},
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

            //{
            //    attackState,
            //    new Dictionary<StateMachineDelegate, EnemyBaseState>
            //    {
            //        {() => Work < WorkThreshHold, walkState }
            //    }
            //},

        };
    }

    public bool CheckIdleTimer()
    {
        return idleTimer <= 0;
    }

    private void IncreaseWork()
    {
        Work += Time.deltaTime;
    }

    public void ResetWork()
    {
        Work = 0;
    }


    protected override void Update()
    {
        UpdateFSM();
        IncreaseWork();
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

}