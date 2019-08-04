using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPickAttack : MonoBehaviour
{
    AI _parent;
    Cell AI_parent;

    private void Awake()
    {
        _parent = gameObject.GetComponent<AI>();
        AI_parent = gameObject.GetComponent<Cell>();
    }

    public  void Attackselect()//randomly select attack ability
    {
        List<GameObject> Enemies = AI_parent.Enemies;
        List<GameObject> Attack = _parent.Attack;

        if (Enemies.Count < 1)
        {
            return;
        }
        if (Attack.Count < 1)
        {//if no attacks do nothing
            _parent.PickAction();
            return;
        }

        int shieldcount = 0;
        foreach (GameObject nmy in Enemies)
        {//count how many shields
            if (nmy.GetComponentInChildren<Shield>())
            {
                shieldcount++;
            }
            if (nmy.GetComponentInChildren<MegaShield>())
            {
                shieldcount = shieldcount + 2;
            }
        }
        if (Enemies.Count - shieldcount < 1)
        {//if there are too many shields fire missile
            foreach (GameObject atk in Attack)
            {
                if (atk.GetComponent<Burger>())
                {
                    _parent.chosenAbility = atk;
                }
            }
            if (_parent.chosenAbility == null)
            {//if there is no missile attack pick another at random
                int x = Random.Range(0, Attack.Count);
                _parent.chosenAbility = Attack[x];
            }
        }
        else
        {
            int x = Random.Range(0, Attack.Count);
            _parent.chosenAbility = Attack[x];
        }
        // isbusy = true;
        AI_parent.Cost = _parent.chosenAbility.GetComponent<Ability>().GetCost();
        AI_parent._GameObjectData.AbilityPrefab = _parent.chosenAbility;
        _parent.TargetSelect();
        //x = Random.Range(0, enemies.Count);
        //target = enemies[x];

    }
}
