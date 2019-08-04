using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject player;
    public GameObject attack;
    public GameObject defend;
    public TextMeshProUGUI EnergyText;
    public TextMeshProUGUI AbilityText;
    public TextMeshProUGUI AbilityCostPaidText;

    public ChosenAbility PlayerChosenAbility;

    #region Join The tick thingy
    void OnEnable()
    {
        GameManager.OnTick += ontick;
    }

    void OnDisable()
    {
        GameManager.OnTick -= ontick;
    }
    #endregion

    void ontick()
    {
        CurrentEnergy();
        AbilityCostPaid();
        AbilityTextset();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetAbilitydef()
    {
        PlayerChosenAbility.chosenAbility = defend;
        AbilityTextset();


    }

    public void SetAbilityatk()
    {
        PlayerChosenAbility.chosenAbility = attack;
        AbilityTextset();
    }

    public void CurrentEnergy()
    {

        EnergyText.text = (player.GetComponent<Player>().GetEnergy()).ToString();
    }

    public void AbilityCostPaid()
    {
        if (PlayerChosenAbility.chosenAbility != null)
           AbilityCostPaidText.text = player.GetComponent<Player>().Paid.ToString() + " / " + PlayerChosenAbility.GetCost();
        else
            AbilityCostPaidText.text = "";
    }

    public void AbilityTextset()
    {
        if (PlayerChosenAbility.chosenAbility != null)
            AbilityText.text = PlayerChosenAbility.chosenAbility.GetComponent<Ability>().GetName();
        else
            AbilityText.text = "";
    }
}
