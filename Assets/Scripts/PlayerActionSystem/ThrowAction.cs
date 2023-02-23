using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ThrowAction : MonoBehaviour, IObserver
{
    private Animator animator;
    [SerializeField] private Transform rockGenerator;
    private AimAction aimAction;
    private void Awake()
    {
        aimAction = GetComponent<AimAction>();
        animator = GetComponent<Animator>();
    }
    public void OnNotify(PlayerActionsEnum action)
    {
        if (action == PlayerActionsEnum.Throw)
        {
            StartCoroutine(ThrowRockAnimation());
            StartCoroutine(aimAction.PathFollow());
        }
    }
    private IEnumerator ThrowRockAnimation()
    {
        animator.CrossFade("Throw 2", 0.1f);
        yield return null;
    }
    public void ThrowRock()
    {
        //aimAction.DrawPath(aimAction.startPointOfRock.position, aimAction.aimTarget.position, 0.5f);
    }
    private void OnEnable()
    {
        ObserverManager.Instance.AddObserver(this);
    }
    private void OnDisable()
    {
        ObserverManager.Instance.RemoveObserver(this);
    }
}