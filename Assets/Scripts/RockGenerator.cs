using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    private GameObject rock;
    private void Awake()
    {
        rock = transform.GetChild(0).gameObject;
    }
    private void RockSetter()
    {
        if (transform.childCount == 0)
        {
            Instantiate(rock, transform.position, Quaternion.identity, transform);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        RockSetter();
    }
}
