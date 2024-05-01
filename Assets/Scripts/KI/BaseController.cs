using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseController : MonoBehaviour
{
    protected Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>> stateDictionary;

    protected virtual void Start()
    {
        InitFSM();

    }

    protected virtual void Update()
    {
        UpdateFSM();
    }

    protected virtual void InitFSM()
    {

    }

    protected virtual void UpdateFSM()
    {

    }
}
