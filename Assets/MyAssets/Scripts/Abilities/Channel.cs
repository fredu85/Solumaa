using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : Ability
{
    float totaldistance = 0;
    Vector3 upvector = new Vector3(0, 0.02f, 0);
    int count;
    
   // EnumType = Ability.AbilityType.Channel;

    private new void Awake()
    {
        base.Awake();
        SetCost(100);
        SetSpeed(50);
    }

    // Start is called before the first frame update
    void Start()
    {
        totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
    }
    new private void OnEnable()
    {
        base.OnEnable();
        if(GetTarget())
        totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        upvector = new Vector3(0, 0.02f, 0);
    }

    private void FixedUpdate()
    {
        if (GetTarget() == null)
        {//do nothing if no target
            return;
        }
       upvector.y = (Vector3.Distance(transform.position, GetTarget().transform.position) / totaldistance) / 20;

        //movement towards the target
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f)+upvector;

    }

    public new void RecieveTick()
    {

          base.RecieveTick();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GetSender())
        {
            return;
        }
        //if other was player or ai (cant hit other projectiles for instance)
      //  if (other.CompareTag("Player") || other.CompareTag("AI"))

        if(other.gameObject == GetComponent<AbilityBase>().GetTarget())
        {//call function in <Ability>

            //set other GaMeobject energy to whatever it was +X
            if (other.gameObject.GetComponent<Player>())
            {//if player would get more than max energy set energy to max energy
               if( other.GetComponent<Player>().GetEnergy() + GetDamage() > other.GetComponent<Player>().MaxEnergy)
                {
                    other.GetComponent<Cell>().SetEnergy(other.gameObject.GetComponent<Player>().MaxEnergy-GetDamage());
                }
            }
          
            other.GetComponent<Cell>().SetEnergy(other.GetComponent<Cell>().GetEnergy() + GetDamage());

            gameObject.SetActive(false);
        }
    }

}
