using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeBuff : Ability
{

    [SerializeField]
    int AddEnergy = 2;
    [SerializeField]
    int Addhealth = 2;
    public ParticleSystem JokeTick;

    public int ticksleft;

    #region TickSystem
    new public void OnEnable()
    {//join the OnTick family
        GameManager.OnTick += RecieveTick;
    }

    new public void OnDisable()
    {//leave the Tick family
        GameManager.OnTick -= RecieveTick;
    }

    new public void OnDestroy()
    {//leave the Tick family
        GameManager.OnTick -= RecieveTick;
    }
    #endregion


    private void Start()
    {
        ticksleft = 101;
        if(GetComponentInParent<JokeBuff>())
        {
            
        }
    }

    new void RecieveTick()
    {
        ticksleft--;

        if (ticksleft < 10)
            gameObject.SetActive(false);

        if (ticksleft % 10 != 0)
            return;

       
        base.RecieveTick();

        GetTarget().GetComponent<Cell>().SetEnergy(GetTarget().GetComponent<Cell>().GetEnergy() + AddEnergy);
        GetTarget().GetComponent<Cell>().SetHealth(GetTarget().GetComponent<Cell>().GetHealth() + Addhealth);

        Instantiate(JokeTick, transform.position, Quaternion.identity);
    }
}
