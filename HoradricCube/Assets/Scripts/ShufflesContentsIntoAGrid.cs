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

        Debug.Log("child count: " + transform.childCount);

        foreach (Transform child in transform)
        {
            originalItemList.Add(child);
        }

        // it doesn't throw a collection modified exception if you do this up there, but it does break
        foreach (Transform child in originalItemList)
        {
            child.parent = null;
        }

        itemList = new List<Transform>(originalItemList);
        itemList.Add(item);

        itemList.Sort((item1, item2) =>
            {
                return (int)(item1.collider.bounds.size.magnitude - item2.collider.bounds.size.magnitude);
            }
        );

        bool added = false;

        Debug.Log("shuffling " + itemList.Count + " items");

        foreach (Transform currentItem in itemList)
        {
            Debug.Log("there are " + inventory.transform.childCount + " items currently in the inventory");
            added = inventory.TryToAdd(currentItem);
            if (added)
            {
                Debug.Log("added " + currentItem.collider.bounds.size + " at " + currentItem.localPosition);
            }
            else
            {
                Debug.Log("failed to add " + currentItem.collider.bounds.size);
            }

            if (!added)
            {
                break;
            }
        }

        if (!added)
        {
            foreach (Transform child in transform)
            {
                child.parent = null;
            }

            foreach (Transform currentItem in originalItemList)
            {
                inventory.TryToAdd(currentItem);
            }
        }

        return added;
    }
}
