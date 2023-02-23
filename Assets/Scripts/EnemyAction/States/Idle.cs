using System.Collections;
using System.Diagnostics;

public class Idle : StateBase
{
    private void Start()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
        StartCoroutine(IdleState());
    }
    private IEnumerator IdleState()
    {
        while (!enemyScript.inSight)
        {
            yield return null;
        }
        GoToNextState(states[1], this);
    }
}
