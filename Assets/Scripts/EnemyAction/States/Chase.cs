using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase : StateBase
{
    Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        /*agent.SetDestination(player.transform.position);
        enemyScript.RotateEnemy();*/
    }
    
}
