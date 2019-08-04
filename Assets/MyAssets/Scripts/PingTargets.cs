using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingTargets : MonoBehaviour
{
    public ObjectList Players;
    public CellObject PlayerObject;
    GameObject chosenAbility;
    GameObject player;
    float targetvfxtimer = 1.0f;
    private void Awake()
    {
        player = GetComponent<Player>().gameObject;
    }

    public void OnTick()
    {
       chosenAbility= GetComponent<Player>().chosenAbility;
        if (Time.fixedTime >= targetvfxtimer)
        {
             Ping();

            targetvfxtimer = Time.fixedTime + 1.0f;
        }
    }

    private void Ping()
    {
        //    if (chosenAbility.chosenAbility == null)//dont ping if no ability chosen
        if (chosenAbility == null)//dont ping if no ability chosen
            return;
        if (PlayerObject.Cost != PlayerObject.Paid)
            return;
       
        if (chosenAbility.GetComponent<Ability>().PayMethod.Equals(Ability.AbilityPayEnum.Charge))
        {

            if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Friend))
            {
                foreach (GameObject go in Players.GetItems())
                {
                    if (go.GetComponent<Cell>().GetTeam() == player.GetComponent<Cell>().GetTeam())
                        go.GetComponent<Cell>().SetisCurrentTarget(true);
                }
            }
            else if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Enemy))
            {
                foreach (GameObject go in Players.GetItems())
                {
                    if (go.GetComponent<Cell>().GetTeam() != player.GetComponent<Cell>().GetTeam())
                        go.GetComponent<Cell>().SetisCurrentTarget(true);
                }
            }
            else if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Self))
            {
                gameObject.GetComponent<Cell>().SetisCurrentTarget(true);
            }

        }
    }


    //ONLY if Channel once again becomes useful
    //if (chosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Channel))
    //{//channeling ability
    //    if (GetComponent<Player>().ThisCellObject.Paid == 0)
    //    {
    //        if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Friend))
    //        {
    //            foreach (GameObject go in Players.Items)
    //            {
    //                if (go.GetComponent<Cell>().GetTeam() == GetTeam())
    //                    go.GetComponent<Cell>().SetisCurrentTarget(true);
    //            }
    //        }
    //        else if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Enemy))
    //        {
    //            foreach (GameObject go in Players.Items)
    //            {
    //                if (go.GetComponent<Cell>().GetTeam() != GetTeam())
    //                    go.GetComponent<Cell>().SetisCurrentTarget(true);
    //            }
    //        }
    //    }

    //}
    //if (chosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Charge))
    //{

    //    if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Friend))
    //        {
    //            foreach (GameObject go in Players.GetItems())
    //            {
    //                if (go.GetComponent<Cell>().GetTeam() == GetTeam())
    //                    go.GetComponent<Cell>().SetisCurrentTarget(true);
    //            }
    //        }
    //        else if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Enemy))
    //        {
    //            foreach (GameObject go in Players.GetItems())
    //            {
    //                if (go.GetComponent<Cell>().GetTeam() != GetTeam())
    //                    go.GetComponent<Cell>().SetisCurrentTarget(true);
    //            }
    //        }
    //        else if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Self))
    //        {
    //            gameObject.GetComponent<Cell>().SetisCurrentTarget(true);
    //        }

    //}
}
