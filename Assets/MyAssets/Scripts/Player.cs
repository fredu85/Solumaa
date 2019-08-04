using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : Cell
{
    public int MaxEnergy = 1000;
    //int[] gemGoals = { 1000, 2500, 5000, 10000 };

    public GameEvent PlayerDead;
    public GameEvent HasWalkedEvent;
    public LayerMask mask;

    //public GameObject bFire;
    //public GameObject gFire;

    public bool CanStartChannel = false;

    public GameObject Arms;

    public ChosenAbility PlayerChosenAbility;
    //public GameEvent OnTick;



    //#region Join The tick thingy
    void OnEnable()
    {
        Players.Add(this.gameObject);
    }
    //    GameManager.OnTick += RecieveTick;
    //}

    void OnDisable()
    {
        Players.Remove(gameObject);
    }

        //    GameManager.OnTick -= RecieveTick;
        //}
        //#endregion

        // Start is called before the first frame update
        void Start()
    {
        _GameObjectData = new GameObjectData();
        ThisCellObject.MaxHealth = 100;
        ThisCellObject.Health = ThisCellObject.MaxHealth;
        ThisCellObject.Gems = 50;
        ThisCellObject.Energy = GetEnergy();
        ThisCellObject.Cost = 0;
        ThisCellObject.Paid = 0;
        ThisCellObject.Steps = 900;
        ThisCellObject.Pos = transform.position;
        ThisCellObject.IsFiring = false;
        ThisCellObject.IsAlive = true;
        ThisCellObject.CanSelectTarget = true;
        Time.timeScale = 1;
        PlayerChosenAbility.chosenAbility = null;
       // chosenAbility.chosenAbility = null;

        UpdateLists();
    }

    // Update is called once per frame
    void Update()
    {
       // SetHealth(ThisCellObject.Health);

        UpdateColor();

        if(ThisCellObject.Health <= 0)
        {
            PlayerDead.Raise();
        }
        CheckifisTarget(); //display visual effect if mouseover target is player
     
        TargetCircle();  //visual effect around mouseover targets

        if (Input.GetButtonDown("Fire1"))
        {


            //if (!CanSelectTarget)
            //    return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
              //  if (!chosenAbility.chosenAbility)
              if(!chosenAbility)
                    return;

                //  if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Friend)) //check if its a healing ablility
                if(chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Equals(Ability.AbilityTargetEnum.Friend)) )
                {
                    //check if they object clicked on is on the same team (cant heal enemy)
                    if (hit.collider.gameObject.GetComponent<Cell>().GetTeam() == gameObject.GetComponent<Cell>().GetTeam())
                    {
                        SetTarget( hit.collider.gameObject);
                        _GameObjectData.Target = hit.collider.gameObject;

                        return;
                        
                    }

                }
                // if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Enemy)) //check if its a damage ablility
                if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Equals(Ability.AbilityTargetEnum.Enemy)))
                {
                    if (!hit.collider.gameObject.GetComponent<Cell>())
                        return;
                    //check if they object clicked on is on the same team (cant damage friendlies)
                    if (hit.collider.gameObject.GetComponent<Cell>().GetTeam() != gameObject.GetComponent<Cell>().GetTeam())
                    {
                       
                        SetTarget(hit.collider.gameObject);
                        _GameObjectData.Target = hit.collider.gameObject;
                            return;
                        
                        
                    }

                }

            }

        }
    }

    public void RecieveTick()
    {
        AddEnergy();
       // GemProgresstick();
        CheckAndChargeAbility();

        if (ThisCellObject.Paid != ThisCellObject.Cost && ThisCellObject.Cost > 0)
            return;

        //if (Time.fixedTime >= targetvfxtimer)
        //{
        //   // PingTargets();

        //    targetvfxtimer = Time.fixedTime + 1.0f;
        //}

    }

    void AddEnergy()
    {
        SetEnergy(GetEnergy() + 1);
        ThisCellObject.Energy = GetEnergy();
    }

    void CheckAndChargeAbility()
    {
        //if no ability
        //if (chosenAbility.chosenAbility == null)
        if (chosenAbility== null)
        {
            if(PlayerChosenAbility.chosenAbility)
            {
                chosenAbility = PlayerChosenAbility.chosenAbility;
                PlayerChosenAbility.chosenAbility = null;
            }
            return;
        }

       //if player has chosen an ability start charging it
            //if (chosenAbility.GetAbilityPayMethod().Equals( Ability.AbilityPayEnum.Channel))
            //{
            //    if (CanStartChannel)
            //        Channeling();
            //    else
            //        return;
            //}
            //else if (chosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Charge))
            //{
            //    Charge();
            //}

        if(chosenAbility.GetComponent<Ability>().PayMethod.Equals(Ability.AbilityPayEnum.Charge))
        {
            Charge();
        }
        
    }

    void Charge()
    {

        //if player has more energy stored than the ability ThisCellObject.Costs fire immidiately;
        if (GetEnergy()-ThisCellObject.Cost >0 && ThisCellObject.Paid < ThisCellObject.Cost)
        {
            //if (firing)
            //    return;

            //    SetEnergy(GetEnergy() - chosenAbility.GetCost());
            SetEnergy(GetEnergy() - chosenAbility.GetComponent<Ability>().GetCost());
            ThisCellObject.Paid = ThisCellObject.Cost;
            
          //  StartCoroutine(ArmsAnimation());
        }
        else
        {//if player has less energy than required it will slowly charge
            while (GetEnergy()>0 && ThisCellObject.Paid<ThisCellObject.Cost)
            {
                SetEnergy(GetEnergy()-1);
                ThisCellObject.Paid++;
            }
          
        }
    }

    //IEnumerator ArmsAnimation()
    //{
    //    ThisCellObject.IsFiring = true;
    //    Arms.GetComponent<Arms>().Clap();
    //    yield return new WaitForSeconds(0.8f);
    //    pFire();
    //    PlayUseAbilitySound();
    //    yield return new WaitForSeconds(0.1f);
    //    ThisCellObject.IsFiring = false;
    //}

    //private void pFire()
    //{
    //    if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Self))
    //        chosenAbility.SetTarget(this.gameObject);

    //    //Instantiate Current ability and go to <Ability> to set its target
    //    //  GameObject ability;
    //    //  _GameObjectData = new GameObjectData();

    //    _GameObjectData.Target = GetTarget();
    //    _GameObjectData.AbilityPrefab = chosenAbility.chosenAbility;
    //    _GameObjectData.StartLocation = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //    _GameObjectData.Sender = gameObject;
    //    _GameObjectData.TeamColor = TeamColor;
    //    _GameObjectData.TeamInt = GetTeam();
    //    _gameObjectDataEvent.Raise(_GameObjectData);

    //  //  Vector3 spanwpoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //  //  ability = Instantiate(chosenAbility.chosenAbility, spanwpoint, Quaternion.identity);
    //  //  ability.GetComponent<AbilityBase>().TeamColor = TeamColor;
    //  ////ability.GetComponent<AbilityBase>().SetTarget(GetTarget());
    //  //  ability.GetComponent<AbilityBase>().SetSender(gameObject);
    //  //  ability.GetComponent<AbilityBase>().SetTeam(GetTeam());

    //    if (chosenAbility.GetAbilityPayMethod().Equals(Ability.AbilityPayEnum.Charge))
    //    {
    //        ResetAbility();
    //    }
    //}


    public void Fire()
    {
        if (isFiring)
            return;

        _GameObjectData.AbilityPrefab = PlayerChosenAbility.chosenAbility;
        _GameObjectData.Target = PlayerChosenAbility.GetTarget();


        GetComponent<Cell>().fireScript.GetComponent<FireAbility>().Fire(_GameObjectData);
        //  ResetAbility();
        chosenAbility = null;
        _GameObjectData = new GameObjectData();
       // StartCoroutine(ArmsAnimation());
    }
   
    void Channeling()
    {
       // bFire.SetActive(true);
        if (GetTarget() == null)
        {
            return;
        }

        //   
        if (GetEnergy() - chosenAbility.GetComponent<AbilityBase>().GetDamage() > 0)
           
        {
            SetEnergy(GetEnergy() - chosenAbility.GetComponent<AbilityBase>().GetDamage());
          //  SetEnergy(GetEnergy() - chosenAbility.GetDamage());
          //  ThisCellObject.Paid = chosenAbility.GetDamage() + ThisCellObject.Paid;
            ThisCellObject.Paid = chosenAbility.GetComponent<AbilityBase>().GetDamage() + ThisCellObject.Paid;
            GameObject ability;
            Vector3 spanwpoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //    ability = Instantiate(chosenAbility.chosenAbility, spanwpoint, Quaternion.identity);
            ability = Instantiate(chosenAbility, spanwpoint, Quaternion.identity);
            ability.GetComponent<AbilityBase>().SetTarget(GetTarget());
            ability.GetComponent<AbilityBase>().SetSender(gameObject);
        }

        if (ThisCellObject.Paid >= ThisCellObject.Cost && ThisCellObject.Cost >0)
        {
          //  Debug.Log("Stop Channel");
            CanStartChannel = false;
         //   ResetAbility();
        }
    }

    //void ResetAbility()
    //{
    //    Debug.Log("Reset ThisCellObject.Cost, ThisCellObject.Paid, ability and target and disable fire button");
    //    chosenAbility= null;
    //  //  chosenAbility.chosenAbility = null;
    //    SetTarget(null);

    //    ThisCellObject.Cost = 0;
    //    ThisCellObject.Paid = 0;
    //}


    //public void StartAbility(GameObject a)
    //{
    //    ResetTarget();
    //    //Call this when ability is chosen.
    //    Debug.Log("StartAbility"+chosenAbility);
    //    //gets ThisCellObject.Cost of that ability

    //    //ThisCellObject.Cost = chosenAbility.GetCost();
    //    //chosenAbility.chosenAbility = a;
    //    ThisCellObject.Cost = chosenAbility.GetComponent<AbilityBase>().GetCost();
    //    chosenAbility = a;
    //}

    public void ResetTarget()
    {
        SetTarget(null);
        ThisCellObject.HasTarget = false;
    }

    public bool PlayerHasTarget() //returns true or false depending on target value
    {
        if (GetTarget() != null)
            return true;
        else
            return false;
    }


 

    //for the projectileRayCast
    void CheckMouseOverTarget(RaycastHit hit )
    {
        if (hit.collider.gameObject.GetComponent<AbilityBase>())
        {
            hit.collider.gameObject.GetComponent<Ability>().bShowTravelPath = true;
            return;
        }
    }

    void TargetCircle()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if we dont have an ability loaded we simply display the VFX and return;
     //   if (chosenAbility.chosenAbility==null)
            if (chosenAbility == null)
            {
            ////check whatever you mouseover
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                CheckMouseOverTarget(hit);

                if (hit.collider.gameObject.GetComponent<Cell>())
                    hit.collider.gameObject.GetComponent<Cell>().SetisCurrentTarget(true);

            }
            return;
        }

        //else we have to check what type of ability we are about to use:
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && chosenAbility)
        {
            //check to make sure we cant accidentally target wrong target for the ability
         //   if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Friend)) //check if its a healing ablility
                if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Friend)) //check if its a healing ablility
                {
                CheckMouseOverTarget(hit);
                //check if they object clicked on is on the same team (cant heal enemy)
                if (!hit.collider.gameObject.GetComponent<Cell>())
                    return;
                if (hit.collider.gameObject.GetComponent<Cell>().GetTeam() == gameObject.GetComponent<Cell>().GetTeam())
                {
                    hit.collider.gameObject.GetComponent<Cell>().SetisCurrentTarget(true);
                }
            
                return;
            }

           // if (chosenAbility.GetAbilityTargets().Equals(Ability.AbilityTargetEnum.Enemy)) //check if its a healing ablility
                if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Enemy)) //check if its a healing ablility
                {
                CheckMouseOverTarget(hit);
                //check if they object clicked on is on the same team (cant damage friendlies)
                if (!hit.collider.gameObject.GetComponent<Cell>())
                    return;
                if (hit.collider.gameObject.GetComponent<Cell>().GetTeam() != gameObject.GetComponent<Cell>().GetTeam())
                {
                    hit.collider.gameObject.GetComponent<Cell>().SetisCurrentTarget(true);
                }
                return;
            }

        }

       
    }

    public void SetNewAbility()
    {
        chosenAbility = PlayerChosenAbility.chosenAbility;
        ThisCellObject.Cost=chosenAbility.GetComponent<Ability>().GetCost();
        ThisCellObject.Paid = 0;
    }

    //void GemProgresstick()
    //{
    //    ThisCellObject.Steps++;
    //    if (ThisCellObject.Steps % 10 == 0)
    //    {
 
    //    }

    //    if (ThisCellObject.Steps == gemGoals[0])
    //    {
    //        GemComplete(10);
    //    }
    //    else if (ThisCellObject.Steps == gemGoals[1])
    //    {
    //        GemComplete(15);
    //    }
    //    else if (ThisCellObject.Steps == gemGoals[2])
    //    {
    //        GemComplete(20);
    //    }
    //    else if (ThisCellObject.Steps == gemGoals[3])
    //    {
    //        GemComplete(25);
    //    }
    //}

    //void GemComplete(int gems)
    //{
    //    ThisCellObject.Gems = ThisCellObject.Gems + gems;
    //    HasWalkedEvent.Raise();
    //}
   
    public void CanSelectTarget()
    {
        ThisCellObject.CanSelectTarget = true;
    }
}





