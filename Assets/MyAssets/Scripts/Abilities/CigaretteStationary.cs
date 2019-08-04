using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigaretteStationary : Ability
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> targets = new List<GameObject>();
    int TicksLeft = 125;

    public GameObject smoke;

    new void OnEnable()
    {//join the OnTick family
        base.OnEnable();
        GameManager.OnTick += RecieveTick;
    }

    new void OnDisable()
    {//leave the Tick family
        base.OnDisable();
        GameManager.OnTick -= RecieveTick;
    }

    new private void OnDestroy()
    {//leave the Tick family
        base.OnDestroy();
        GameManager.OnTick -= RecieveTick;
    }

    private void Start()
    {
        List<GameObject> previousCigarettes = new List<GameObject>();
        // foreach (GameObject go in transform.parent.transform)
        foreach (Transform t in GetTarget().GetComponentsInChildren<Transform>(true) ) 
        {
            if(t.GetComponent<CigaretteStationary>())
            previousCigarettes.Add(t.gameObject);
        }
        float rotationangle = -15f;
        transform.Rotate(new Vector3(transform.rotation.x,
                                     transform.rotation.y,
                                     transform.rotation.z + rotationangle * previousCigarettes.Count));
    }

    new void RecieveTick()
    {
       base.RecieveTick();

        if(TicksLeft < 0)
        {
            gameObject.SetActive(false);
        }

        TicksLeft--;

        if(TicksLeft % 20 == 0)
        {
            targets.Clear();
            foreach (GameObject g in Players.Items)
            {
                if (g.GetComponent<Cell>().GetTeam() == GetTarget().GetComponent<Cell>().GetTeam())
                {
                    targets.Add(g);
                }
            }

            foreach (GameObject enemy in targets)
            {
                smoke.SetActive(true);
                DealDamage(enemy, GetDamage());
            }
        }

    }

}
