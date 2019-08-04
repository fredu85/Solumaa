using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : MonoBehaviour
{
    GameObjectData _GameObjectData = new GameObjectData();
    GameObject arms;
    private void Awake()
    {
        arms =GetComponentInChildren<Arms>().gameObject;
        arms.SetActive(false);
    }
    public void Fire(GameObjectData data)
    {
        _GameObjectData = data;
        StartCoroutine(ArmsAnimation());
    }

   IEnumerator ArmsAnimation()
    {
        GetComponent<Cell>().isFiring = true;
        arms.GetComponent<Arms>().Clap();
        yield return new WaitForSeconds(0.8f);
        ActivatePrefab();
        GetComponent<Cell>().PlayUseAbilitySound();
        yield return new WaitForSeconds(0.1f);
        GetComponent<Cell>().isFiring = false;
    }

    private void ActivatePrefab()
    {
        //ChosenAbility chosenAbility = GetComponent<Cell>().chosenAbility;
        //if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Self))
        //    chosenAbility.SetTarget(this.gameObject);
        //  GameObject chosenAbility = GetComponent<Cell>().chosenAbility;
        GameObject chosenAbility = _GameObjectData.AbilityPrefab;
        if(chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Self))
        {
            chosenAbility.GetComponent<AbilityBase>().SetTarget(this.gameObject);
        }

        //Instantiate Current ability and go to <Ability> to set its target
        //  GameObject ability;
        //GameObjectData _GameObjectData= GetComponent<Cell>()._GameObjectData;
        GameObjectDataEvent _gameObjectDataEvent = GetComponent<Cell>()._gameObjectDataEvent;

       // _GameObjectData.Target = gameObject.GetComponent<Cell>().GetTarget();
      //  _GameObjectData.AbilityPrefab = chosenAbility;
        _GameObjectData.StartLocation = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        _GameObjectData.Sender = gameObject;
        _GameObjectData.TeamColor = GetComponent<Cell>().TeamColor;
        _GameObjectData.TeamInt = GetComponent<Cell>().GetTeam();
        _gameObjectDataEvent.Raise(_GameObjectData);

        //  Vector3 spanwpoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //  ability = Instantiate(chosenAbility.chosenAbility, spanwpoint, Quaternion.identity);
        //  ability.GetComponent<AbilityBase>().TeamColor = TeamColor;
        ////ability.GetComponent<AbilityBase>().SetTarget(GetTarget());
        //  ability.GetComponent<AbilityBase>().SetSender(gameObject);
        //  ability.GetComponent<AbilityBase>().SetTeam(GetTeam());

        GetComponent<Cell>().chosenAbility= null;
        GetComponent<Cell>().SetTarget(null);

        GetComponent<Cell>().Cost = 0;
        GetComponent<Cell>().Paid = 0;

        //if (chosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Charge))
        //{
        //    chosenAbility.chosenAbility = null;
        //    GetComponent<Cell>().SetTarget(null);

        //    GetComponent<Cell>().Cost = 0;
        //    GetComponent<Cell>().Paid = 0;
        //}
    }

}
