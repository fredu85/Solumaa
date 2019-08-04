using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : Ability
{
    [SerializeField]
    Vector3 targetrot;
    float rotspeed = 2f;

    Quaternion lookv;

    Vector3 vtarget;

    public LayerMask mask;

    bool blockobject;
    bool checkCollisionright;
    bool checkCollsionleft;
    public BoxCollider SelfCollider;

    new private void OnEnable()
    {
        if(GetTarget() != null)
        transform.LookAt(GetTarget().transform.position);
        //  transform.position =transform.position+ transform.forward;
        vtarget = transform.position + transform.forward * 2;
    }


    private void Start()
    {//TODO make scientist "Jump" at start;
        // vtarget = new GameObject("vtarget"); //saved incase of debugging needs later
      transform.LookAt(GetTarget().transform.position);
      //  transform.position =transform.position+ transform.forward;
        vtarget = transform.position + transform.forward * 2;
        SetSpeed(20);

    }

    private void Update()
    {
        ShowTravelPath();

    }


    private void FixedUpdate()
    {
        if (!GetTarget())
            GetNewTarget();

            //turning
            Quaternion lookatvtarget = Quaternion.LookRotation(vtarget - transform.position, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookatvtarget, Time.deltaTime * rotspeed);


            RaycastHit objectHit;

            Vector3 fwd = transform.forward * 1f;

            float vectoroffset = 0.25f;

            Vector3 rightRay = new Vector3(transform.position.x - vectoroffset, transform.position.y - 0.75f, transform.position.z + vectoroffset);
            Vector3 leftRay = new Vector3(transform.position.x + vectoroffset, transform.position.y - 0.75f, transform.position.z - vectoroffset);

            Debug.DrawRay(rightRay, fwd, Color.red);
            Debug.DrawRay(leftRay, fwd, Color.green);



        blockobject = false;
        //if (Physics.Raycast(leftRay, fwd, out objectHit, 0.5f, mask) && Physics.Raycast(rightRay, fwd, out objectHit, 0.5f, mask))
        //{
        //    if (objectHit.collider.gameObject != GetTarget() && objectHit.collider != SelfCollider)
        //    {
        //        SetNewTargetAtAngle(transform.eulerAngles.y + 120);
        //        blockobject = true;
        //        StartCoroutine(StopCheck());
        //    }
        //}
        //else
        //{

            if (Physics.Raycast(leftRay, fwd, out objectHit, 0.75f, mask) && checkCollsionleft)
            {

                if (objectHit.collider.gameObject != GetTarget() && objectHit.collider != SelfCollider)
                {
                    SetNewTargetAtAngle(transform.eulerAngles.y + 120);
                checkCollisionright = false;
                StartCoroutine(StartCheckRight());
                blockobject = true;
                }
            }
            if (Physics.Raycast(rightRay, fwd, out objectHit, 0.75f, mask) && checkCollisionright)
            {
                if (objectHit.collider.gameObject != GetTarget() && objectHit.collider != SelfCollider)
                {
                    SetNewTargetAtAngle(transform.eulerAngles.y - 120);
                checkCollsionleft = false;
                StartCoroutine(StartCheckLeft());
                
                blockobject = true;
                }
            }
        //}
        
         if(!checkCollisionright && !checkCollsionleft)
        {
            checkCollsionleft = true;
        }

            //if close to the target gameobject set it as the vtarget
            if (Vector3.Distance(gameObject.transform.position, GetTarget().transform.position) < 0.3f)
            {
                vtarget = GetTarget().transform.position;
            }
            else
            {
                if (!blockobject)
                    Moveforward();
            }

            if (vtarget.y != 1)
            {
                vtarget.y = 1;
            }
            if (transform.position.y > 1.2f)
            {
                
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            }

        }

    IEnumerator StartCheckRight()
    {
        yield return new WaitForSeconds(1f);
        checkCollisionright= true;
    }
    IEnumerator StartCheckLeft()
    {
        yield return new WaitForSeconds(1f);
        checkCollsionleft = true;
    }


    void Moveforward()
    {
        //if scientist reaches target, make a new one
        if (Vector3.Distance(gameObject.transform.position, vtarget) < 1f)
        {
            SetNewTarget(-45, 45, 2, 4);
        }

        transform.position = transform.position + transform.forward * GetSpeed() / 1000;

    }

    //set new location for vtarget
    void SetNewTarget(float min, float max, float distmin, float distmax)
    {
        lookv = Quaternion.LookRotation(GetTarget().transform.position - transform.position, Vector3.up);

        if (Vector3.Distance(transform.position, GetTarget().transform.position) > 1)
        {
            float angle = lookv.eulerAngles.y;

            float randomangle = Random.Range(min, max);
            angle = angle + randomangle;

            float distance = Random.Range(distmin, distmax);

            vtarget = new Vector3(distance * Mathf.Sin(angle * Mathf.Deg2Rad) + transform.position.x,
                                  transform.position.y,
                                 distance * Mathf.Cos(angle * Mathf.Deg2Rad) + transform.position.z);

        }
        else
            vtarget = new Vector3(GetTarget().transform.position.x, transform.position.y, GetTarget().transform.position.z);
    }


    //set a vtarget location at certain angle
    void SetNewTargetAtAngle(float specificangle)
    {

        float angle = specificangle;

        vtarget = new Vector3(1 * Mathf.Sin(angle * Mathf.Deg2Rad) + transform.position.x,
                              transform.position.y,
                             1 * Mathf.Cos(angle * Mathf.Deg2Rad) + transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AbilityBase>())
        {
            if (other.gameObject.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Attack))
                return;
            if (other.gameObject.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Utility))
                return;
            if (other.gameObject.GetComponent<AbilityBase>().Type.Equals(Ability.AbilityTypeEnum.Defend))
            {
                if (other.gameObject.GetComponent<AbilityBase>().GetTarget() == GetTarget())
                    gameObject.SetActive(false);
            }
        }
        //if other was player or ai (cant hit other projectiles for instance)
        // if(other.CompareTag("Player") || other.CompareTag("AI"))
        if (other.gameObject.GetComponent<Cell>())
        {
            if (other.gameObject == GetTarget())
            {//call function in <Ability>
                DealDamage(other.gameObject, GetDamage());
                Manager.ActionDamage(
                    gameObject.GetComponent<Ability>().AbilityName,
                    other.gameObject.GetComponent<Cell>().CellName,
                    gameObject.GetComponent<AbilityBase>().GetDamage().ToString(),
                    sSender);


                //AudioManager.instance.Play("TakeHit");
                gameObject.SetActive(false);
            }
        }
    }
}

