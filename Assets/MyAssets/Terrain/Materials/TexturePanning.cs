using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePanning : MonoBehaviour
{
    public float PanX = 0.1f;
    public float PanY = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float OffsetX = Time.time * PanX;
        float OffsetY = Time.time * PanY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
