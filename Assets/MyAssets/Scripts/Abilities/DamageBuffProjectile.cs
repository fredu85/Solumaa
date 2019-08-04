using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuffProjectile : Ability
{
    float originalDistance = 0;
    Vector3 upVector = new Vector3(0, 0.02f, 0);
    Vector3 lastPos = Vector3.zero;
    public LayerMask mask;
    public GameObject DamageBuff;

    private new void Awake()
    {
        base.Awake();
    }
    new private void OnEnable()
    {
        base.OnEnable();

        upVector = new Vector3(0, 0.02f, 0);
        lastPos = Vector3.zero;
    }

    void Start()
    {
        originalDistance = Vector3.Distance(transform.position, GetTarget().transform.position);
    }

    private void Update()
    {
        ShowTravelPath();
    }

    private void FixedUpdate()
    {
        if (GetTarget() == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / 1000f) + upVector;
    }

    public new void RecieveTick()
    {
        base.RecieveTick();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cell>())
        {
            if (other.gameObject == GetTarget())
            {
                Vector3 startpos = new Vector3(0, 1, 0);
                GameObject go = Instantiate(DamageBuff, other.gameObject.transform.position + startpos, Quaternion.identity);
                go.GetComponent<AbilityBase>().SetTarget(other.gameObject);
                go.GetComponent<AbilityBase>().SetTeam(GetTeam());
                go.transform.parent = other.gameObject.transform;
                gameObject.SetActive(false);
            }
        }
    }
}
