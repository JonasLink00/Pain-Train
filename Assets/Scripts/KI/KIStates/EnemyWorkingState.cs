using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWorkingState : EnemyBaseState
{
    private NavMeshAgent agent;
    public EnemyWorkingState(EnemyController _controller, NavMeshAgent _agent) : base(_controller)
    {
        agent = _agent;
    }

    public override void Enter()
    {
        agent.SetDestination(controller.currendFreight.transform.position);

    }

    public override void Exit()
    {
        
        controller.currendFreight = null;
        agent.SetDestination(controller.enterPosition);

    }

    public override void Update()
    {
        if (agent.remainingDistance <= 0.2f)
        {
            controller.StartCoroutine(TimeOnFreight());
            controller.ResetWork();
        }
    }

    IEnumerator TimeOnFreight()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(controller.currendFreight.timeOnFreight);
    }
     
}
