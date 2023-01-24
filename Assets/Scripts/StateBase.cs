using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBase : MonoBehaviour
{
    public EnemyScript enemyScript;
    public Animator animator;
    public NavMeshAgent agent;
    [SerializeField] public string whichAnimation;
    public void GoToNextState(StateBase nextState)
    {
        if (nextState != null)
        {
            animator.CrossFade(nextState.whichAnimation, 0.2f);
            nextState.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
