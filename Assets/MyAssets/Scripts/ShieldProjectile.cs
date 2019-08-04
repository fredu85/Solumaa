using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : Ability
{
    protected float totaldistance = 0;
    protected Vector3 upvector = new Vector3(0, 0, 0);
  //  Vector3 lastpos = Vector3.zero;
    public GameObject ShieldStationary;

    public LayerMask mask;

    protected new void Awake()
    {
        //AudioManager.instance.Play("StartAbility");
        base.Awake();
        SetSpeed(75);
        GetComponent<Ability>().AbilityName = "Shield";

    }
    protected new void OnEnable()
    {
        base.OnEnable();
        if (GetTarget() != null)
            totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        upvector = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    protected void Start()
    {
        totaldistance = Vector3.Distance(transform.position, GetTarget().transform.position);
    }

    protected void Update()
    {
        ShowTravelPath();
    }

    protected void FixedUpdate()
    {
        if (GetTarget() == null)
        {//do nothing if no target
            return;
        }
        float distance = Vector3.Distance(transform.position, GetTarget().transform.position);
        if (distance > 0.5)
            upvector.y = (Vector3.Distance(transform.position, GetTarget().transform.position) / totaldistance) * 0.05f;
        else
            upvector.y = 0;

        //movement towards the target
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f) + upvector;

    }

    public new void RecieveTick()
    {
        base.RecieveTick();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cell>())
        {
            if (other.gameObject == GetTarget())
            {//call function in <Ability>

                GameObject go= Instantiate(ShieldStationary, GetTarget().transform.position, Quaternion.identity);
                go.transform.parent = GetTarget().transform;
                go.GetComponent<AbilityBase>().TeamColor = TeamColor;
                go.GetComponent<AbilityBase>().SetTarget(GetTarget());
                go.GetComponent<AbilityBase>().SetTeam(GetTeam());
                gameObject.SetActive(false);
            }
        }
    }
}
