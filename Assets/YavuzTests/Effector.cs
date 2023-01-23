using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector : MonoBehaviour
{
    float scale;
    void Update()
    {
        scale += Time.deltaTime;
        transform.localScale = Vector3.one* scale;
        if(scale > 1)
            Destroy(gameObject);
    }
}
