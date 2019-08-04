using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUI : MonoBehaviour
{
    public List<Sprite> HowToPlaySprites;
    int currentSprite = 0;

    public GameObject DisplayImage;
    public GameObject NextButton;
    public GameObject PreviousButton;

    private void Start()
    {
        PreviousButton.SetActive(false);
    }

    public void NextImage()
    {
        if (currentSprite >= HowToPlaySprites.Count)
            return;
        PreviousButton.SetActive(true);
        DisplayImage.GetComponent<Image>().overrideSprite = HowToPlaySprites[currentSprite+1];
        currentSprite++;

        if(currentSprite >= HowToPlaySprites.Count-1)
        {
           NextButton.SetActive(false);
        }


    }

    public void PreviousImage()
    {
        if (currentSprite == 0)
            return;

        NextButton.SetActive(true);
        DisplayImage.GetComponent<Image>().overrideSprite = HowToPlaySprites[currentSprite-1];
        currentSprite--;

        if(currentSprite <1)
        {
            PreviousButton.SetActive(false);
        }

    }
}
