using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        MovementHandler();
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

    #region Movement
    private void MovementHandler()
    {
        if (inSight)
        {
            StartCoroutine(AnimationHandler());
            Vector3 dir = player.transform.position - transform.position;
            transform.Translate(dir.normalized * Time.deltaTime * 2f, Space.World);
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    #endregion

    IEnumerator AnimationHandler()
    {
        
        while (inSight)
        {
            yield return null;
            animator.SetBool(isRunningHash, true);
        }
        animator.SetBool(isRunningHash, false);
    }
    
}
