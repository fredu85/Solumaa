using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTargetVFXScript : MonoBehaviour
{
    public GameObject onDisableVFX;
    public GameObject colorholder;
    public ParticleSystem childps;
    public GameObject child;
    private ParticleSystem ps;
    private ParticleSystem.MainModule main;


    private void OnEnable()
    {
        if (!colorholder)
            return;

        Color Parentcolor =colorholder.GetComponent<Renderer>().material.color;

        ps = GetComponent<ParticleSystem>();
        main = ps.main;
        main.startColor = Parentcolor;


        if (childps)
        {
            main = childps.main;
            main.startColor = Parentcolor;
        }

        if(child != null)
        {
            ps = child.GetComponent<ParticleSystem>();
            main = ps.main;
            main.startColor = Parentcolor;
        }

        //if (transform.parent.GetComponent<AI>())
        //{
        //    if (transform.parent.GetComponent<AI>().thingthatchangescolor != null)
        //    {
        //        Color parentcolor = transform.parent.GetComponent<AI>().thingthatchangescolor.GetComponent<Renderer>().material.color;

        //        ps = GetComponent<ParticleSystem>();
        //        main = ps.main;
        //        main.startColor = parentcolor;


        //        if (childps)
        //        {
        //            main = childps.main;
        //            main.startColor = parentcolor;

        //        }
        //    }
        //}
        //else if (transform.parent.GetComponent<Player>())
        //    if (transform.parent.GetComponent<Player>().thingthatchangescolor != null)
        //    {
        //        Color parentcolor = transform.parent.GetComponent<Player>().thingthatchangescolor.GetComponent<Renderer>().material.color;

        //        ps = GetComponent<ParticleSystem>();
        //        main = ps.main;
        //        main.startColor = parentcolor;

        //        if (childps)
        //        {
        //            main = childps.main;
        //            main.startColor = parentcolor;

        //        }
        //    }


    }


    private void OnDisable()
    {
        if (onDisableVFX==null)
            return;

        GameObject go = Instantiate(onDisableVFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        Color Parentcolor = colorholder.GetComponent<Renderer>().material.color;

        ps = go.GetComponent<ParticleSystem>();
        main = ps.main;
        main.startColor = Parentcolor;

        if (childps)
        {
            main = childps.main;
            main.startColor = Parentcolor;

        }

        //if (transform.parent.GetComponent<AI>())
        //{
        //    if (transform.parent.GetComponent<AI>().thingthatchangescolor != null)
        //    {
        //        Color parentcolor = transform.parent.GetComponent<AI>().thingthatchangescolor.GetComponent<Renderer>().material.color;

        //        ps = go.GetComponent<ParticleSystem>();
        //        main = ps.main;
        //        main.startColor = parentcolor;



        //    }
        //}
        //else if (transform.parent.GetComponent<Player>())
        //    if (transform.parent.GetComponent<Player>().thingthatchangescolor != null)
        //    {
        //        Color parentcolor = transform.parent.GetComponent<Player>().thingthatchangescolor.GetComponent<Renderer>().material.color;

        //        ps = go.GetComponent<ParticleSystem>();
        //        main = ps.main;
        //        main.startColor = parentcolor;


        //    }
        //if (transform.parent.gameObject.GetComponent<Renderer>() != null)
        //{
        //    Color parentcolor = transform.parent.gameObject.GetComponent<Renderer>().material.color;

        //    ps = go.GetComponent<ParticleSystem>();
        //    main = ps.main;
        //    main.startColor = parentcolor;
        //}
        //else if (transform.parent.gameObject.GetComponent<Renderer>() == null)
        //{
        //    Color parentcolor = colorholder.GetComponent<Renderer>().material.color;


        //    ps = go.GetComponent<ParticleSystem>();
        //    main = ps.main;
        //    main.startColor = parentcolor;
        //}

    }
}

