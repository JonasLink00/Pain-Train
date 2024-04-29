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
    private bool timerStarted;
    private float waitTimerAtPosition;
    private LayerMask FreightLayer;
    private bool FOUND;

    public EnemySearchWorkState(EnemyController _controller, NavMeshAgent _agent, LayerMask _FreightLayer) : base(_controller)
    {
        agent = _agent;
        searchWalkRadius = 10;
        waitTimerAtPosition = 10;
        FreightLayer = _FreightLayer;
    }

    public override void Enter()
    {
        Debug.Log("SearchworkStateEnter");
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
        if (agent.remainingDistance <= 0.2f)
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


        FOUND = SearchFreight();

        if(!FOUND)
        {
            Vector3 newPos = startPosition + Random.insideUnitSphere * searchWalkRadius;
            agent.SetDestination(newPos);
        }

        timerStarted = false;
    }

    private bool SearchFreight()
    {
        Collider[] cols = Physics.OverlapSphere(controller.transform.position, 20, FreightLayer);

        if (!cols.Any()) 
            return false;
        controller.currendFreight = cols[0].GetComponent<Freight>();
            return true;
    }
   
}
