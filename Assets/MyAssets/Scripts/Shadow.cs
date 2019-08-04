using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    Vector3 position;
    public Shadow shadowscript;
    public GameObject thingthatgetsshadow;

    public bool movingshadow = false;
    float distance;
    
    float scale;
    private void Start()
    {
        if(movingshadow)
        transform.parent = null;

        name = "Shadow_" + thingthatgetsshadow.ToString();
    }

    private void FixedUpdate()
    {
        if (thingthatgetsshadow == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!movingshadow)
        {
            shadowscript.enabled = false;
            return;
        }

        position = thingthatgetsshadow.transform.position;
        position.y = 0.2f;

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.position = position;

        // scale = Vector3.Distance(transform.position,thingthatgetsshadow.transform.position);
        scale = thingthatgetsshadow.transform.position.y - position.y;
        transform.localScale = new Vector3(1 / scale+0.1f, 1 / scale+0.1f);
    }

}
