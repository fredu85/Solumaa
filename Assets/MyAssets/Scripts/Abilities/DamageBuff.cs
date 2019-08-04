using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : AbilityBase
{
    //Bonus damage variables
    [SerializeField]
    protected int AddDamage = 5;
    [SerializeField]
    protected float AddDamagePercent = 0.5f;
    public ParticleSystem DamageFX;

    [SerializeField]
    protected int duration;
    [SerializeField]
    protected int buffDelay;
    int buffDelayCounter;
    bool buffUsed;

    new public void OnEnable()
    {
        buffUsed = false;
        GameManager.OnTick += RecieveTick;
    }

    new public void OnDisable()
    {
        GameManager.OnTick -= RecieveTick;
    }

    new public void OnDestroy()
    {
        GameManager.OnTick -= RecieveTick;
    }

    private void Start()
    {

    }

    new void RecieveTick()
    {
        duration--;

        if (duration <= 0)
            gameObject.SetActive(false);

        //If the buff has been used, count down cooldown
        if (buffUsed)
        {
            buffDelayCounter--;
            if (buffDelayCounter <= 0)
            {
                buffUsed = false;
            }
        }
        //Instantiate(DamageFX, transform.position, Quaternion.identity);
    }

    //Method for checking if cast ability can be buffed (checks on every ability)
    public void CheckForBuff(GameObjectData data)
    {
        if (!buffUsed)
            if (data.Sender == GetTarget())
                if (data.AbilityPrefab.GetComponent<AbilityBase>().Type == AbilityTypeEnum.Attack)
                {
                    //Change bonus damage variable in ability that is always 0 when enabled
                    data.AbilityPrefab.GetComponent<AbilityBase>().SetBonusDamage(
                        Mathf.RoundToInt(data.AbilityPrefab.GetComponent<AbilityBase>().GetDamage() * AddDamagePercent));

                    /* Attempt to in transit change ability prefab when a cell fires an ability by instantiating a copy with changed damage, didn't work
                    GameObject tempGameObject = Instantiate(data.AbilityPrefab, data.AbilityPrefab.transform.parent);
                    AbilityBase tempAbility = tempGameObject.GetComponent<AbilityBase>();
                    tempAbility.Pool.Items.Remove(tempGameObject);
                    tempAbility.SetDamage(Mathf.RoundToInt(tempAbility.GetDamage() + tempAbility.GetDamage() * AddDamagePercent));
                    tempGameObject.name = data.AbilityPrefab.name;
                    data.AbilityPrefab = tempGameObject;
                    */
                    buffDelayCounter = buffDelay;
                    buffUsed = true;
                }
    }
}
