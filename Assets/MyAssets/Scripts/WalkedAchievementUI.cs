using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalkedAchievementUI : MonoBehaviour
{
    public Canvas AchievementParentCanvas;
    public TextMeshProUGUI AmountWalked;
    public TextMeshProUGUI AmountGems;


    public GameObject achievementparent;
    public CellObject PlayerStats;

    public void SetGemString(string s)
    {
        AmountGems.text = "You recieve " + s + " Gems!";
    }

    public void SetWalkedSTring(string w)
    {
        AmountWalked.text = "You have Walked " + w + " Steps!";
    }

    public void EnableAchievementCanvas()
    {
        // achievementparent.SetActive(true);
        AchievementParentCanvas.enabled = true;


        Time.timeScale = 0;
    }

    public void OkButtonClick()
    {
        Time.timeScale = 1;

        //achievementparent.SetActive(false);
        AchievementParentCanvas.enabled = false;
    }
}
