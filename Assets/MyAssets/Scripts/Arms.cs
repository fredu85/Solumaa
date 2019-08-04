using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{

    public Animator Animator;
    public GameObject Clapeffect;

    public GameObject SetColor;
    public GameObject Getcolor;

    

    private void ArmsColor()
    {
        SetColor.GetComponent<Renderer>().material.color = Getcolor.GetComponent<Renderer>().material.color;
    }

    public void Clap()
    {
        gameObject.SetActive(true);
        ArmsColor();
        Animator.SetTrigger("Clap");
        StartCoroutine(ClapEffect());
    }

    IEnumerator ClapEffect()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        GetComponent<GrowAtStartScript>().enabled = true;
        yield return new WaitForSeconds(0.7f);
        Clapeffect.SetActive(true);
      //  yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
   
}
