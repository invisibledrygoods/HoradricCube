using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class ShufflesContentsIntoAGrid : MonoBehaviour
{
    public StoresContentsInAGrid inventory;

    public void Awake()
    {
        inventory = transform.Require<StoresContentsInAGrid>();
    }

    public bool TryToAdd(Transform item)
    {
        List<Transform> itemList;
        List<Transform> originalItemList = new List<Transform>();

        foreach (Transform child in transform)
        {
            originalItemList.Add(child);
        }
        inventory.clearChildren();
        itemList = new List<Transform>(originalItemList);
        itemList.Add(item);

        itemList.Sort(delegate(Transform item1, Transform item2)
            {
                float size1 = item1.collider.bounds.size.x * item1.collider.bounds.size.y;
                float size2 = item2.collider.bounds.size.x * item2.collider.bounds.size.y;

                if (size1 > size2)
                {
                    return -1;
                }
                else if (size1 == size2)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        );

        bool added = false;

        foreach (Transform currentItem in itemList)
        {
            added = inventory.TryToAdd(currentItem);

            if (!added)
            {
                break;
            }
        }

        if (!added)
        {
            inventory.clearChildren();

            foreach (Transform currentItem in originalItemList)
            {
                inventory.TryToAdd(currentItem);
            }
        }

        return added;
    }
}
