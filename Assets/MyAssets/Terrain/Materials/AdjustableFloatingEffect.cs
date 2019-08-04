using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableFloatingEffect : MonoBehaviour
{
    //adjust this to change speed
    public float speed = 1.0f;
    //adjust this to change how high it goes
    public float amplitude = 0.0f;
    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = amplitude * Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY , pos.z);
    }
}
