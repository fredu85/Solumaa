using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillsButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject ability;
    public GameObject skillsUI;

    public Canvas description;

    public Image descriptionImage;
    public Image FireImage;

    public TextMeshProUGUI descriptionAbilityName;
    public TextMeshProUGUI descriptionAbilityCost;
    public TextMeshProUGUI descriptionAbilityDescription;
    public TextMeshProUGUI descriptionAbilityAmount;



    public void OnPointerEnter(PointerEventData eventData)
    {
       // skillsUI.GetComponent<SkillsUI>().chosenAbility = null;

        description.enabled = true;
  
        descriptionImage.overrideSprite = ability.GetComponent<Ability>().picture;
        descriptionAbilityName.text = ability.GetComponent<Ability>().GetName();
        descriptionAbilityCost.text = ability.GetComponent<Ability>().GetCost().ToString();
        descriptionAbilityDescription.text = ability.GetComponent<Ability>().GetDescription();

        if (ability.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Attack))
            descriptionAbilityAmount.text = "Damage: "+ability.GetComponent<AbilityBase>().GetDamage().ToString();
        else if(ability.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Defend))
        {
            if(ability.GetComponent<AbilityBase>().GetHeal() > 0)
            descriptionAbilityAmount.text = "Heal: " + ability.GetComponent<AbilityBase>().GetHeal().ToString();
            else
                descriptionAbilityAmount.text = " ";
        }
        else if(ability.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Utility))
        {
            descriptionAbilityAmount.text = " ";
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (skillsUI.GetComponent<SkillsUI>().chosenAbility != null)
        {//if an ability has been clicked on/selected, show that ability, othervise hide the ability
            description.enabled = true;

            //look at the skillsUI to get the on clicked ability then get the data from that chosen ability
            descriptionImage.overrideSprite = skillsUI.GetComponent<SkillsUI>().chosenAbility.GetComponent<Ability>().picture;
            descriptionAbilityName.text = skillsUI.GetComponent<SkillsUI>().chosenAbility.GetComponent<Ability>().GetName();
            descriptionAbilityCost.text = skillsUI.GetComponent<SkillsUI>().chosenAbility.GetComponent<Ability>().GetCost().ToString();
            descriptionAbilityDescription.text = skillsUI.GetComponent<SkillsUI>().chosenAbility.GetComponent<Ability>().GetDescription();
            descriptionAbilityAmount.text = " ";

        }
        else
        {
            description.enabled = false;
        }
    }


}
