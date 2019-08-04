using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaProjectile : AbilityBase
{
    private bool _used = false;
    public bool Used { get { return _used; } set { _used = value; } }

    bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } set { _hasTarget = value; } }
    
    void OnTriggerEnter(Collider other)
    {
        //hitObject.Invoke(this.gameObject, other);
        if (other.gameObject.GetComponent<Cell>())
        {
            if (other.gameObject == GetTarget())
            {
                DealDamage(other.gameObject, GetDamage());
                gameObject.SetActive(false);
            }
        }
    }

    public void GoToTarget(GameObject abilityTarget)
    {
        if (abilityTarget.activeInHierarchy)
        {
            SetTarget(abilityTarget);
            HasTarget = true;
        }
    }

    private void FixedUpdate()
    {
        if (HasTarget)
        {
            float distanceFloat = Vector3.Distance(transform.position, GetTarget().transform.position);
            float moveDistanceScale = 1000.0f;
            transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position, GetSpeed() / moveDistanceScale);
        }
    }

    new void Awake()
    {
        base.Awake();
        SetSpeed(100);
    }

    private new void OnEnable()
    {
        base.OnEnable();
        Used = false;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        Used = true;
        HasTarget = false;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
