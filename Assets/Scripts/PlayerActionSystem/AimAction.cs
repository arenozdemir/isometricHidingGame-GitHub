using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class AimAction : MonoBehaviour, IObserver
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Transform rockGenerator;
    private LineRenderer lineRenderer;
    private int vertexCount = 16;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertexCount;
    }
    public void OnNotify(PlayerActionsEnum action)
    {
        if (action == PlayerActionsEnum.Aim && (rockGenerator.GetChild(0).gameObject.activeSelf==true)) 
        {
            aimTarget.gameObject.SetActive(true);
            StartCoroutine(SetWeightOfRig(0.6f));
            StartCoroutine(PathSetter());
        }
        else 
        {
            aimTarget.gameObject.SetActive(false);
            StartCoroutine(SetWeightOfRig(0.2f));
            StopCoroutine(PathSetter());
        }
    }
    #region DrawPath
    private IEnumerator PathSetter()
    {
        while (true)
        {
            for(int i = 0; i<vertexCount; i++)
            {
                float percent = (float)i / (vertexCount - 1);
                lineRenderer.SetPosition(i, DrawPath(rockGenerator.position, aimTarget.position, percent));
            }
            yield return null;
        }
    }
    private Vector3 DrawPath(Vector3 start, Vector3 end, float t)
    {
        Vector3 middlePoint = ((start + end) / 2) + Vector3.up * 2;
        Vector3 a = Vector3.Lerp(start, middlePoint, t);
        Vector3 b = Vector3.Lerp(middlePoint, end, t);
        return Vector3.Lerp(a, b, t);
    }
    public IEnumerator PathFollow()
    {
        Vector3 startPos = rockGenerator.position;
        Vector3 endPos = aimTarget.position;
        float t = 0f;
        Transform rock = rockGenerator.GetChild(0);
        rock.parent = null;
        while (true)
        {
            t += Time.deltaTime;
            rock.position = DrawPath(startPos, endPos, t);
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
    #region SetWeightOfRig
    private IEnumerator SetWeightOfRig(float weight)
    {
        switch (weight)
        {
            case 0.2f:
                while (rig.weight > 0.2f)
                {
                    rig.weight = Mathf.Lerp(rig.weight, 0.19f, Time.deltaTime);
                    yield return null;
                }
                break;
            case 0.6f:
                while (rig.weight < 0.59f)
                {
                    rig.weight = Mathf.Lerp(rig.weight, 0.6f, Time.deltaTime);
                    yield return null;
                }
                break;
        }
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
