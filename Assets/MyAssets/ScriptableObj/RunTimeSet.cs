using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //public abstract class RuntimeSet<T>: ScriptableObject
    //{
    //    public List<T> Items = new List<T>();

    //    public void Add(T thing)
    //    {
    //      //  if (!Items.Contains(thing))
    //            Items.Add(thing);
    //    }

    //    public void Remove(T thing)
    //    {
    //        if (Items.Contains(thing))
    //            Items.Remove(thing);
    //    }

    //    public List<T> GetItems()
    //    {
    //    return Items;
    //    }
    //}
[CreateAssetMenu(fileName = "RunTimeThings", menuName = "ScriptableObjects/RunTimeThings", order = 54)]
public class RuntimeSet : ScriptableObject
{
    public List<GameObject> Items = new List<GameObject>();

    public void Add(GameObject thing)
    {
        //  if (!Items.Contains(thing))
        Items.Add(thing);
    }

    public void Remove(GameObject thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);
    }

    public List<GameObject> GetItems()
    {
        return Items;
    }
}