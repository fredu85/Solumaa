using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Ability
{
    
    public int AbsorbAmount;
    protected int ShieldColor;
    public GameObject Visual;
    public Color VisualColor;
    protected float VisualColorAlpha = .6f;
    protected float TargetAlpha = 0.5f;
    protected float scrollspeed = 0.01f;

    // Start is called before the first frame update
    protected void Start()
    {
        //AudioManager.instance.Play("ShieldStart");
        transform.position = GetTarget().transform.position;
        transform.parent = GetTarget().transform;
        VisualColor=GetTarget().GetComponent<Cell>().thingthatchangescolor.GetComponent<Renderer>().material.color;

        foreach (Transform t in GetTarget().GetComponentsInChildren<Transform>(true))
        {
            if (t.gameObject.GetComponent<Shield>())
            {
                if (t.gameObject != gameObject)
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
       // Visual.GetComponent<Renderer>().material.color = VisualColor;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (AbsorbAmount < 1 && TargetAlpha < 0)
        {//no shield left, fade away
            if (VisualColorAlpha <= 0)
                gameObject.SetActive(false);
        }


        //fade towards target value
        if (VisualColorAlpha > TargetAlpha)
        {
            VisualColorAlpha = VisualColorAlpha - 0.005f;
        }
        else if(TargetAlpha > VisualColorAlpha)
        {
            VisualColorAlpha = VisualColorAlpha + 0.005f;
        }

        //set value
        VisualColor.a = VisualColorAlpha;
        Visual.GetComponent<Renderer>().material.color = VisualColor;

        float offset = Time.time * scrollspeed;
        Visual.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset / 2, -offset);

    }

    protected void OnTriggerEnter(Collider other)
    {
        //dont collide if absorbamount is less than 1
        if(AbsorbAmount < 1)
        {
            return;
        }
        //if not an ability dont collide
        if(other.gameObject.GetComponent<AbilityBase>() == null)
        {
            return;
        }

        //if(other.gameObject.GetComponent<AbilityBase>().GetTeam() == -1)
        //{ //if team is not known destroy the shield anyway (team is set every tick for ablities)
        //    AudioManager.instance.Play("HitShield");
        //    Destroy(other.gameObject);
        //    AbsorbAmount--;
        //    weakenShield();
        //    return;
        //}

        //can only hit enemy projectiles
        if(other.gameObject.GetComponent<AbilityBase>().GetTeam() != GetTeam() )
            {
            //AudioManager.instance.Play("HitShield");
            //destroy other projectiles
            other.gameObject.SetActive(false);
            AbsorbAmount--;
            weakenShield();
            //Set how transparent the shield should be based on how many charges it has left
           
        }
    }

    public void weakenShield()
    { //sets the alpha visual to whatever current absorb amount is at.
        if (AbsorbAmount > 1)
        {
            TargetAlpha = 0.5f;
        }
        if (AbsorbAmount == 1)
        {
            TargetAlpha = 0.5f;
        }
        if (AbsorbAmount < 1)
        {
            TargetAlpha = -0.1f;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
