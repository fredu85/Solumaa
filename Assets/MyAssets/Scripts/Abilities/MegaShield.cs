using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShield : Ability
{

    public int AbsorbAmount;
    int ShieldColor;
    public GameObject Visual;
    Color VisualColor;
    float VisualColorAlpha = 0.0f;
    float TargetAlpha = 0.25f;

    public ParticleSystem circleboom;

    float scrollspeed = 0.01f;

    Vector3 centerpos;
    List<GameObject> friendlies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.instance.Play("ShieldStartBig");
        SetSender(GetTarget());
        transform.parent = GetTarget().transform;
        transform.Rotate(new Vector3(45, 0, 0));
        //if (GetTarget().GetComponent<Renderer>())
        //    VisualColor = GetTarget().GetComponent<Renderer>().material.color;
        //else
        //    VisualColor = GetTarget().GetComponentInChildren<Renderer>().material.color;

        VisualColor = Visual.GetComponent<Renderer>().material.color;

        foreach (GameObject player in Players.Items)
        {
            if (player.GetComponent<Cell>().GetTeam() == GetTarget().GetComponent<Cell>().GetTeam())
            {
                friendlies.Add(player);
            }
        }

        for (int i = 0; i < friendlies.Count; i++)
        {
            centerpos += friendlies[i].transform.position;
        }
        centerpos = centerpos / friendlies.Count;

        transform.position = centerpos;



        foreach (GameObject friend in friendlies)
        {
            if (friend.GetComponentInChildren<MegaShield>())
            {

                GameObject othershield = friend.GetComponentInChildren<MegaShield>().gameObject;
                if (othershield != gameObject)
                {
                    //  Debug.Log("Destroying an extra big shield");
                    othershield.SetActive(false);
                }
            }
        }

        //deal damage to its target
        DealDamage(GetTarget(), GetDamage());

    }

    // Update is called once per frame
    void Update()
    {
        if (AbsorbAmount < 1 && TargetAlpha < 0)
        {//no shield left, fade away
            if (VisualColorAlpha <= 0)
                gameObject.SetActive(false);
        }

        float offset = Time.time * scrollspeed;
        Visual.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset / 2, -offset);


        //fade towards target value
        if (VisualColorAlpha > TargetAlpha)
        {
            VisualColorAlpha = VisualColorAlpha - 0.005f;
        }
        else if (TargetAlpha > VisualColorAlpha)
        {
            VisualColorAlpha = VisualColorAlpha + 0.005f;
        }

        //set value
        VisualColor.a = VisualColorAlpha;
        Visual.GetComponent<Renderer>().material.color = VisualColor;

    }

    private void OnTriggerEnter(Collider other)
    {
        //dont collide if absorbamount is less than 1
        if (AbsorbAmount < 1)
        {
            return;
        }
        //if not an ability base or derivative dont collide
        if (other.gameObject.GetComponent<AbilityBase>() == null)
        {
            return;
        }

        //can only hit enemy projectiles
        if (other.gameObject.GetComponent<AbilityBase>().GetTeam() != GetTeam())
        {
            //AudioManager.instance.Play("HitShield");

            //destroy other projectiles
            ParticleSystem Circleboomgo = Instantiate(circleboom, other.gameObject.transform.position, Quaternion.identity);

            float distApart = (gameObject.transform.position - Circleboomgo.transform.position).magnitude;

            float fraction = (distApart - 1f) / distApart;
            Circleboomgo.transform.position = gameObject.transform.position +
                                             (Circleboomgo.transform.position - gameObject.transform.position) * fraction;

            //  Circleboomgo.transform.position = other.gameObject.transform.position

            other.gameObject.SetActive(false);
            AbsorbAmount--;
            //Set how transparent the shield should be based on how many charges it has left
            weakenShield();

        }


    }

    public void weakenShield()
    { //sets the alpha visual to whatever current absorb amount is at.

        if (AbsorbAmount < 1)
        {
            TargetAlpha = -0.1f;
        }
        else
            TargetAlpha = AbsorbAmount / 40f;
    }
}
