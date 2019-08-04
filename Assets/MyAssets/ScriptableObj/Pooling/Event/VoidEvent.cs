using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New VoidEvent", menuName = "ScriptableObjects/New Void Event", order = 56)]
public class VoidEvent : BaseAbilityEvent<Void>
{
    public void Raise() => Raise(new Void());
}
