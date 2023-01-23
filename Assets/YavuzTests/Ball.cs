using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject effect;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Instantiate(effect,transform.position,Quaternion.identity);
        }
            
    }
}
