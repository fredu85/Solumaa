using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{ 
    public ObjectPoolScriptable objectList;

    public void Start()
    {
        objectList.ClearList();
    }

    public void SpawnObject(GameObjectData data)
    {
        objectList.SpawnObject(data);
    }
}
