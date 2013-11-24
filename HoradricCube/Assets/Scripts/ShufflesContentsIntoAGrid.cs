using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class ShufflesContentsIntoAGrid : StoresContentsInAGrid
{
    public override bool TryToAdd(Transform item)
    {
        return false;
    }

    public override bool TryToAddAt(Transform item, Vector2 at)
    {
        return false;
    }
}
