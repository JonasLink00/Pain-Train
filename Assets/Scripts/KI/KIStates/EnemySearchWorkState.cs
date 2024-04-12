using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchWorkState : EnemyBaseState
{
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float searchWalkRadius;
    private bool goToStart;
    private bool timerStarted;
    private float waitTimerAtPosition;
    private LayerMask FrachtLayer;

    public EnemySearchWorkState(EnemyController _controller, NavMeshAgent _agent, LayerMask _FrachtLayer) : base(_controller)
    {
        agent = _agent;
        searchWalkRadius = 5;
        waitTimerAtPosition = 10;
        FrachtLayer = _FrachtLayer;
    }

    public override void Enter()
    {
        startPosition = controller.transform.position;
        Vector3 newPos = startPosition + Random.insideUnitSphere * searchWalkRadius;

        //Check for Nav Mesh

        agent.SetDestination(newPos);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if(agent.remainingDistance <= 0.2f)
        {
            if(!timerStarted)
            {
                controller.StartCoroutine(C_WaitForNewPosition());
            }
        }
    }

    private IEnumerator C_WaitForNewPosition()
    {
        timerStarted = true;

        yield return new WaitForSeconds(waitTimerAtPosition);

        goToStart = !goToStart;

        if(goToStart)
        {
            SearchFracht();
        }
        else
        {
            Vector3 newPos = startPosition + Random.insideUnitSphere * searchWalkRadius;
            agent.SetDestination(newPos);
        }

        timerStarted = false;
    }

    private void SearchFracht()
    {
        Collider[] cols = Physics.OverlapSphere(controller.transform.position, 20, FrachtLayer);

        if (!cols.Any()) return;
        controller.currendFracht = cols[0].GetComponent<Fracht>();
    }
}
