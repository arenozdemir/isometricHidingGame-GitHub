using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Idle : StateBase
{
    [SerializeField] private StateBase chase;
    float timer = 0f; 
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        if (enemyScript.inSight)
        {
            timer += Time.deltaTime;
            if(timer>=1f)
                GoToNextState(chase);
        }
    }
}
