using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempEnergyScript : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI EnergyText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        EnergyText.text ="Energy: "+ (player.GetComponent<Player>().GetEnergy()).ToString();
    }
}
