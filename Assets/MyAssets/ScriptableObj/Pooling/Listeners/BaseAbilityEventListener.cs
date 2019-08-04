using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseAbilityEventListener<T, E, UER> : MonoBehaviour,
    IAbilityEventListener<T> where E : BaseAbilityEvent<T> where UER : UnityEvent<T>
{
    [SerializeField]
    private E _AbilityEvent;
    public E AbilityEvent { get { return _AbilityEvent; } set { AbilityEvent = value; } }

    [SerializeField]
    private UER unityAbilityEventResponse;

    private void OnEnable()
    {
        if (_AbilityEvent == null)
            return;

        _AbilityEvent.RegisterListener(this);

    }

    private void OnDisable()
    {
        if (_AbilityEvent == null)
            return;

        _AbilityEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T item)
    {
        if (unityAbilityEventResponse != null)
        {
            unityAbilityEventResponse.Invoke(item);
        }
    }
}
