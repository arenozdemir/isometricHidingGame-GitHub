using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRock : MonoBehaviour, IObserver
{
    private Animator animator;
    [SerializeField] private Transform rock;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnNotify(PlayerActionsEnum action)
    {
        if (action == PlayerActionsEnum.PickUp)
        {
            Debug.Log("RockGenerator: OnNotify");
            PickUpAnimation();
        }
    }
    #region pick up animation
    private void PickUpAnimation()
    {
        animator.CrossFade("Throw 1", 0.1f);
    }

    public void RockGenerator()
    {
        rock.GetChild(0).gameObject.SetActive(true);
    }
    #endregion
    private void OnEnable()
    {
        ObserverManager.Instance.AddObserver(this);
    }
    private void OnDisable()
    {
        ObserverManager.Instance.RemoveObserver(this);
    }
}
