using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : UIInheritenceStart
{
    public Canvas OverView;
    public Canvas PlayerView;
    public Canvas InspectView;
    public Canvas AbilityView;

    public GameObject Cameramanager;
    public GameEvent CameraMoveTarget;
    public GameEvent CameraMoveBack;

    public AudioSource _PlaySound;
    public SimpleAudioEvent UIClick;

    private void Awake()
    {
        DisableAll();
        OverView.enabled = true;
    }

    public void DisableAll()
    {
        OverView.enabled = false;
        PlayerView.enabled = false;
        InspectView.enabled = false;
        AbilityView.enabled = false;
    }

    public void ClickSound()
    {
        UIClick.Play(_PlaySound);
       // AudioManager.instance.Play("UIClick");
    }

    public void EnableCanvas(Canvas c)
    {
        ClickSound();
        DisableAll();
        c.enabled = true;

        PlayerObject.CanSelectTarget = false;

        if(c == PlayerView)
        {
            CameraMoveTarget.Raise();
        }
        if(c == OverView)
        {
            CameraMoveBack.Raise();
            PlayerObject.CanSelectTarget = true;
        }

       if(c==AbilityView)
        {

        }
    }
   
}
