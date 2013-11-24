using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class StoresContentsInAGrid : MonoBehaviour
{
    public Vector2 size = new Vector2(6, 4);

    public virtual bool TryToAdd(Transform item)
    {
        return false;
    }

    public virtual bool TryToAddAt(Transform item, Vector2 at)
    {
        return false;
    }
}
