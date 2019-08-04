using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ObjectPool", menuName = "ScriptableObjects/new ObjectPoolManager", order = 52)]
public class ObjectPoolScriptable : ScriptableObject
{
   public List<ObjectList> Pool = new List<ObjectList>();

   //Spawns or Activates Abilities previously spawned
   public void SpawnObject(GameObjectData data)
    {
        GameObject ability;

        if(data.AbilityPrefab.GetComponent<AbilityBase>().Pool.Items.Count < 1)
        { 
            ability = Instantiate(data.AbilityPrefab, data.StartLocation, Quaternion.identity);
            SetObjectData(data, ability);
        }
        else
        {
            ability = data.AbilityPrefab.GetComponent<AbilityBase>().Pool.Items[0];
            SetObjectData(data, ability);
            ability.GetComponent<AbilityBase>().Pool.Remove(ability);
        }
    }
    
    public void SetObjectData(GameObjectData data, GameObject ability)
    {
        
        ability.GetComponent<AbilityBase>().TeamColor = data.TeamColor;
        ability.transform.position = data.Sender.transform.position + new Vector3(0, 1.5f, 0);
        ability.GetComponent<AbilityBase>().SetTarget(data.Target);
        ability.GetComponent<AbilityBase>().SetSender(data.Sender);
        ability.GetComponent<AbilityBase>().SetTeam(data.TeamInt);
        ability.SetActive(true);
    }

    public void ClearList()
    {
        if(Pool.Count > 0)
        foreach (ObjectList x in Pool)
        {
            if(x.Items.Count > 0)
            x.Items.Clear();
        }
    }

    public void AddGameObject(GameObject go)
    {
        go.GetComponent<AbilityBase>().Pool.Items.Add(go);
    }
    //List<GameObject> temp = null;
    //if (DictPool.TryGetValue(go.GetComponent<AbilityBase>().AbilityInt, out temp))
    //{
    //    temp.Add(go);
    //}
    //else
    //{
    //    unknownslist.Add(go);
    //}

    //bool available = false;
    //List<GameObject> temp = null;

    //if(DictPool.TryGetValue( data.AbilityPrefab.GetComponent<AbilityBase>().AbilityInt, out temp) )
    //{
    //    if (temp.Count > 1)
    //    {
    //        ability = temp[0];
    //        SetObjectData(data, ability);
    //        temp.Remove(temp[0]);
    //    }
    //    else
    //    {
    //        ability = Instantiate(data.AbilityPrefab, data.StartLocation, Quaternion.identity);
    //        SetObjectData(data, ability);
    //    }
    //}
    //else
    //{
    //    ability = Instantiate(data.AbilityPrefab, data.StartLocation, Quaternion.identity);
    //    SetObjectData(data, ability);
    //    DictPool.Add(ability.GetComponent<AbilityBase>().AbilityInt, unknownslist);
    //}

    //for (int i = Pool.Count - 1; i >= 0; i--)
    //{
    //    //TODO: do a better compare!
    //    if (true)
    //    {
    //        ability = Pool[i];
    //        Pool.Remove(ability);
    //        Debug.Log("Found one, Pool Size is now: " + Pool.Count);
    //        i = 0;
    //        SetObjectData(data,ability);
    //        available = true;
    //    }
    //}

    //if(!available)
    //{
    //    ability = Instantiate(data.AbilityPrefab, data.StartLocation, Quaternion.identity);
    //    SetObjectData(data, ability);
    //    return;
    //}

}
