using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing002 : MonoBehaviour
{

    float i = 0.0f;
    //adjust this to change speed
    public float speed = 1.0f;
    //adjust this to change how high it goes
    //float height = -0.5f;
    float distanceTraveled = 0.0f;
    public float frequency = 100.0f;
    //bool ResetX = false;
    public Vector3 position1 = new Vector3(0, 0, 0);
    void Start()
    {
        //float oldX = this.transform.position.x;
    }
    void Update()
    {
        if (i < speed)
        {
            i++;
        }
        else
        {


            if (distanceTraveled <= frequency)
            {
                //  Hummmm this could've been done inn
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                distanceTraveled++;
            }
            else
            {
                //  Takes the object back to it's initial location
                transform.position = new Vector3(transform.position.x + distanceTraveled, transform.position.y, transform.position.z);
                distanceTraveled = 0.0f;
            }
            i = 0;
        }
    }
}
