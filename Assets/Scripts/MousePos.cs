using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePos : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        //set position to mouse current position, mouse position can only calculate in screen space
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
        {
            transform.position = hit.point;
        }
    }
}
