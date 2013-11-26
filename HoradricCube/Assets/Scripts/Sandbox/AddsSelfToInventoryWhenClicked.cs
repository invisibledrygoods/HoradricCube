using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class AddsSelfToInventoryWhenClicked : MonoBehaviour
{
    public ShufflesContentsIntoAGrid inventory;

    void OnMouseDown()
    {
        Transform clone = (Instantiate(gameObject) as GameObject).transform;
        if (!inventory.TryToAdd(clone))
        {
            Destroy(clone.gameObject);
        }
    }
}
