using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTestScript : MonoBehaviour
{
    public GameEvent TestEvent;

    // Update is called once per frame
    void Update()
    {
        TestEvent.Raise();
    }
}
