using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunTimeThings", menuName = "ScriptableObjects/ObjectList", order = 54)]
public class ObjectList : ScriptableObject
{
    public List<GameObject> Items = new List<GameObject>();

    public void Add(GameObject go)
    {
        //  if (!Items.Contains(thing))
        Items.Add(go);
    }

    public void Remove(GameObject go)
    {
        if (Items.Contains(go))
            Items.Remove(go);
    }

    public List<GameObject> GetItems()
    {
        return Items;
    }
}
