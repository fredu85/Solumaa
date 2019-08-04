using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : UIInheritenceStart
{
    public GameObject PlayerHealth;
    public GameObject PlayerEnergy;
    public GameObject playerGems;

    public Slider HealtSlider;
    public GameObject slider;
    public GameObject GemUse;
    public GameObject FireButton;

    public GameObject SliderAbilityName;
    public Image SliderAbilityImage;

    // Update is called once per frame
    void Update()
    {
        #region ResourcesUpdate
        //Health
        PlayerHealth.GetComponent<TextMeshProUGUI>().text = PlayerObject.Health.ToString() + "/" + PlayerObject.MaxHealth.ToString();
        //Energy
        PlayerEnergy.GetComponent<TextMeshProUGUI>().text = PlayerObject.Energy.ToString();
        //Gems
        playerGems.GetComponent<TextMeshProUGUI>().text = PlayerObject.Gems.ToString();


        #endregion

        if (PlayerChosenAbility.chosenAbility == null)
        {
            slider.SetActive(false);
            FireButton.SetActive(false);
            return;
        }

        if (GemUse.activeInHierarchy)
        {//update gem cost if the object is active
            if ((PlayerChosenAbility.GetCost() - PlayerObject.Paid / 10) == 0)
                GemUse.GetComponentInChildren<TextMeshProUGUI>().text = 1.ToString();
            else
            {
                GemUse.GetComponentInChildren<TextMeshProUGUI>().text =( (
                      (PlayerChosenAbility.GetCost() - PlayerObject.Paid) / 10).ToString());
            }
        }

        //set the correct values for the charge bar picture and text
        SliderAbilityName.GetComponent<TextMeshProUGUI>().text = PlayerChosenAbility.GetName();

        SliderAbilityImage.overrideSprite = PlayerChosenAbility.GetSprite();
        slider.SetActive(true);

        //TODO write this in a smarter way!
        //check if has been paid
        if (PlayerObject.Cost == PlayerObject.Paid &&
                //make sure one ability has been selected
                PlayerChosenAbility.chosenAbility != null &&
                //only show fire button if the ability is targetting the player
                PlayerChosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Self))
        {
            FireButton.SetActive(true);
        }
        else
            FireButton.SetActive(false);



        if (PlayerObject.Paid < PlayerObject.Cost)
        { //if we have enough gems and previous conditions are false, show the gem fire button
            if (PlayerObject.Gems * 10 + PlayerObject.Paid >= PlayerObject.Cost)
            {
                GemUse.SetActive(true);
            }
            else
                GemUse.SetActive(false);
        }
        else
            GemUse.SetActive(false);

        HealtSlider.maxValue = PlayerObject.Cost;
        HealtSlider.value = PlayerObject.Paid;

        if (HealtSlider.maxValue >= 0)
            {
                slider.SetActive(true);
            }
            else
                slider.SetActive(false);
    }

    public void PlayerGemfire()
    {
            PlayerObject.Gems = PlayerObject.Gems - (int)(Mathf.Clamp(((PlayerObject.Cost - PlayerObject.Paid) / 10), 1, Mathf.Infinity));
            PlayerObject.Paid = PlayerObject.Cost;
    }



    //public void PlayerFire()
    //{
    //    player.GetComponent<Player>().Fire();
    //}
}
