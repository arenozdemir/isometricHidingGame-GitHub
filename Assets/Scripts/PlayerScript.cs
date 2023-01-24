using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    #region animation tools
    Animator animator;
    int walkingHash;
    bool isWalking;
    #endregion

    CharacterController controller;
    NavMeshAgent meshAgent;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        meshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        walkingHash = Animator.StringToHash("isWalking");
    }
    // Update is called once per frame
    void Update()
    {
        MoveToPosition();
        RotatePlayer();
    }
    private void MoveToPosition()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                meshAgent.SetDestination(hit.point);
                StartCoroutine(AnimateMovement());
            }
        }
    }
    IEnumerator AnimateMovement()
    {
        while (meshAgent.remainingDistance > meshAgent.stoppingDistance)
        {
            animator.SetBool(walkingHash, true);
            yield return null;
        }
        animator.SetBool(walkingHash, false);
    }
    private void RotatePlayer()
    {
        Vector3 direction = meshAgent.steeringTarget - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5);
    }

    #region Throwing Rock


    #endregion
}
