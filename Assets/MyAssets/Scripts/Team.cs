using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Team : MonoBehaviour
{
    public GameObject[] Friendlies;
    //public GameObject[] Spawns;

    public GameObject FriendStart;

    //public Material maxcolor;
    //public Material mincolor;
    public Color FriendColor;

    public int TeamInt;
    public CellObject CurrentPlayer;

    //public RuntimeSet Players;
    public ObjectList Players;
    public ObjectList FriendlyCellGroup;
    public ObjectList DefaultCellTypes;//0 Attack, 1 Average, 2 Secret, 3 Support, 4 All abilities
    public GameObject DefaultCell;

    //GameManager gameManager;

    private void Start()
    {
        InstantiateCells(Friendlies, FriendStart, FriendColor);
    }

    private void InstantiateCells(GameObject[] team, GameObject start, Color maxcolor)
    {
        if (team.Length ==0 && !start)
            return;

        if(maxcolor==null)
        {
         Debug.Log("Missing Max Color");
         return;
        }

        int realCount = FriendlyCellGroup.Items.Count;
        for (int i = 0; i < FriendlyCellGroup.Items.Count; i++)
            if (FriendlyCellGroup.Items[i] == DefaultCell)
                realCount--;

        //float distance = team.Length * 0.65f;
        float distance = realCount * 0.65f;
        for (int i=0; i< FriendlyCellGroup.Items.Count; i++ )
        {

            float angle = 360 / realCount;
            angle = angle * i;
            Vector3 position = new Vector3(distance * Mathf.Sin(angle * Mathf.Deg2Rad) + transform.position.x,
                      transform.position.y,
                     distance * Mathf.Cos(angle * Mathf.Deg2Rad) + transform.position.z);

            //GameObject go = Instantiate(team[i], position, Quaternion.identity);
            GameObject go;
            if (FriendlyCellGroup.Items[i] != DefaultCell)
            {
                go = Instantiate(FriendlyCellGroup.Items[i], position, Quaternion.identity);

                go.transform.parent = gameObject.transform;
                go.GetComponent<Cell>().SetTeam(TeamInt);
                go.GetComponent<Cell>().TeamColor = maxcolor;
                go.GetComponent<Cell>().UpdateColor();
            }
            else
            {
                continue;
                //If list contains a default empty AI, that means nothing is spawned.
                //go = Instantiate(DefaultCellTypes.Items[1], position, Quaternion.identity);
            }

            /*
            go.transform.parent = gameObject.transform;
            //choose the team for it (set it for this object in the inspector window)
            go.GetComponent<Cell>().SetTeam(TeamInt);
            //make sure it has a link to the game manager
            //go.GetComponent<Cell>().SetManager(gameManager);
            //add gameobject to the players list
            //gameManager.GetComponent<GameManager>().Players.Add(go);
            //set the color of them to be of the material set in the inspector for this object
            go.GetComponent<Cell>().TeamColor = maxcolor;
            go.GetComponent<Cell>().UpdateColor();*/
        }
    }

    //private void MakeMinColor(Color MaxColor, GameObject cell)
    //{
    //    float r = MaxColor.r;
    //    float g = MaxColor.g;
    //    float b = MaxColor.b;
    //    float a = 1;

    //    if(r > g && r>b)
    //    {
    //        r = r / 1.5f;
    //        g = g/3f;
    //        b = b/3f;
    //    }
    //    else if(g>r && g>b)
    //    {
    //        g = 0f;
    //        b = 0f;
    //        r = 0f;
    //    }
    //    else
    //    {
    //        b = b/1.5f;
    //        r = r/3;
    //        g = g/3;
    //    }
    //   // cell.GetComponent<Cell>().LowHpColor = new Color(r, g, b, a);
    //}

    //private void Awake()
    //{
    //    gameManager = GameManager.FindObjectOfType<GameManager>();
    //    transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-90, 90),0));

    //    for(int i =0; i<Friendlies.Length;i++)
    //    {
    //        //create first object in the public array
    //        Friendlies[i] = Instantiate(Friendlies[i], transform.position, Quaternion.identity);
    //        //for easier inspector viewing, set it as child
    //        Friendlies[i].transform.parent = gameObject.transform;
    //        //choose the team for it (set it for this object in the inspector window)
    //        Friendlies[i].GetComponent<Cell>().SetTeam(team);
    //        //spawn it at one of the objects in this game objects spawns array
    //        Friendlies[i].transform.position = Spawns[i].transform.position;
    //        //make sure it has a link to the game manager
    //        Friendlies[i].GetComponent<Cell>().SetManager(gameManager);

    //        //add gameobject to the players list
    //        gameManager.GetComponent<GameManager>().Players.Add(Friendlies[i]);

    //        //set the color of them to be of the material set in the inspector for this object

    //        Friendlies[i].GetComponent<Cell>().matfullhp = maxcolor;
    //        Friendlies[i].GetComponent<Cell>().matlowhp = mincolor;

    //        Friendlies[i].GetComponent<Cell>().UpdateColor();

    //        //destroy spawns as they are used
    //        Destroy(Spawns[i]);
    //    }

    //    //disable remaining spawners
    //    foreach(GameObject spawns in Spawns)
    //    {
    //        spawns.SetActive(false);
    //    }
    //}
}
