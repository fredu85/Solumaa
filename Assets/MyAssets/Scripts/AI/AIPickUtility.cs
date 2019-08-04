using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPickUtility : MonoBehaviour
{

    AI _parent;

    private void Awake()
    {
        _parent = gameObject.GetComponent<AI>();
    }

    public void UtilitySelect()
    {
        if (_parent.Utility.Count < 1)
        {//if no defence, do nothing
            _parent.PickAction();
            return;
        }

        _parent.UpdateLists();
        if (_parent.Enemies.Count < 1 && _parent.Friends.Count < 1)
            return;

        //Joke
        bool SomeoneIsTellingJoke = false;
        float Teamhealth = 0;
        foreach (GameObject go in _parent.Friends)
        {
            Teamhealth = Teamhealth + go.GetComponent<Cell>().GetHealth();

            if (go.GetComponentInChildren<JokeBuff>())
            {

                SomeoneIsTellingJoke = true;
            }
        }
        Teamhealth = Teamhealth / _parent.Friends.Count;

        if (Teamhealth < 95 && SomeoneIsTellingJoke == false)
        {
            foreach (GameObject go in _parent.Utility)
            {
                if (go.GetComponent<Joke>())
                {
                    _parent.chosenAbility = go;
                }
            }
        }
        else
        {
            int x = Random.Range(0, _parent.Utility.Count);
            _parent.chosenAbility = _parent.Utility[x];
            if (_parent.Utility.Count > 1)
            {
                while (_parent.chosenAbility.GetComponent<Joke>())
                {
                    int y = Random.Range(0, _parent.Utility.Count);
                    _parent.chosenAbility = _parent.Utility[y];
                }
            }
        }

        //    isbusy = true;
        if (_parent.chosenAbility != null)
        {
            _parent._GameObjectData.AbilityPrefab = _parent.chosenAbility;
            _parent.Cost = _parent.chosenAbility.GetComponent<Ability>().GetCost();
            _parent.TargetSelect();
        }

    }
}
