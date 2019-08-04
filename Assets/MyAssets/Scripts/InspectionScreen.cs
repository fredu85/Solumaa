using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InspectionScreen : UIInheritenceStart
{
    [SerializeField]
    GameObject InspectTarget1;
    public GameObject CameraManager;

    public GameObject Confirm;

    public GameObject TargetInfo;
    public GameObject TargetName;
    public GameObject TargetHealth;
    public GameObject TargetAction;
    public GameObject Slider;
    public Slider progressSlider;

    public GameEvent PlayerFireEvent;
    public CellObject Player;
    public CellObject InspectTarget;

    public GameEvent cameraMoveBack;

    // Update is called once per frame
    //void Update()
    //{
        //if (player == null)
        //   player= GameObject.FindGameObjectWithTag("Player");

        //if (player == null) //no player found, dont do anything else, if we get here means shit has gone very wrong
        //    return;

        //if (player.GetComponent<Player>().firing)
        //    Confirm.SetActive(false);
        //if (InspectTarget)
        //{
        //    TargetName.GetComponent<TextMeshProUGUI>().text = InspectTarget.Health.ToString();
        //}
        //the inspect panel for the target


        //if (InspectTarget.CompareTag("Player") && InspectTarget.GetComponent<Player>().chosenAbility)
        //{ //show progress for current ability in case the target is a player
        //   // progressSlider.maxValue = InspectTarget.GetComponent<Player>().chosenAbility.GetComponent<AbilityBase>().GetCost();
        //    progressSlider.value = InspectTarget.GetComponent<Player>().Paid; 

        //    if (InspectTarget.GetComponent<Player>().chosenAbility)
        //        TargetAction.GetComponent<TextMeshProUGUI>().SetText(InspectTarget.GetComponent<Player>().chosenAbility.ToString());
        //}
        //if (InspectTarget.CompareTag("AI") && InspectTarget.GetComponent<AI>().AIchosenAbility)
        //{//show progress for current ability in case the target is AI
        //    progressSlider.maxValue = InspectTarget.GetComponent<AI>().AIchosenAbility.GetComponent<AbilityBase>().GetCost();
        //    progressSlider.value = InspectTarget.GetComponent<AI>().Paid;

        //    if (InspectTarget.GetComponent<AI>().AIchosenAbility.GetComponent<AbilityBase>())
        //        TargetAction.GetComponent<TextMeshProUGUI>().SetText(InspectTarget.GetComponent<AI>().AIchosenAbility.GetComponent<AbilityBase>().AbilityName);
        //}

        //if (Player.IsFiring)
        //    return;

        //TargetName.GetComponent<TextMeshProUGUI>().SetText(InspectTarget.GetComponent<Cell>().CellName.ToString());
        //TargetHealth.GetComponent<TextMeshProUGUI>().SetText(InspectTarget.GetComponent<Cell>().GetHealth().ToString()
        //                                                     + "/" + InspectTarget.GetComponent<Cell>().GetMaxHealth().ToString());
        //if (InspectTarget.CompareTag("Player"))
        //{

        //}
        //if (InspectTarget.CompareTag("AI"))
        //{

        //}

  //  }

    public void Ontick()
    {
        EnableConfirm();
        
        TargetName.GetComponent<TextMeshProUGUI>().text = InspectTarget.CellName;
        TargetHealth.GetComponent<TextMeshProUGUI>().text = InspectTarget.Health.ToString();
        TargetAction.GetComponent<TextMeshProUGUI>().text = InspectTarget.CurrentAction;

        progressSlider.maxValue = InspectTarget.Cost;
        progressSlider.value = InspectTarget.Paid;
    }

    public void PlayerFire()
    {
        PlayerFireEvent.Raise();
    }

    public void CameraMoveBack()
    {
        cameraMoveBack.Raise();
    }
    public void EnableConfirm()
    {
        if (PlayerChosenAbility.chosenAbility == null)
            return;

        if (PlayerChosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Charge))
        {//if charge ability, check that it is paid for
            if (PlayerObject.Cost == PlayerObject.Paid
                && !PlayerObject.IsFiring)
                    Confirm.SetActive(true);
        }

        if (PlayerChosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Channel))
        { //if channel ability fire away
            if (player.GetComponent<Player>().PlayerHasTarget())
                Confirm.SetActive(true);
        }
    }

    public void InfoCanvasSetActive(GameObject t)
    {
        InspectTarget1 = t;
        TargetInfo.SetActive(true);
    }

}
