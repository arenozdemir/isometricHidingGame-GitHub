using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISetter : MonoBehaviour
{
    StateBase stateBase;
    private void Awake()
    {
        stateBase = GetComponent<StateBase>();
        
        /*stateBase.enemyScript = GetComponentInParent<EnemyScript>();
        stateBase.agent = GetComponentInParent<NavMeshAgent>();
        stateBase.animator = GetComponentInParent<Animator>();*/
    }
}
