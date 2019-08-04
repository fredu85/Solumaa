using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibilityOnEnterLeave : MonoBehaviour
{
    int AmountUnderRoof = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerEnter(Collider other)
    private void OnTriggerEnter(Collider other)
    {
        AmountUnderRoof++;
        GetComponent<MeshRenderer>().enabled = false;
        
    }
    private void OnTriggerExit(Collider other)
    {
        AmountUnderRoof--;
        if(AmountUnderRoof==0)
        GetComponent<MeshRenderer>().enabled = true;
    }
}
