using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSetter : MonoBehaviour
{
    [SerializeField] private Transform rootOfBody;
    [SerializeField] private LayerMask groundLayer = default;
    private float footDistance;
    // Update is called once per frame
    private void Start()
    {
        footDistance = transform.localPosition.x;
    }
    void Update()
    {
        Ray ray = new Ray(rootOfBody.position + ( rootOfBody.right * -rootOfBody.transform.localPosition.x), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10f, groundLayer))
        {
            transform.position = info.point;
        }
    }
}
