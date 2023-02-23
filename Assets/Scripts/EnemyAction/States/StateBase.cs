using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBase : MonoBehaviour
{
    protected static List<StateBase> states = new List<StateBase>();
    [SerializeField] public string whichAnimation;

    public EnemyScript enemyScript;
    public NavMeshAgent agent;
    public Animator animator;
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            states.Add(child.GetComponent<StateBase>());
        }
    }
    protected void GoToNextState(StateBase nextState, StateBase currentState)
    {
        if (nextState != null)
        {
            currentState.gameObject.SetActive(false);
            nextState.gameObject.SetActive(true);
        }
    }

}