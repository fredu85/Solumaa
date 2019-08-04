using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChosenAbility", order = 51)]
public class ChosenAbility : ScriptableObject
{
    public CellObject PlayerObject;

    [SerializeField]
    GameObject _chosenAbility;
    public GameObject chosenAbility
    {
        get { return _chosenAbility; }
        set { _chosenAbility = value; }
    } 

    public Ability.AbilityTypeEnum GetAbilityType()
    {
        return _chosenAbility.GetComponent<AbilityBase>().Type;
    }

    public Ability.AbilityPayEnum GetAbilityPayMethod()
    {
        return _chosenAbility.GetComponent<Ability>().PayMethod;
    }

    public Ability.AbilityTargetEnum GetAbilityTargets()
    {
        return _chosenAbility.GetComponent<AbilityBase>().TargetType;
    }

    public int GetCost()
    {
        if (_chosenAbility != null)
            return _chosenAbility.GetComponent<Ability>().Cost;
        else
            return 0;
    }

    public int GetDamage()
    {
        return _chosenAbility.GetComponent<AbilityBase>().GetDamage();
    }

    public int GetHeal()
    {
        return _chosenAbility.GetComponent<AbilityBase>().GetHeal();
    }

    public string GetName()
    {
        return _chosenAbility.GetComponent<Ability>().GetName();
    }

    public Sprite GetSprite()
    {
        return _chosenAbility.GetComponent<Ability>().picture;
    }

    public void SetTarget(GameObject go)
    {
        if(_chosenAbility != null)
        _chosenAbility.GetComponent<AbilityBase>().SetTarget(go);
    }

    public GameObject GetTarget()
    {
        return _chosenAbility.GetComponent<AbilityBase>().GetTarget();
    }

    public GameObjectData gameObjectData;
}
