using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakrSplash : MonoBehaviour
{

    public GameObject Splash;
    public GameObject Circle;

    public void WaterSplash()
    {
        Splash.SetActive(true);
    }
    public void WaterCircle()
    {
        Circle.SetActive(true);
    }
}
