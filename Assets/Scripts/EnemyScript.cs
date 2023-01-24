using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] public float radius;
    [Range(0, 360)][SerializeField] public float angle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstuctionMask;

    public GameObject player;
    public bool inSight;

    private int isRunningHash;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(FOVRoutine());
        isRunningHash = Animator.StringToHash("isRunning");
    }
    private void Update()
    {
        //MovementHandler();
    }

    #region "FOV""
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfWievCheck();
        }
    }

    private void FieldOfWievCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < angle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstuctionMask))
                {
                    inSight = true;
                }
                else inSight = false;
            }
            else inSight = false;
        }
        else if (inSight)
            inSight = false;
    }
    #endregion

    public void RotateEnemy()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5);
    }
}
