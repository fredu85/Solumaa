using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joke : Ability
{
  
    List<GameObject> friendlies = new List<GameObject>();
    public GameObject JokeBuff;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject player in Players.Items)
        {
            if (player.GetComponent<Cell>().GetTeam() == GetTarget().GetComponent<Cell>().GetTeam())
            {
                friendlies.Add(player);
            }
        }

        //destroy any remaining jokebuffs
        foreach (GameObject friend in friendlies)
        {
            if (friend.GetComponentInChildren<JokeBuff>())
            {
                GameObject otherJokeBuff = friend.GetComponentInChildren<JokeBuff>().gameObject;
                if (otherJokeBuff != gameObject)
                {
                    //Debug.Log("Destroying a joke");
                    otherJokeBuff.SetActive(false);
                }
            }
        }
        //spawn jokebuffs
        foreach (GameObject friend in friendlies)
        {
            Vector3 startpos = new Vector3(0,1, 0);
            GameObject go = Instantiate(JokeBuff, friend.transform.position+startpos, Quaternion.identity);
            go.GetComponent<AbilityBase>().SetTarget(friend);
            go.GetComponent<AbilityBase>().SetTeam(GetTeam());
            go.transform.parent = friend.transform;
            //if (go.GetComponent<JokeBuff>())
            //{
            //    //go.GetComponent<JokeBuff>().JokeParent = friend;
            //}


        }

        gameObject.SetActive(false);
    }

 
}
