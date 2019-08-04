using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllyWindowUI : MonoBehaviour
{
    GameObject player;
    List<GameObject> playerteam = new List<GameObject>();
    List<GameObject> createdUI = new List<GameObject>();
    public GameObject manager;

    public GameObject AllyInfoPrefab;

    //public RuntimeSet Players;
    public ObjectList Players;

    //private void Awake()
    //{
    //  //  manager = GameObject.Find("GameManager");
    //}


    private void OnEnable()
    {
        UpdateAlly();
    }
  
  
    public void UpdateAlly()
    {

        foreach (GameObject allies in createdUI)
        {
            Destroy(allies);
        }
        playerteam.Clear();

    
      //  player = GameObject.FindGameObjectWithTag("Player");


        if (Players.Items == null)
        {
            return;
        }


        foreach(GameObject cell in Players.Items)
        {
            if (cell.GetComponent<Player>())
                player = cell;
         

        }
        if (player == null)
        {
            return;
        }

        
        foreach (GameObject cell in Players.Items)
        {
            if(cell.GetComponent<Cell>().GetTeam() == player.GetComponent<Cell>().GetTeam())
            {
                if (cell == player)
                {
                    //dont add player to the list
                }
                else
                {
                    playerteam.Add(cell);
                }
            }
        }
        AllyList();
    }

    public void AllyList()
    {
        foreach(GameObject ally in playerteam)
        {
            GameObject go = Instantiate(AllyInfoPrefab);
            go.transform.SetParent(gameObject.transform);
            go.transform.localScale = new Vector3(1, 1, 1);

            go.GetComponent<PlayerAllyInfoSingularUI>().AllyCell = ally;

            createdUI.Add(go);

        }
    }
    
}
