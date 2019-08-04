using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityEventListener<T> 
{
    void OnEventRaised(T item);
}
