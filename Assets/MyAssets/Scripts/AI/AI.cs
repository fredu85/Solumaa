using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Cell
{
    GameManager GameManager;

    private string[] AINames = { "Steven", "John", "Peter", "Maria", "Sofia", "Julia" };

    public List<GameObject> Attack = new List<GameObject>();
    public List<GameObject> Defence = new List<GameObject>();
    public List<GameObject> Utility = new List<GameObject>();

    public GameObject Arms;

    //personality
    public enum Personality
    {
        Aggressive, Defensive, Utility
    }
    Personality PersonalityEnum;

    //AI first choses which ability to use based on some criteria, then choses which target to 
    // attack based on another criteria
    //these criterias are different for each ability

    readonly int MaxEnergy = 1000;


    #region Join Events
    void OnEnable()
    {
        Players.Add(gameObject);
      //  GameManager.OnTick += RecieveTick;
    }

    void OnDisable()
    {
        Players.Remove(gameObject);
      //  GameManager.OnTick -= RecieveTick;
    }

    //private void OnDestroy()
    //{
    //    //Players.Remove(this.gameObject);
    //  //  GameManager.OnTick -= RecieveTick;
    //}
    #endregion


    private void Awake()
    {
        _GameObjectData = new GameObjectData();
        //set the AI personality
        SetPersonality();
        int rng = Random.Range(0, AINames.Length);
        CellName = AINames[rng];
        name = CellName;

        //Fetch all the starting Abilities
        GetSTartingAbilities();

    }

    private void SetPersonality()
        {
        int personality = Random.Range(0, 6);
        if (personality < 3)
        {
            PersonalityEnum = Personality.Aggressive;
        }
        else if (personality == 3)
        {
            PersonalityEnum = Personality.Utility;
        }
        else if (personality > 3)
        {
            PersonalityEnum = Personality.Defensive;
        }

    }

    private void GetSTartingAbilities()
    {
        foreach (GameObject ability in StartingAbilities.GetItems())
        {
            if (ability.GetComponent<AbilityBase>().Type.Equals(AbilityBase.AbilityTypeEnum.Attack))
            {
                Attack.Add(ability);
            }
            if (ability.GetComponent<AbilityBase>().Type.Equals(AbilityBase.AbilityTypeEnum.Defend))
            {
                Defence.Add(ability);
            }
            if (ability.GetComponent<AbilityBase>().Type.Equals(AbilityBase.AbilityTypeEnum.Utility))
            {
                Utility.Add(ability);
            }
        }
    }

    private void Start()
    {
        //get friends and enemies in their respective lists
        UpdateLists();
    }

    private void Update()
    {
        //Start the VFX bling bling if this is the mouseover target of the player
        CheckifisTarget();
    }


    public void RecieveTick()
    {
        if (gameObject != null)
        {

            if (GetEnergy() < MaxEnergy)
            {
                SetEnergy(GetEnergy() + 1);
            }
            CheckBusy();
        }

    }

    void CheckBusy()//checks if something is chosenly being charged, if not -> PickAction()
    {
        if (chosenAbility == null)
        {
            PickAction();
            return;
        }

        //Add channel here later if that becomes a thing again)
        if (chosenAbility.GetComponent<Ability>().PayMethod.Equals(Ability.AbilityPayEnum.Charge))
            Charge();


    }

    public void PickAction()//randomly pick Attack, Defend or Utility
    {
        UpdateLists(); //make sure friendlies and enemies exist

        int action;
        action = Random.Range(0, 100);
        //Random Roll and based on personality pick action
        if (PersonalityEnum == Personality.Aggressive)
        {
            if (action < 15)
                if (GetComponent<AIPickUtility>())
                    GetComponent<AIPickUtility>().UtilitySelect();


                else if (action > 14 && action < 30)
                    if (GetComponent<AIPickDefend>())
                           GetComponent<AIPickDefend>().Defendselect();

                    else
                       if (GetComponent<AIPickAttack>())
                            GetComponent<AIPickAttack>().Attackselect();
        }


        else if (PersonalityEnum == Personality.Defensive)
        {
            if (action < 15)
                if (GetComponent<AIPickUtility>())
                     GetComponent<AIPickUtility>().UtilitySelect();
    
                else if (action > 14 && action < 30)
                    if (GetComponent<AIPickAttack>())
                          GetComponent<AIPickAttack>().Attackselect();

                    else
                        GetComponent<AIPickDefend>().Defendselect();
        }


        else if (PersonalityEnum == Personality.Utility)
        {
            if (action < 15)
                if (GetComponent<AIPickAttack>())
                       GetComponent<AIPickAttack>().Attackselect();

                else if (action > 14 && action < 30)
                    if (GetComponent<AIPickDefend>())
                           GetComponent<AIPickDefend>().Defendselect();

                    else
                     if (GetComponent<AIPickUtility>())
                              GetComponent<AIPickUtility>().UtilitySelect();
        }


    }



    public void TargetSelect()
    {
        UpdateLists();
        if (Enemies.Count < 1)
            return;
        #region TargetEnemy
        if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(AbilityBase.AbilityTargetEnum.Enemy))
        {//abilities targeting enemies
            UpdateLists();
            //a list of enemies that are shielded
            List<GameObject> unshielded = new List<GameObject>();
            foreach (GameObject noshield in Enemies)
            {

                if (!noshield.GetComponentInChildren<Shield>() || !noshield.GetComponentInChildren<MegaShield>())
                {
                    unshielded.Add(noshield);
                }
            }

            //Burger - target enemy that is shielded
            if (chosenAbility.GetComponent<Burger>())
            {
                List<GameObject> shielded = new List<GameObject>();
                foreach (GameObject noshield in Enemies)
                {
                    if (noshield.GetComponentInChildren<Shield>() || noshield.GetComponentInChildren<MegaShield>())
                    {
                        shielded.Add(noshield);
                    }
                }

                //if no target has any shield shoot at random target
                if (shielded.Count == 0)
                {
                    int x;
                    x = Random.Range(0, Enemies.Count);
                    SetTarget(Enemies[x]);

                }
                else
                {//pick random target from those shielded
                    int x;
                    x = Random.Range(0, shielded.Count);
                    SetTarget(Enemies[x]);

                }

            }


            //Scientist
            else if (chosenAbility.GetComponent<Scientist>())
            {
                //if there are no open targets shoot randomly
                if (unshielded.Count == 0)
                {
                    int x;
                    x = Random.Range(0, unshielded.Count);
                    SetTarget(Enemies[x]);
                }
                else
                {
                    GameObject lowesthp = Enemies[0];
                    foreach (GameObject unshld in unshielded)
                    {
                        if (lowesthp.GetComponent<Cell>().GetHealth() > unshld.GetComponent<Cell>().GetHealth())
                        {
                            lowesthp = unshld;
                        }
                    }//target the lowest hp enemy
                    SetTarget(lowesthp);
                }
            }


            //Cigarette
            else if (chosenAbility.GetComponent<Cigarette>())
            {
                if (unshielded.Count == 0)
                {
                    int x;
                    x = Random.Range(0, unshielded.Count);
                    SetTarget(Enemies[x]);
                }
                else
                {
                    int x;
                    x = Random.Range(0, unshielded.Count);
                    SetTarget(unshielded[x]);
                }
            }


            else
            {// no specific assignment
                int x;
                x = Random.Range(0, Enemies.Count);
                SetTarget(Enemies[x]);
            }
        }
        #endregion

        #region TargetAlly

        else if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Friend))
        {//abilities targeting friendlies, AIs cannot target themselves
            UpdateLists();
            if (Friends.Count < 1)
                return;
            //shield
            if (chosenAbility.GetComponent<Shield>())
            {
                List<GameObject> unshielded = new List<GameObject>();
                foreach (GameObject go in Friends)
                {
                    if (!go.GetComponentInChildren<Shield>())
                    {
                        unshielded.Add(go);
                    }
                }
                if (unshielded.Count == 0)
                {
                    SetTarget(gameObject);
                    return;
                }
                int x;
                x = Random.Range(0, unshielded.Count);
                SetTarget(unshielded[x]);
            }

            //berry
            else if (chosenAbility.GetComponent<HealingBerry>())
            {
                List<GameObject> healable = new List<GameObject>();
                foreach (GameObject go in Friends)
                {
                    if (go.GetComponent<Cell>().GetHealth() < go.GetComponent<Cell>().GetMaxHealth())
                    {
                        healable.Add(go);
                    }
                }
                int x;
                x = Random.Range(0, healable.Count);
                SetTarget(healable[x]);
            }

            else
            {
                int x;
                x = Random.Range(0, Friends.Count);
                while (Friends[x] == gameObject)
                {
                    x = Random.Range(0, Friends.Count);
                }
                SetTarget(Friends[x]);

            }

        }
        #endregion

        #region Self

        else if (chosenAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Self))
        {
            SetTarget(gameObject);
        }

        #endregion

        _GameObjectData.Target = GetTarget();
    }

    void Charge()
    {
        if (GetEnergy() - Cost > 0 && Cost != 0)
        {
            Paid = Cost;
            SetEnergy(GetEnergy() - Cost);
            if (isFiring)
                return;
            Fire();
            // StartCoroutine(CastingAnimation());
        }
        else
        {//if has less energy than required it will slowly charge until the ability has been paid for
            while (GetEnergy() > 0 && Paid < Cost)
            {
                SetEnergy(GetEnergy() - 1);
                Paid++;
            }
            if (Paid >= Cost && Cost != 0)
            {
                if (isFiring)
                    return;
                Fire();
                //StartCoroutine(CastingAnimation());
            }
        }
    }

    public void Fire()
    {

        if (isFiring)
            return;

        GetComponent<Cell>().fireScript.GetComponent<FireAbility>().Fire(_GameObjectData);
    }

}




//ALL of the old stuff probably never going to get used below!


    //Only if CHANNELING comes back!
    //void Channeling()
    //{
    //    if(!GetTarget())
    //    { return; }
    //   // isbusy = true;
    //    if (GetEnergy() - chosenAbility.GetComponent<AbilityBase>().GetDamage() > 0)
    //    {
    //        SetEnergy(GetEnergy() - chosenAbility.GetComponent<AbilityBase>().GetDamage());
    //    }

    //        GameObject go;
    //        Vector3 spanwpoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //        go = Instantiate(chosenAbility, spanwpoint, Quaternion.identity);
    //        go.GetComponent<AbilityBase>().SetTarget(GetTarget());
    //        go.GetComponent<AbilityBase>().SetSender(gameObject);
    //        go.GetComponent<AbilityBase>().SetTeam(GetTeam());
    //        Paid = Paid + chosenAbility.GetComponent<AbilityBase>().GetDamage();
    //       // Cost = Cost - chosenAbility.GetComponent<AbilityBase>().GetDamage();
        

    //    if (Paid >= Cost)
    //    {
    //        Paid = 0;
    //      //  isbusy = false;
    //        chosenAbility = null;
    //    }
   

    //public void PlayAngrySound()
    //{
    //    return;
    //int x = Random.Range(0, 2);
    //switch(x)
    //{
    //    case 0:
    //        AudioManager.instance.Play("Angry1");
    //        break;
    //    case 1:
    //        AudioManager.instance.Play("Angry2");
    //        break;
    //    case 2:
    //        AudioManager.instance.Play("Angry3");
    //        break;
    //    default:
    //        break;
    //}
//}

 //public void UpdateLists()
    //{
    //    //friendlies.Clear();
    //    //enemies.Clear();
    //    Friends.Clear();
    //    Enemies.Clear();

    //    if (Players.GetItems() != null)
    //        foreach (GameObject p in Players.GetItems())
    //        {
    //            if (p.GetComponent<Cell>().GetTeam() == gameObject.GetComponent<Cell>().GetTeam())
    //            {
    //                Friends.Add(p);
    //            }
    //            if (p.GetComponent<Cell>().GetTeam() != gameObject.GetComponent<Cell>().GetTeam())
    //            {
    //                Enemies.Add(p);
    //            }
    //        }

    //}
    //void aFire() //spawn chosen object and set its target as current target
    //{
    //    if (GetTarget() == null)
    //        TargetSelect();

    //    if(CurrentAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Enemy))
    //    {
    //        PlayAngrySound();
    //    }
    //    if(CurrentAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Friend))
    //    {
    //        PlayCrazySound();
    //    }

    //    isbusy = false;
    //    //GameObject go;
    //    //Vector3 spanwpoint = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z);
    //    //go = Instantiate(CurrentAbility, spanwpoint, Quaternion.identity);
    //    //go.GetComponent<AbilityBase>().SetSender(gameObject);
    //    //go.GetComponent<AbilityBase>().SetTarget(GetTarget());
    //    //go.GetComponent<AbilityBase>().SetTeam(GetTeam());
    //    GameObjectData newGameObjectDAta = new GameObjectData();

    //    newGameObjectDAta.Target = GetTarget();
    //    newGameObjectDAta.AbilityPrefab = CurrentAbility;
    //    newGameObjectDAta.StartLocation = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //    newGameObjectDAta.Sender = gameObject;
    //    newGameObjectDAta.TeamColor = TeamColor;
    //    newGameObjectDAta.TeamInt = GetTeam();
    //    _gameObjectDataEvent.Raise(newGameObjectDAta);


    //    if (CurrentAbility.GetComponent<AbilityBase>().TargetType.Equals(Ability.AbilityTargetEnum.Self))
    //    {
    //        //say nothing
    //    }
    //    //TODO: Add the History to a ScriptableObject
    //    //else
    //    //GetManager().GetComponent<GameManager>().ActionFire(
    //    //            CellName, CurrentAbility.GetComponent<AbilityBase>().AbilityName, 
    //    //            GetTarget().GetComponent<Cell>().CellName);

    //    chosenAbility = null;
    //    Paid = 0;
    //}

    //IEnumerator CastingAnimation()
    //{
    //    firing = true;
    //    Arms.GetComponent<Arms>().Clap();
    //    yield return new WaitForSeconds(0.8f);
    //    PlayUseAbilitySound();
    //    Fire();
    //    //play smoke animation;

    //    //so it doesnt fire twice
    //    yield return new WaitForSeconds(0.05f);
    //    firing = false;
    //}

    //void Attackselect()//randomly select attack ability
    //{
    //    if(Enemies.Count < 1)
    //    {
    //        return;
    //    }
    //    if (Attack.Count <1)
    //    {//if no attacks do nothing
    //        PickAction();
    //        return;
    //    }

    //    int shieldcount = 0;
    //    foreach (GameObject nmy in Enemies)
    //    {//count how many shields
    //        if(nmy.GetComponentInChildren<Shield>())
    //        {
    //            shieldcount++;
    //        }
    //        if(nmy.GetComponentInChildren<MegaShield>())
    //        {
    //            shieldcount = shieldcount + 2;
    //        }
    //    }
    //    if( Enemies.Count - shieldcount < 1)
    //    {//if there are too many shields fire missile
    //       foreach(GameObject atk in Attack)
    //       {
    //            if(atk.GetComponent<Burger>())
    //            {
    //                chosenAbility = atk;
    //            }
    //       }
    //       if(chosenAbility == null)
    //       {//if there is no missile attack pick another at random
    //            int x = Random.Range(0, Attack.Count);
    //            chosenAbility = Attack[x];
    //       }
    //    }
    //    else
    //    {
    //        int x = Random.Range(0, Attack.Count);
    //        chosenAbility = Attack[x];
    //    }
    //   // isbusy = true;
    //    Cost = chosenAbility.GetComponent<AbilityBase>().GetCost();
    //    _GameObjectData.AbilityPrefab = chosenAbility;
    //    TargetSelect();
    //    //x = Random.Range(0, enemies.Count);
    //    //target = enemies[x];

    //}

    //void Defendselect()//randomly select an attack and a target
    //{
    //    if(Friends.Count < 1)
    //    {
    //        return;
    //    }

    //    if (Defence.Count < 1)
    //    {//if no defence, do nothing
    //        PickAction();
    //        return;
    //    }

    //        int x = Random.Range(0, Defence.Count);
    //        chosenAbility = Defence[x];


    //    //check if there is a megashield and if so pick another ability
    //    if (chosenAbility.GetComponent<MegaShield>())
    //    {
    //        foreach (GameObject frind in Friends)
    //        {
    //            if (!frind.GetComponentInChildren<MegaShield>() && GetHealth()> chosenAbility.GetComponent<AbilityBase>().GetDamage() )
    //            {//if a megshield is not active
    //                //start charging megashield
    //            }
    //            else
    //            {
    //                if (Defence.Count > 1)
    //                {//make sure there are other abilities than megashield to choose from
    //                    while (chosenAbility.GetComponent<MegaShield>())
    //                    {//pick a new ability until we are no longer picking megashield
    //                        int y = Random.Range(0, Defence.Count);
    //                        chosenAbility = Defence[y];
    //                    }
    //                }
    //                else
    //                    PickAction();

    //            }
    //        }
    //    }

    //    //check if should use healingberry
    //    if(chosenAbility.GetComponent<HealingBerry>())
    //    {
    //        List<GameObject> healable = new List<GameObject>();
    //        foreach(GameObject go in Friends)
    //        {
    //            if(go.GetComponent<Cell>().GetHealth() != go.GetComponent<Cell>().GetMaxHealth())
    //            {//not at full hp
    //                healable.Add(go);
    //            }
    //        }
    //        if (healable.Count == 0)
    //        {//no targets to heal, pick new ability
    //            if (Defence.Count > 2)
    //            {//more than just megashield and healingberry
    //                while (chosenAbility.GetComponent<MegaShield>() || chosenAbility.GetComponent<HealingBerry>())
    //                {//pick until something that is not megashield and healingberry is picked
    //                    int y = Random.Range(0, Defence.Count);
    //                    chosenAbility = Defence[y];
    //                }
    //            }
    //            else//if there are no abilities to pick
    //                PickAction();
    //        }
    //    }

    //    _GameObjectData.AbilityPrefab = chosenAbility;
    //    Cost = chosenAbility.GetComponent<AbilityBase>().GetCost();
    //    TargetSelect();
    //}

    //void Utilityselect()
    //{
    //    if (Utility.Count < 1)
    //    {//if no defence, do nothing
    //        PickAction();
    //        return;
    //    }

    //    UpdateLists();
    //    if (Enemies.Count < 1 && Friends.Count < 1)
    //        return;

    //    //Joke
    //    bool SomeoneIsTellingJoke = false;
    //    float Teamhealth = 0;
    //    foreach(GameObject go in Friends)
    //    {
    //        Teamhealth = Teamhealth + go.GetComponent<Cell>().GetHealth();

    //        if(go.GetComponentInChildren<JokeBuff>())
    //        {

    //            SomeoneIsTellingJoke = true;
    //        }
    //    }
    //    Teamhealth = Teamhealth / Friends.Count;

    //    if(Teamhealth < 95 && SomeoneIsTellingJoke ==false)
    //    {
    //        foreach(GameObject go in Utility)
    //        {
    //            if(go.GetComponent<Joke>())
    //            {
    //                chosenAbility = go;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        int x = Random.Range(0, Utility.Count);
    //        chosenAbility = Utility[x];
    //        if (Utility.Count > 1)
    //        {
    //            while (chosenAbility.GetComponent<Joke>())
    //            {
    //                int y = Random.Range(0, Utility.Count);
    //                chosenAbility = Utility[y];
    //            }
    //        }
    //    }

    //    //    isbusy = true;
    //    if (chosenAbility != null)
    //    {
    //        _GameObjectData.AbilityPrefab = chosenAbility;
    //        Cost = chosenAbility.GetComponent<AbilityBase>().GetCost();
    //        TargetSelect();
    //    }

    //}

