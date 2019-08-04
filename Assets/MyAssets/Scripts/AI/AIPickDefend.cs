using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPickDefend : MonoBehaviour
{
    AI _parent;

    private void Awake()
    {
        _parent = gameObject.GetComponent<AI>();
    }

    public void Defendselect()//randomly select an attack and a target
    {
        if (_parent.Friends.Count < 1)
        {
            return;
        }

        if (_parent.Defence.Count < 1)
        {//if no defence, do nothing
            _parent.PickAction();
            return;
        }

        int x = Random.Range(0, _parent.Defence.Count);
        _parent.chosenAbility = _parent.Defence[x];


        //check if there is a megashield and if so pick another ability
        if (_parent.chosenAbility.GetComponent<MegaShield>())
        {
            foreach (GameObject frind in _parent.Friends)
            {
                if (!frind.GetComponentInChildren<MegaShield>() && _parent.GetHealth() > _parent.chosenAbility.GetComponent<AbilityBase>().GetDamage())
                {//if a megshield is not active
                    //start charging megashield
                }
                else
                {
                    if (_parent.Defence.Count > 1)
                    {//make sure there are other abilities than megashield to choose from
                        while (_parent.chosenAbility.GetComponent<MegaShield>())
                        {//pick a new ability until we are no longer picking megashield
                            int y = Random.Range(0, _parent.Defence.Count);
                            _parent.chosenAbility = _parent.Defence[y];
                        }
                    }
                    else
                        _parent.PickAction();

                }
            }
        }

        //check if should use healingberry
        if (_parent.chosenAbility.GetComponent<HealingBerry>())
        {
            List<GameObject> healable = new List<GameObject>();
            foreach (GameObject go in _parent.Friends)
            {
                if (go.GetComponent<Cell>().GetHealth() != go.GetComponent<Cell>().GetMaxHealth())
                {//not at full hp
                    healable.Add(go);
                }
            }
            if (healable.Count == 0)
            {//no targets to heal, pick new ability
                if (_parent.Defence.Count > 2)
                {//more than just megashield and healingberry
                    while (_parent.chosenAbility.GetComponent<MegaShield>() || _parent.chosenAbility.GetComponent<HealingBerry>())
                    {//pick until something that is not megashield and healingberry is picked
                        int y = Random.Range(0, _parent.Defence.Count);
                        _parent.chosenAbility = _parent.Defence[y];
                    }
                }
                else//if there are no abilities to pick
                    _parent.PickAction();
            }
        }

        _parent._GameObjectData.AbilityPrefab = _parent.chosenAbility;
        _parent.Cost = _parent.chosenAbility.GetComponent<Ability>().GetCost();
        _parent.TargetSelect();
    }
}
