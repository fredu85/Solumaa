using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTypeManager : MonoBehaviour
{
    [SerializeField]
    private ObjectList playerGroup;
    [SerializeField]
    private ObjectList enemyGroup;

    [SerializeField]
    private ObjectList defaultPlayerGroup;
    [SerializeField]
    private ObjectList defaultEnemyGroup;


    public void SetDefaultPlayerGroup()
    {
        playerGroup.Items = defaultPlayerGroup.Items;
    }

    public void SetDefaultEnemyGroup()
    {
        enemyGroup.Items = defaultEnemyGroup.Items;
    }

    public void ChangePlayerGroup(ObjectList newPlayerGroup)
    {
        playerGroup.Items = newPlayerGroup.Items;
    }

    public void ChangeEnemyGroup(ObjectList newEnemyGroup)
    {
        enemyGroup.Items = newEnemyGroup.Items;
    }
}
