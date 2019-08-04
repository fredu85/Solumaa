using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : UIInheritenceStart
{
    Player playerScript;
    public Slider HealtSlider;
    public GameObject slider;
    public GameObject FireButton;
    public Canvas Overview;
    public Canvas Inspection;
    public Canvas PlayerView;

    //public void PlayerGemfire()
    //{
    //    Debug.Log("PlayerGemFire()");
    //    PlayerObject.
    //}


    public void FireButtonFunct()
    {
        if (PlayerChosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Self))
        {
            player.GetComponent<Player>().SetTarget(player);
            player.GetComponent<Cell>()._GameObjectData.Target = player;
            player.GetComponent<Player>().Fire();
        }
        Inspection.enabled = false;
        PlayerView.enabled = false;
        Overview.enabled = true;

    }
}
