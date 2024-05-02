using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public NavMeshAgent agent;
    public TrainManager trainManager;

    public EnemyAttackState(EnemyController _controller, NavMeshAgent _agent, TrainManager _trainManager) : base(_controller)
    {
        agent = _agent;
        trainManager = _trainManager;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
       //if Savezone is wanted

       //if (trainManager.trigger.GetAttacked)
       //{
       //    trainManager.Playerspotted();
       //}

        //trigger works only one time
        trainManager.Playerspotted();
    }
}
