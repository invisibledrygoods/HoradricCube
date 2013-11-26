using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class StoresContentsInAGrid : MonoBehaviour
{
    public Vector2 size = new Vector2(6, 4);

    public virtual bool TryToAdd(Transform item)
    {
        Vector3 halfSize = item.collider.bounds.size / 2;

        for (float y = halfSize.y; y < size.y - halfSize.y + 0.4f; y++)
        {
            for (float x = halfSize.x; x < size.x - halfSize.x + 0.4f; x++)
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
        Vector2 halfSize = item.collider.bounds.size / 2;
        Vector2 snapTo = new Vector2(Mathf.Round(Mathf.Clamp(at.x, halfSize.x, size.x - halfSize.x) - halfSize.x), Mathf.Round(Mathf.Clamp(at.y, halfSize.y, size.y - halfSize.y) - halfSize.y)) + halfSize;

        for (float y = transform.position.y + snapTo.y + 0.5f - halfSize.y; y < transform.position.y + snapTo.y + halfSize.y; y++)
        {
            for (float x = transform.position.x + snapTo.x + 0.5f - halfSize.x; x < transform.position.x + snapTo.x + halfSize.x; x++)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    if (child.collider.bounds.Contains(new Vector3(x, y, transform.position.z)))
                    {
                        return false;
                    }
                }
            }
        }

        item.parent = transform;
        item.localPosition = snapTo;
        return true;
    }
}
