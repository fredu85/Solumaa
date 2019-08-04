using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAllyInfoSingularUI : MonoBehaviour
{
    public GameObject AllyCell;

    public GameObject AllyName;
    public GameObject AllyAbilityImage;
    public GameObject AllyHealth;
    public GameObject AllyTarget;
    public GameObject Progress;
    public Slider Progressslider;



    private void Update()
    {
        if (AllyCell == null)
        {
            Destroy(gameObject);
            return;
        }


        //Most of this code only works for AI allies
        //player allies will need updated version

        //AllyName
        AllyName.GetComponent<TextMeshProUGUI>().text = AllyCell.GetComponent<AI>().name;
        //AllyAbilityImage
        if (AllyCell.GetComponent<AI>().chosenAbility)
            AllyAbilityImage.GetComponent<Image>().overrideSprite = AllyCell.GetComponent<AI>().chosenAbility.GetComponent<Ability>().picture;
        else
            AllyAbilityImage.GetComponent<Image>().overrideSprite = null;
        //AllyHealth
        AllyHealth.GetComponent<TextMeshProUGUI>().text = (AllyCell.GetComponent<Cell>().GetHealth().ToString() + "/" +
                                                                       AllyCell.GetComponent<Cell>().GetMaxHealth().ToString());
        //AllyTarget
        if(AllyCell.GetComponent<AI>().GetTarget())
        AllyTarget.GetComponent<TextMeshProUGUI>().text = AllyCell.GetComponent<AI>().GetTarget().GetComponent<Cell>().CellName;

        //AllyProgress
        if (AllyCell.GetComponent<AI>().chosenAbility)
        {
            Progress.SetActive(true);
            Progressslider.maxValue = AllyCell.GetComponent<AI>().chosenAbility.GetComponent<Ability>().GetCost();
            Progressslider.value = AllyCell.GetComponent<AI>().Paid;
        }
        else
            Progress.SetActive(false);

    }
}
