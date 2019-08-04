using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAtStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void FixedUpdate()
    {
        if (gameObject.transform.localScale.z < Vector3.forward.z)
        {
            transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
        }
        else
        {
            this.enabled = false;
        }
    }
}
