using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RealizePlayer : StateBase
{
    [SerializeField] private Transform headTracker;
    private void OnEnable()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
        Debug.Log("RealizePlayer");
        StartCoroutine(Realize());
    }
    private IEnumerator Realize()
    {
        while (enemyScript.inSight)
        {
            //rotate to player
            Vector3 direction = enemyScript.player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            enemyScript.transform.rotation = Quaternion.Slerp(enemyScript.transform.rotation, lookRotation, Time.deltaTime * 5f);
            yield return null;
        }
    }
}

