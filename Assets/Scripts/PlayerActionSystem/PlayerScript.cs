using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PlayerScript : ObserverManager
{
    #region animation tools
    Animator animator;
    int walkingHash;
    bool isWalking;
    #endregion
    CharacterController controller;
    NavMeshAgent playerNavMeshAgent;
    private InputManager playerInput;
    private void Awake()
    {
        playerInput = new InputManager();
        controller = GetComponent<CharacterController>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerInput.FindAction("PickUp").started += PickUp;
        playerInput.FindAction("PickUp").performed += PickUp;
        playerInput.FindAction("PickUp").canceled += PickUp;

        playerInput.FindAction("Throw").started += Throw;
        playerInput.FindAction("Throw").performed += Throw;
        playerInput.FindAction("Throw").canceled += Throw;
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
    #region moving
    private void MoveToPosition()
    {
        if (Mouse.current.rightButton.IsPressed())
        {
            NotifyObservers(PlayerActionsEnum.Moving);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                playerNavMeshAgent.SetDestination(hit.point);
                StartCoroutine(AnimateMovement());
            }
        }
    }
    IEnumerator AnimateMovement()
    {
        bool isMoving = true;
        while (playerNavMeshAgent.remainingDistance > playerNavMeshAgent.stoppingDistance)
        {
            animator.SetBool(walkingHash, isMoving);
            yield return null;
        }
        yield return null;
        isMoving = false;
        animator.SetBool(walkingHash, isMoving);
    }
    #endregion

    #region rotating
    private void RotatePlayer()
    {
        Vector3 direction = playerNavMeshAgent.steeringTarget - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5);
    }
    #endregion

    #region pickUp
    private void PickUp(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) NotifyObservers(PlayerActionsEnum.PickUp);
    }
    #endregion

    #region throw
    private void Throw(InputAction.CallbackContext context)
    {
        bool aim = context.ReadValueAsButton();
        if (aim) NotifyObservers(PlayerActionsEnum.Aim);
        else if(context.canceled) NotifyObservers(PlayerActionsEnum.Throw);
    }
    #endregion
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}
