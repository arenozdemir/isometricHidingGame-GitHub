using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePos : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        //transform position to mouse position related with the camera
        transform.position = Camera.main.WorldToScreenPoint(Mouse.current.position.ReadValue());
        Debug.Log(transform.position);
    }
}
