using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargeterTest : MonoBehaviour
{
    [SerializeField] Transform target, player,ball;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Animator animator;
    [SerializeField] BallLauncer launcer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ball.gameObject.SetActive(true);
            animator.Play("Throw1");
        }
        if (Input.GetMouseButtonUp(0))
        {
           // ball.gameObject.SetActive(false);
            animator.Play("Throw2");
            launcer.Launch();
        }
        if (Input.GetMouseButton(0))
        {
            LookToTarget();
        }
    }

    private void LookToTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, groundMask))
        {
            target.position = hit.point;
            player.LookAt(target.position);
        }
    }
}
