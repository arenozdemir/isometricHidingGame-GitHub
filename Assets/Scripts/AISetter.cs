using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISetter : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform item in transform)
        {
            item.GetComponent<StateBase>().animator = transform.parent.GetComponent<Animator>();
            item.GetComponent<StateBase>().agent = transform.parent.GetComponent<NavMeshAgent>();
            item.GetComponent<StateBase>().enemyScript = transform.parent.GetComponent<EnemyScript>();
        }
    }
}
