using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditorInternal;

public class Cigarette : Ability
{
    float totaldistance = 0;
    Vector3 upvector = new Vector3(0, 0.02f, 0);
   // public LineRenderer TravelPath;
    //int vertexCount = 15;
    //public Vector3 point1;
    //public Vector3 point2;
    //public Vector3 point3;
    public GameObject CigaretteStationary;

    public LayerMask mask;

    Vector3 lastPosition = Vector3.zero;

    float Zrot;

    new private void OnEnable()
    {
        base.OnEnable();
        if (GetTarget() != null)
            totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        upvector = new Vector3(0, 0.02f, 0);
    }



    private new void Awake()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0,0,90));


        base.Awake();
        SetSpeed(50);
        GetComponent<Ability>().AbilityName = "Cigarette";

    }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GetTarget().transform.position);
        totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        //point1 = gameObject.transform.position;
        //point3 = GetTarget().transform.position;
    }

    private void Update()
    {
        ShowTravelPath();

    }

    private void FixedUpdate()
    {
        if (GetTarget() == null)
        {//do nothing if no target
            return;
        }
        float distancefloat = Vector3.Distance(transform.position, GetTarget().transform.position);
        upvector.y = distancefloat / totaldistance / 20;

        //movement towards the target
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f) + upvector;

        //rotation


        float direction = (transform.position.x- lastPosition.x);
        lastPosition = transform.position;
        if (direction > 0)
            Zrot = Zrot - 5;
        else if(direction < 0)
            Zrot = Zrot + 5;
           // Zrot = 40;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Zrot));

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
        // if(other.CompareTag("Player") || other.CompareTag("AI"))
        if (other.gameObject == GetComponent<AbilityBase>().GetTarget())
        {//call function in <Ability>
         
            DealDamage(other.gameObject, GetDamage());
            Manager.ActionDamage(
                gameObject.GetComponent<Ability>().AbilityName,
                other.gameObject.GetComponent<Cell>().CellName,
                gameObject.GetComponent<AbilityBase>().GetDamage().ToString(),
                gameObject.GetComponent<AbilityBase>().GetSender().gameObject.GetComponent<Cell>().CellName);
            //spawn the cigarettestationry
            //AudioManager.instance.Play("TakeHit");
            GameObject go = Instantiate(CigaretteStationary, GetTarget().transform.position, Quaternion.identity);
            go.GetComponent<AbilityBase>().SetTarget( GetTarget() );
            go.transform.parent = GetTarget().transform;
            gameObject.SetActive(false);
        }
    }
}
