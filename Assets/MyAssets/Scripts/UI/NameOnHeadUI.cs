using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameOnHeadUI : MonoBehaviour
{

    public Text NameOfCell;
    public GameObject AI;
    public GameObject canvascamera;
    // Start is called before the first frame update
    void Start()
    {
        canvascamera.GetComponent<Canvas>().worldCamera = Camera.main;
        NameOfCell.text = AI.GetComponent<AI>().CellName;
    }

    // Update is called once per frame
    void Update()
    {
      
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        namePos.x = namePos.x+ 40f;
        namePos.y = namePos.y + 20f;
        NameOfCell.transform.position = namePos;

    }
}
