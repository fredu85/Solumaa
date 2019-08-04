using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityEvent<T> :ScriptableObject
{
    private readonly List<IAbilityEventListener<T>> abilitylistener = new List<IAbilityEventListener<T>>();

    public void Raise(T item)
    {
        for (int i = abilitylistener.Count - 1; i >= 0; i--)
        {
            abilitylistener[i].OnEventRaised(item);
        }
    }

    public void RegisterListener(IAbilityEventListener<T> listener)
    {
        if(!abilitylistener.Contains(listener))
        {
            abilitylistener.Add(listener);
        }
    }
    public void UnregisterListener(IAbilityEventListener<T> listener)
    {
        if (abilitylistener.Contains(listener))
        {
            abilitylistener.Remove(listener);
        }
    }
}
