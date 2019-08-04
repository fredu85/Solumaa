using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsUI : UIInheritenceStart
{
    List<GameObject> attacks = new List<GameObject>();
    List<GameObject> defences= new List<GameObject>();
    List<GameObject> utilities = new List<GameObject>();
    public Button skillsbutton;
    public Button fire;
    public GameObject[] parentpanel;
    public Canvas thiscanvas;
    public Canvas nextcanvas; // what canvas you open when this closes
    public Canvas Overviewcanvas;
    public GameObject chosenAbility;
    public Canvas IsChargingAbilityErrorCanvas;
    public GameObject UIBase;
  //  GameObject player;

   // public GameObject CM;
   // public GameObject getback;
    public GameObject CameraManager;
    //[SerializeField]
    //GameObject GameManager;

    public Canvas description;
    public Image descriptionImage;
    public Image FireImage;
    public TextMeshProUGUI descriptionAbilityName;
    public TextMeshProUGUI descriptionAbilityCost;
    public TextMeshProUGUI descriptionAbilityDescription;
    public TextMeshProUGUI descriptionAbilityAmount;

    public ObjectList StartingAbilities;
    public GameEvent NewAbility;


    private void Awake()
    {
        //if (GameManager == null)
        //{
        //    GameManager = GameObject.Find("GameManager");
        //}

        //Fetch all the starting Abilities
        //  foreach (GameObject ability in GameManager.GetComponent<GameManager>().StartingAbilities)
        foreach (GameObject ability in StartingAbilities.GetItems())
        {
            if (ability.GetComponent<AbilityBase>().Type.Equals(AbilityBase.AbilityTypeEnum.Attack))
            {
                attacks.Add(ability);
            }
            if (ability.GetComponent<AbilityBase>().Type.Equals(AbilityBase.AbilityTypeEnum.Defend))
            {
                defences.Add(ability);
            }
            if (ability.GetComponent<AbilityBase>().Type.Equals(AbilityBase.AbilityTypeEnum.Utility))
            {
                utilities.Add(ability);
            }
        }
        //for (int i = 0; i < parentpanel.Length; i++)
        //{
        foreach (GameObject go in attacks)
            {
                Button newbutton = Instantiate(skillsbutton);
                newbutton.transform.SetParent(parentpanel[0].transform,false); //unity wanted to use SetParent so I did xD
                newbutton.image.sprite = go.GetComponent<Ability>().picture;
                newbutton.onClick.AddListener(delegate { SelectAbility(newbutton); });
                newbutton.GetComponent<SkillsButtonScript>().skillsUI = this.gameObject;
                newbutton.GetComponent<SkillsButtonScript>().ability = go;
                newbutton.GetComponent<SkillsButtonScript>().description = description.GetComponentInChildren<Canvas>();
                newbutton.GetComponent<SkillsButtonScript>().descriptionImage= descriptionImage;
                newbutton.GetComponent<SkillsButtonScript>().FireImage= FireImage;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityName = descriptionAbilityName;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityCost = descriptionAbilityCost;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityDescription = descriptionAbilityDescription;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityAmount = descriptionAbilityAmount;


            }
            foreach (GameObject go in defences)
            {
                Button newbutton = Instantiate(skillsbutton);
                newbutton.transform.SetParent(parentpanel[1].transform, false); //unity wanted to use SetParent so I did xD
                newbutton.image.sprite = go.GetComponent<Ability>().picture;
                newbutton.onClick.AddListener(delegate { SelectAbility(newbutton); });
                newbutton.GetComponent<SkillsButtonScript>().skillsUI = this.gameObject;
                newbutton.GetComponent<SkillsButtonScript>().ability = go;
                newbutton.GetComponent<SkillsButtonScript>().description = description.GetComponentInChildren<Canvas>();
                newbutton.GetComponent<SkillsButtonScript>().descriptionImage = descriptionImage;
                newbutton.GetComponent<SkillsButtonScript>().FireImage = FireImage;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityName = descriptionAbilityName;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityCost = descriptionAbilityCost;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityDescription = descriptionAbilityDescription;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityAmount = descriptionAbilityAmount;


        }
            foreach (GameObject go in utilities)
            {
                Button newbutton = Instantiate(skillsbutton);
                newbutton.transform.SetParent(parentpanel[2].transform, false); //unity wanted to use SetParent so I did xD
                newbutton.image.sprite = go.GetComponent<Ability>().picture;
            // newbutton.onClick.AddListener(delegate { Click(newbutton); });
                newbutton.onClick.AddListener(delegate { SelectAbility(newbutton); });
                newbutton.GetComponent<SkillsButtonScript>().skillsUI = this.gameObject;
                newbutton.GetComponent<SkillsButtonScript>().ability = go;
                newbutton.GetComponent<SkillsButtonScript>().description = description.GetComponentInChildren<Canvas>();
                newbutton.GetComponent<SkillsButtonScript>().descriptionImage = descriptionImage;
                newbutton.GetComponent<SkillsButtonScript>().FireImage = FireImage;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityName = descriptionAbilityName;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityCost = descriptionAbilityCost;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityDescription = descriptionAbilityDescription;
                newbutton.GetComponent<SkillsButtonScript>().descriptionAbilityAmount = descriptionAbilityAmount;


            }
       // }
    }



    public void Update()
    {

        if (chosenAbility == null)
        {
            fire.interactable = false;
            FireImage.enabled = false;
        }
        else
        {
            fire.interactable = true;
            FireImage.enabled = true;
        }
    }


    public void ConfirmAbility()
    {
        if (chosenAbility == null)
            return;

        if (PlayerChosenAbility.chosenAbility)
        {
            IsChargingAbilityErrorCanvas.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            StartAbility();
            UIBase.GetComponent<UIBase>().EnableCanvas(Overviewcanvas);
            UIBase.GetComponent<UIBase>().ClickSound();
            //AudioManager.instance.Play("UIClick");
        }
    }

    public void back()
    {
        Time.timeScale = 1;
    }

    public void DeleteProgressStartAbility()
    {
        PlayerObject.Cost = 0;
        PlayerObject.Paid = 0;
        Time.timeScale = 1;
        StartAbility();
    }

    public void StartAbility()
    {

        // PlayerObject.Player.GetComponent<Player>().StartAbility(chosenAbility);

        //CameraManager.GetComponent<CameraManager>().CameraCanMove = true;
        //CameraManager.GetComponent<CameraManager>().CameraMoveBack();

        PlayerChosenAbility.chosenAbility = chosenAbility;
      //  PlayerObject.Cost = PlayerChosenAbility.GetCost();
        UIBase.GetComponent<UIBase>().ClickSound();

        //Overviewcanvas.enabled = true;
        //thiscanvas.enabled = false;
        UIBase.GetComponent<UIBase>().EnableCanvas(Overviewcanvas);
        chosenAbility = null;
        NewAbility.Raise();

    }

    public void SelectAbility(Button b)
    {
        UIBase.GetComponent<UIBase>().ClickSound();
        chosenAbility = (b.GetComponent<SkillsButtonScript>().ability);
            FireImage.overrideSprite = b.GetComponent<SkillsButtonScript>().ability.GetComponent<Ability>().picture;
    }
}
