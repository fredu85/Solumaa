using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected GameObject sender;
    [SerializeField]
    protected int team = -1;
    [SerializeField]
    protected int damage = 0;
    [SerializeField]
    protected int heal = 0;
    [SerializeField]
    protected int Speed;
    [SerializeField]
    protected int bonusDamage;

    public string sSender = "Unknown";
    public GameManager Manager;
    public ObjectPoolScriptable objectPool;
    public ObjectList Pool;
    
    [System.Serializable]
    public enum AbilityTypeEnum
    {
        Attack, Defend, Utility
    }
    public AbilityTypeEnum Type;

    [System.Serializable]
    public enum AbilityTargetEnum
    {
        Friend, Enemy, Self
    }
    public AbilityTargetEnum TargetType;

    Color _TeamColor;
    public Color TeamColor
    {
        get { return _TeamColor; }
        set { _TeamColor = value; }
    }

    #region Getters and Setters
    #region Target
    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }
    #endregion

    #region BonusDamage
    public int GetBonusDamage()
    {
        return bonusDamage;
    }

    public void SetBonusDamage(int bDMG)
    {
        bonusDamage = bDMG;
    }
    #endregion

    #region Sender
    public GameObject GetSender()
    {
        return sender;
    }

    public void SetSender(GameObject s)
    {
        sender = s;
    }
    #endregion

    #region Damage
    public void SetDamage(int d)
    {
        damage = d;
    }

    public int GetDamage()
    {
        return damage;
    }
    #endregion

    #region Heal
    public void SetHeal(int h)
    {
        heal = h;
    }

    public int GetHeal()
    {
        return heal;
    }
    #endregion

    #region Speed
    public int GetSpeed()
    {
        return Speed;
    }
    public void SetSpeed(int s)
    {
        Speed = s;
    }
    #endregion

    #region Team
    public int GetTeam()
    {
        return team;
    }

    public void SetTeam(int t)
    {
        team = t;
    }
    #endregion
    #endregion

    #region TickSystem
    public void OnEnable()
    {//join the OnTick family
        SetBonusDamage(0);
        GameManager.OnTick += RecieveTick;
    }

    public void OnDisable()
    {//leave the Tick family
        RunOnDisable();
    }

    public void RunOnDisable()
    {
        if (objectPool != null && Pool != null)
            objectPool.AddGameObject(gameObject);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GameManager.OnTick -= RecieveTick;
    }

    public void OnDestroy()
    {//leave the Tick family
        GameManager.OnTick -= RecieveTick;
    }

    public void RecieveTick()
    {
        if (GetSender() && team == -1)
        {
            team = GetSender().GetComponent<Cell>().GetTeam();
        }

        //ability-child-specific functionality goes here.
        //To be edited in each prefab separately
    }
    #endregion

    protected void Awake()
    {
        Manager = FindObjectOfType<GameManager>();
        if (GetSender())
        {
            sSender = GetSender().gameObject.GetComponent<Cell>().CellName;
            team = GetSender().gameObject.GetComponent<Cell>().GetTeam();
        }
    }

    //children of ability will call this when they hit soemthing
    public void DealDamage(GameObject t, int d)
    {
        //gets the parent class Cell which both player and AI has
        t.GetComponent<Cell>().SetHealth(t.GetComponent<Cell>().GetHealth() - d - GetBonusDamage());
        t.GetComponent<Cell>().HitAnim();
        t.GetComponent<Cell>().UpdateColor();
    }
    //children of ability will call this when they hit soemthing
    public void Heal(GameObject t, int d)
    {
        t.GetComponent<Cell>().SetHealth(t.GetComponent<Cell>().GetHealth() + d);
        if (t.GetComponent<Cell>().GetHealth() > t.GetComponent<Cell>().GetMaxHealth())
        {
            t.GetComponent<Cell>().SetHealth(t.GetComponent<Cell>().GetMaxHealth());
            t.GetComponent<Cell>().UpdateColor();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
