using UnityEngine;
using System.Collections.Generic;
using System;
using Require;

public class ShufflesContentsIntoAGrid : MonoBehaviour
{
    StoresContentsInAGrid inventory;

    public void Awake()
    {
        inventory = transform.Require<StoresContentsInAGrid>();
    }

    public List<T> Shuffle<T>(List<T> list)
    {
        List<T> ret = new List<T>();

        while (list.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            ret.Add(list[index]);
            list.RemoveAt(index);
        }

        return ret;
    }

    public bool TryToAdd(Transform item)
    {
        List<Transform> itemList = new List<Transform>();
        Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();

        if (inventory.TryToAdd(item))
        {
            return true;
        }

        foreach (Transform child in transform)
        {
            originalPositions[child] = child.position;
            itemList.Add(child);
        }

        // try a few shuffles before giving up
        for (int i = 0; i < 20; i++)
        {
            // it doesn't throw a collection modified exception if you do this up there, but it does break
            foreach (Transform child in itemList)
            {
                child.parent = null;
            }

            itemList = Shuffle(itemList);

            itemList.Add(item);
            itemList.Sort((item1, item2) => (int)((item2.collider.bounds.size.magnitude - item1.collider.bounds.size.magnitude) * 10.0f));

            bool failed = false;

            foreach (Transform currentItem in itemList)
            {
                if (!inventory.TryToAdd(currentItem))
                {
                    failed = true;
                    break;
                }
            }

            if (failed)
            {
                foreach (KeyValuePair<Transform, Vector3> originalPosition in originalPositions)
                {
                    originalPosition.Key.position = originalPosition.Value;
                    originalPosition.Key.parent = transform;
                }
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
