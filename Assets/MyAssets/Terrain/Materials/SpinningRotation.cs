using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRotation : MonoBehaviour
{
    public float RotationSpeed = 0.1f;
    void Update()
    {
        float FinalRotation = Time.time * RotationSpeed;
        this.transform.eulerAngles = new Vector3(0, 0, FinalRotation * 100);

    }
}
