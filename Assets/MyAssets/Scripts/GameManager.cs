using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{ 
    public delegate void Tick();
    public static event Tick OnTick;

    // int count = 0;
    float timeToGo;

   // public List<GameObject> AllAbilites = new List<GameObject>();
   // public List<GameObject> StartingAbilities = new List<GameObject>();
   // public List<GameObject> Team0Abiliies;
  //  public List<GameObject> Team1Abiliies;

    //public Canvas WalkAchievement;

    public List<string> Actions;
   // public GameObject OverViewHistory;
    //EVENTS
    public GameEvent TickEvent;

    //  public RuntimeSet Players;
    public ObjectList Players;
    public ObjectList StartingAbilities;
    public StringList _StringList;
    public GameEvent UpdateHistory;

    private void Awake()
    {
         Players.Items.Clear();
        _StringList.Items.Clear();
        //Team0Abiliies = StartingAbilities;
        //Team1Abiliies = StartingAbilities;
    }

    private void Start()
    {
        timeToGo = Time.fixedTime + 0.5f;
    }

    private void FixedUpdate()
    {

        //  CheckPlayers(); //TODO dont do this every update? Moved to Tick thingy
        if (Time.fixedTime >= timeToGo)
        {
            //// Do your thang
            if (OnTick != null)
                OnTick();
            
            TickEvent.Raise();
           // CheckPlayers();
            timeToGo = Time.fixedTime + 0.1f;
         

        }
        //   TickEvent.Raise();
    }

    private void Update()
    {
        

        // DoTick();
    }

    public void CheckPlayers()
    {
        for (var i = Players.Items.Count - 1; i > -1; i--)
        {
            if (Players.Items[i] == null)
                Players.Items.Remove(Players.Items[i]);
        }
    }

    public void AddCell(GameObject go)
    {
     
        Players.Add(go);
        CheckPlayers();
    }
  
    public void GetTeamMembers(GameObject go)
    {
        List<GameObject> cells = new List<GameObject>();
        foreach (GameObject p in Players.Items)
        {
            if(p.GetComponent<Cell>().GetTeam() == go.GetComponent<Cell>().GetTeam())
            {
                cells.Add(p);
            }
        }
        go.GetComponent<Cell>().Friends = cells;
    }

    public void GetEnemyMembers(GameObject go)
    {
        List<GameObject> cells = new List<GameObject>();
        foreach (GameObject p in Players.Items)
        {
            if (p.GetComponent<Cell>().GetTeam() != go.GetComponent<Cell>().GetTeam())
            {
                cells.Add(p);
            }
        }
        go.GetComponent<Cell>().Enemies = cells;
    }



    public void ActionFire(string who, string what,string atwho)
    {
        string temp = who + " launched " + what + " at " + who;
        _StringList.Add(temp);
        UpdateHistory.Raise();
      //  Actions.Add(temp);
      //  OverViewHistory.GetComponent<OverviewHistoryUI>().CreateNewEntry(temp);
    }
    public void ActionDamage(string what, string who, string damage, string from)
    {
        string temp = from + "s " + what + " hit " + who + " for " + damage + " damage";
        _StringList.Add(temp);
        UpdateHistory.Raise();
        //Actions.Add(temp);
        //OverViewHistory.GetComponent<OverviewHistoryUI>().CreateNewEntry(temp);
    }
    public void ActionHeal(string what, string who, string heal, string from)
    {
        string temp = from + "s " + what + " healed " + who + " for " + heal + " health";
        _StringList.Add(temp);
        UpdateHistory.Raise();
        //OverViewHistory.GetComponent<OverviewHistoryUI>().CreateNewEntry(temp);
        //Actions.Add(temp);
    }

    private void ClearHistory()
    {
        // Actions.Clear();
        _StringList.Items.Clear();
    }

    //public void AddAbility(int teamnumber, GameObject ability)
    //{
    //    if(teamnumber == 0)
    //    {
    //        Team0Abiliies.Add(ability);
    //    }
    //    if(teamnumber == 1)
    //    {
    //        Team1Abiliies.Add(ability);
    //    }
    //}
    public void TestGameObjectData(GameObjectData test)
    {
        
    }


}
