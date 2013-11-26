using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class TurnsRedWhenInAnInventory : MonoBehaviour
{
    void Update()
    {
        if (transform.parent != null)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }
}
