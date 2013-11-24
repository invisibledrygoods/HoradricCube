using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class StoresContentsInAGrid : MonoBehaviour
{
    public Vector2 size = new Vector2(6, 4);
    public bool shuffle = false;

    public bool TryToAdd(Transform item)
    {
        return false;
    }

    public bool TryToAddAt(Transform item, Vector2 at)
    {
        return false;
    }
}
