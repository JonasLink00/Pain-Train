using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public NavMeshAgent agent;
    public TrainManager trainManager;
    private PlayerInput player;

    Animator enemyAnimator;

    

    public EnemyAttackState(EnemyController _controller, NavMeshAgent _agent, TrainManager _trainManager, Animator _enemyAnimator) : base(_controller)
    {
        agent = _agent;
        trainManager = _trainManager;
        controller = _controller;
        enemyAnimator = _enemyAnimator;
    }

    
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        //trigger works only one time
        trainManager.Playerspotted();
        
        controller.EnemyAttackAnimation();
    }

    
}
