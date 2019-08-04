using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBerry : Ability
{
    float totaldistance = 0;
    Vector3 upvector = new Vector3(0, 0.01f, 0);
    private new void Awake()
    {
        base.Awake();
        SetSpeed(25);
     
    }

    // Start is called before the first frame update
    void Start()
    {
        totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
    }
    new private void OnEnable()
    {
        base.OnEnable();
        if(GetTarget() != null)
        totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        upvector = new Vector3(0, 0.01f, 0);
    }


    private void FixedUpdate()
    {
        if (GetTarget() == null)
        {//do nothing if no target
            return;
        }
        float distance = Vector3.Distance(transform.position, GetTarget().transform.position);
        if (distance > 0.5)
            upvector.y = (Vector3.Distance(transform.position, GetTarget().transform.position) / totaldistance) * 0.04f;
        else
            upvector.y = 0;

        //movement towards the target
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f) + upvector;


        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * 120f));

        //movement towards the target
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f);

    }

    public new void RecieveTick()
    {

       base.RecieveTick();

    }

    private void OnTriggerEnter(Collider other)
    {

        //if other was player or ai (cant hit other projectiles for instance)
        // if (other.CompareTag("Player") || other.CompareTag("AI"))
        if (other.gameObject == GetComponent<AbilityBase>().GetTarget())
        {//call function in <Ability>
          
            Heal(other.gameObject, GetHeal());
            StartCoroutine(Impact());

        }
    }

    IEnumerator Impact()
    {
        yield return new WaitForSeconds(0.0f);
        //AudioManager.instance.Play("HealBerryHit");
        gameObject.SetActive(false);
    }

}
