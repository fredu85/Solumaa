using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetOverviewUI : UIInheritenceStart
{
    public GameObject SelectTarget;

    // Update is called once per frame
    void Update()
    {
        //if (player == null)
        //    return;

        //if (player.GetComponent<Player>().chosenAbility == null)
        //{
        //    SelectTarget.SetActive(false);
        //    return;
        //}
        //if(player.GetComponent<Player>().firing)
        //{
        //    SelectTarget.SetActive(false);
        //    return;
       // }

        //if (player.GetComponent<Player>().chosenAbility.GetComponent<AbilityBase>().PayMethod.Equals(Ability.AbilityPayEnum.Channel))
        //{
        //    if (player.GetComponent<Player>().Paid == 0)
        //        SelectTarget.SetActive(true);
        //    else
        //        SelectTarget.SetActive(false);
        //}
        //else if (player.GetComponent<Player>().chosenAbility.GetComponent<AbilityBase>().PayMethod.Equals(Ability.AbilityPayEnum.Charge))
        //{
        //    if (player.GetComponent<Player>().Paid >= player.GetComponent<Player>().Cost)
        //    {
        //        SelectTarget.SetActive(true);
        //    }
        //}
        //else
        //    SelectTarget.SetActive(false);

    }
}
