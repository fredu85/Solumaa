using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunTimeThings", menuName = "ScriptableObjects/StringList", order = 54)]
public class StringList : ScriptableObject
{
    public List<string> Items = new List<string>();

    public void Add(string str)
    {
        //  if (!Items.Contains(thing))
        Items.Add(str);
    }

    public void Remove(string str)
    {
        if (Items.Contains(str))
            Items.Remove(str);
    }

    public List<string> GetItems()
    {
        return Items;
    }
}
