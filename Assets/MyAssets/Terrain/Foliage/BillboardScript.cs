using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{

    Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main;
        Vector3 lookpos = m_Camera.transform.position - transform.position;

        float scale = Random.Range(0.3f, 0.8f);
        transform.localScale = new Vector3(scale, scale, scale);

        lookpos.x = transform.rotation.x;
        transform.rotation = Quaternion.LookRotation(lookpos);
       
        if(transform.rotation.y == 0)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        }

    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    //void LateUpdate()
    //{
        //need to move the X rotation;



        //  transform.LookAt(transform.position + m_Camera.transform.rotation * -Vector3.forward,
        //     m_Camera.transform.rotation * Vector3.up);
     
        //b = transform.eulerAngles;
        //b.z = 180;
        //transform.eulerAngles = b;

    //}
}
