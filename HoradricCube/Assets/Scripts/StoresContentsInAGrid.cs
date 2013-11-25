using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class StoresContentsInAGrid : MonoBehaviour
{
    public Vector2 size = new Vector2(6, 4);

    public virtual bool TryToAdd(Transform item)
    {
        Vector3 itemSize = item.collider.bounds.size;

        for (float y = itemSize.y / 2; y < size.y - itemSize.y / 2; y++)
        {
            for (float x = itemSize.x / 2; x < size.x - itemSize.x / 2; x++)
            {
                if (TryToAddAt(item, new Vector2(x, y)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public virtual bool TryToAddAt(Transform item, Vector2 at)
    {
        Vector3 itemSize = item.collider.bounds.size;

        for (float y = transform.position.y + at.y + 0.5f - itemSize.y / 2; y < transform.position.y + at.y + itemSize.y / 2; y++)
        {
            for (float x = transform.position.x + at.x + 0.5f - itemSize.x / 2; x < transform.position.x + at.x + itemSize.x / 2; x++)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    Debug.Log(child.collider.bounds + " :: " + new Vector3(x, y, transform.position.z));

                    if (child.collider.bounds.Contains(new Vector3(x, y, transform.position.z)))
                    {
                        return false;
                    }
                }
            }
        }

        item.parent = transform;
        item.localPosition = at;
        return true;
    }
}
