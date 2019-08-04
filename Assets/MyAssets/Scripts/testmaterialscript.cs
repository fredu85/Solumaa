using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmaterialscript : MonoBehaviour
{
    public float speed =12f;
   
    Color endc;
    float starttime;
    public float h = 100;
    float healthratio;
   // bool changedir = false;
    public Material matfullhp;
    public Material matlowhp;

    float timeLeft;


    private void Start()
    {
       // endc = new Color(endc.r, endc.g, endc.b, h / 100);
        starttime = Time.time;
    }

    //private void Update()
    //{

    //    // endc = (matfullhp.color / (100f / h));
    //    endc = matlowhp.color*1/h;

    //    float t = (Time.time - starttime) * speed;
    //    //  GetComponent<Renderer>().material.color = Color.Lerp(startc, endc, t);
    //    GetComponent<Renderer>().material.color = endc;

    //}
    void Update()
    {

        GetComponent<Renderer>().material.color = Color.Lerp(matlowhp.color, matfullhp.color, h/100f);
        //if (timeLeft <= Time.deltaTime)
        //{
        //    // transition complete
        //    // assign the target color
        //    GetComponent<Renderer>().material.color = targetColor;

        //    // start a new transition
        //    targetColor = new Color(Random.value, Random.value, Random.value);
        //    timeLeft = 1.0f;
        //}
        //else
        //{
        //    // transition in progress
        //    // calculate interpolated color
        //    GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, targetColor, Time.deltaTime / timeLeft);

        //    // update the timer
        //    timeLeft -= Time.deltaTime;
        //}
    }

}
