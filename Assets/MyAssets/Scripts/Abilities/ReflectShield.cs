using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectShield : Shield
{
    //Modified from normal shield
    private new void OnTriggerEnter(Collider other)
    {
        //dont collide if absorbamount is less than 1
        if (AbsorbAmount < 1)
        {
            return;
        }
        //if not an ability dont collide
        if (other.gameObject.GetComponent<AbilityBase>() == null)
        {
            return;
        }
        //can only hit enemy projectiles
        if (other.gameObject.GetComponent<AbilityBase>().GetTeam() != GetTeam())
        {
            AbilityBase tempAbilityBase = other.gameObject.GetComponent<AbilityBase>();
            tempAbilityBase.SetTeam(GetTeam());

            //Check for if projectile does not have a sender
            if (tempAbilityBase.GetSender() == null)
                other.gameObject.SetActive(false);
            else
            {
                //send projectile back to sender, with replaced sender and target
                GameObject tempTarget = tempAbilityBase.GetSender();
                tempAbilityBase.SetSender(tempAbilityBase.GetTarget());
                tempAbilityBase.SetTarget(tempTarget);
            }

            AbsorbAmount--;
            weakenShield();//Set how transparent the shield should be based on how many charges it has left

        }
    }
}
