using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWorkingState : EnemyBaseState
{
    private NavMeshAgent agent;
    private bool doneWorking;
    public EnemyWorkingState(EnemyController _controller, NavMeshAgent _agent) : base(_controller)
    {
        agent = _agent;
    }

    public override void Enter()
    {
        agent.SetDestination(controller.currendFracht.transform.position);

    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (agent.remainingDistance <= 0.2f)
        {
            controller.currendFracht = null;
            controller.ResetWork();
        }
    }


}
