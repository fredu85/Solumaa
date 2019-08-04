using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditorInternal;

public class Burger : Ability
{
    float totaldistance = 0;
    Vector3 upvector = new Vector3(0, 0.02f, 0);
    Vector3 lastpos = Vector3.zero;

    public LayerMask mask;

    private new void Awake()
    {
        base.Awake();
        SetSpeed(75);
        GetComponent<Ability>().AbilityName = "Burger";

    }
    new private void OnEnable()
    {
        base.OnEnable();

        if (GetTarget() != null)
            totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        upvector = new Vector3(0, 0.02f, 0);
        lastpos = Vector3.zero;
    }


    private void InitMethod()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
       totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        //point1 = gameObject.transform.position;
       // point3 = GetTarget().transform.position;
    }

    private void Update()
    {
        ShowTravelPath();
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 120f, 0));

        //todo, raycast to check where it should really be placed at
        if (GetTarget()==null)
        {//do nothing if no target
            return;
        }
        upvector.y = (Vector3.Distance(transform.position, GetTarget().transform.position) / totaldistance)/20;
        
        if(transform.position.x - lastpos.x > 0)
            upvector.z = (Vector3.Distance(transform.position, GetTarget().transform.position) / totaldistance) / 15;
        else
            upvector.z = -(Vector3.Distance(transform.position, GetTarget().transform.position) / totaldistance) / 15;
        lastpos = transform.position;

        //movement towards the target
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f)+upvector;


    }

    public new void RecieveTick()
    {

        base.RecieveTick();
      

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.GetComponent<AbilityBase>())
        //{
        //{
        //    if (other.gameObject.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Attack))
        //        return;
        //    if (other.gameObject.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Utility))
        //        return;
        //   if(other.gameObject.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Defend))
        //    {
        //        return;
        //    }
        //}
        //if other was player or ai (cant hit other projectiles for instance)
        // if(other.CompareTag("Player") || other.CompareTag("AI"))
        if (other.gameObject.GetComponent<Cell>())
        {
            if(other.gameObject == GetTarget())
            {//call function in <Ability>
            
                DealDamage(other.gameObject, GetDamage());
                //  AudioManager.instance.Play("TakeHit");
                //Manager.ActionDamage(
                //    gameObject.GetComponent<AbilityBase>().AbilityName,
                //    other.gameObject.GetComponent<Cell>().CellName,
                //    gameObject.GetComponent<AbilityBase>().GetDamage().ToString(),
                //    gameObject.GetComponent<AbilityBase>().GetSender().gameObject.GetComponent<Cell>().CellName);
                // Destroy(gameObject);
                gameObject.SetActive(false);

             
                
            }
        }
    }
}
