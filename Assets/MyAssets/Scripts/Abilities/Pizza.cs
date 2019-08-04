using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : Ability
{
    bool inCenter = false; //When ability is in center of targets, it will start receiving tickevents
    float distanceThreshold = 0.5f; //percentage
    float originalDistance = 0;
    Vector3 upVector = new Vector3(0, 0.02f, 0);
    Vector3 enemyCenter;
    int ticksLeft;
    int projectileCounter = 0;
    public LayerMask mask;

    [SerializeField]
    float projectileRate = 2.0f;
    [SerializeField]
    float floatDistance = 5.0f;
    [SerializeField]
    List<GameObject> projectileList = new List<GameObject>();
    [SerializeField]
    GameObject spinningPlate;
    [SerializeField]
    GameObject projectileRoot;

    float moveDistanceScale = 1000.0f;

    private new void Awake()
    {
        base.Awake();
        SetSpeed(100);
    }

    private new void OnEnable()
    {
        base.OnEnable();
        GameManager.OnTick += RecieveTick;

        //Calculate shooting time based on amount of projectiles and fire rate
        ticksLeft = Mathf.RoundToInt(projectileRate * projectileList.Count * 20) + 10; //10 is for slight delay

        //Reset projectiles
        GameObject tempProjectile;
        for (int i = 0; i < projectileList.Count; i++)
        {
            tempProjectile = projectileList[i];
            //tempProjectile.GetComponent<PizzaProjectile>().hitObject += ProjectileCollision;
            tempProjectile.gameObject.SetActive(true);
            tempProjectile.gameObject.transform.parent = spinningPlate.transform; //Change projectile gameobject parent to spin in with
            tempProjectile.transform.position = this.gameObject.transform.position;
            tempProjectile.transform.localRotation = Quaternion.identity;
            //tempProjectile.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (GetTarget() != null)
            originalDistance = Vector3.Distance(transform.position, GetTarget().transform.position);
        upVector = new Vector3(0, 0.02f, 0);
        projectileCounter = 0;
    }

    private void FixedUpdate()
    {
        if (GetTarget() == null)
        {
            return;
        }

        float distanceFloat = Vector3.Distance(transform.position, enemyCenter);

        //Check to see if in the middle position above enemy
        if (!inCenter && distanceFloat <= 0.5f)
        {
            inCenter = true;
        }
        else
        {
            upVector.y = distanceFloat / originalDistance / 20;
            transform.position = Vector3.MoveTowards(transform.position, enemyCenter, GetSpeed() / moveDistanceScale) + upVector;
        }

        //Check if either in center or designated threshold percentage away from center to rotate
        if (distanceFloat <= 0.5f)
        {
            spinningPlate.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 120f));
        }
        else if ((distanceFloat / originalDistance) < distanceThreshold)
        {
            spinningPlate.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 120f));
        }
    }

    public new void RecieveTick()
    {
        //Check if ran out of time
        if (ticksLeft < 0)
        {
            bool canRemove = true;
            foreach (var proj in projectileList)
            {
                if (proj.activeInHierarchy)
                    canRemove = false;
            }

            //Check if all projectiles have hit to be able to deactive gameobject
            if (canRemove)
            {
                foreach (var proj in projectileList)
                {
                    proj.GetComponent<PizzaProjectile>().Used = false;
                }
                gameObject.SetActive(false);
            }
        }

        if (inCenter)
        {
            ticksLeft--;
        }

        base.RecieveTick();

        if (ticksLeft % Mathf.RoundToInt(projectileRate * 20) == 0 && projectileCounter != projectileList.Count)
        {
            //Enable first inactive projectile, reposition to parent, send towards random enemy target
            GetNewTarget();

            PizzaProjectile tempProjectile = null;
            foreach (var proj in projectileList)
            {
                if (proj.activeInHierarchy)
                {
                    tempProjectile = proj.GetComponent<PizzaProjectile>();
                    if (!tempProjectile.Used && !tempProjectile.HasTarget)
                    {//Choose first active in-game non-used projectile
                        proj.transform.position = this.gameObject.transform.position;
                        proj.transform.parent = projectileRoot.transform; //Change gameobject root to stop spinning
                        proj.SetActive(true);
                        tempProjectile.SetDamage(this.GetDamage());
                        tempProjectile.GoToTarget(GetTarget());
                        /*
                        if (!tempProjectile.HasTarget)
                        {
                            GetNewTarget();
                            tempProjectile.GoToTarget(GetTarget());
                        }
                        */
                        projectileCounter++;
                        break;
                    }
                    else
                        continue;
                }
            }
        }
    }

    private new void OnDisable()
    {
        base.OnDisable();
        inCenter = false;
        GameManager.OnTick -= RecieveTick;
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        inCenter = false;
        GameManager.OnTick -= RecieveTick;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get enemy group center with some randomness
        //List<GameObject> enemies = new List<GameObject>();
        if (Players.GetItems() != null)
        {
            float totalX, totalZ;
            totalX = totalZ = 0f;
            int enemyCount = 0;
            foreach (GameObject p in Players.Items)
            {
                if (p.GetComponent<Cell>().GetTeam() != GetTeam())
                {
                    //enemies.Add(p);
                    totalX += p.transform.position.x;
                    totalZ += p.transform.position.z;
                    enemyCount++;
                }
            }

            float rngX = Random.Range(-1.5f, 1.5f);
            float rngY = Random.Range(-0.5f, 0.5f);
            float rngZ = Random.Range(-1.5f, 1.5f);
            enemyCenter = new Vector3(rngX + (totalX / enemyCount), rngY + floatDistance, rngZ + (totalZ / enemyCount));
        }
        originalDistance = Vector3.Distance(transform.position, enemyCenter);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
