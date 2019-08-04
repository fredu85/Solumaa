using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkTicker : MonoBehaviour
{

    public StringEvent GemEvent;
    public StringEvent GemEventWalked;

    CellObject ThisCellObject;
    int[] gemGoals = { 1000, 2500, 5000, 10000 };
    public GameEvent HasWalkedEvent;

    private void Awake()
    {
        ThisCellObject=GetComponent<Player>().ThisCellObject;
    }

    public void OnTick()
    {
        GemProgresstick();
    }

    void GemProgresstick()
    {
        ThisCellObject.Steps++;
        if (ThisCellObject.Steps % 10 == 0)
        {

        }

        if (ThisCellObject.Steps == gemGoals[0])
        {
            GemComplete(10);
        }
        else if (ThisCellObject.Steps == gemGoals[1])
        {
            GemComplete(25);
        }
        else if (ThisCellObject.Steps == gemGoals[2])
        {
            GemComplete(33);
        }
        else if (ThisCellObject.Steps == gemGoals[3])
        {
            GemComplete(50);
        }
    }

    void GemComplete(int gems)
    {
        GemEvent.Raise(gems.ToString());
        GemEventWalked.Raise(ThisCellObject.Steps.ToString());
        
        ThisCellObject.Gems = ThisCellObject.Gems + gems;
        HasWalkedEvent.Raise();
    }
}
