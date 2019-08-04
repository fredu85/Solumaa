using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    //inherited by player and AI
    string _cellName = "Heikki";
    public string CellName
    {
        set { _cellName = value; }
        get { return _cellName; }
    }

    [HideInInspector]
    public int Gems { set; get; }
    [HideInInspector]
    public int AbilityTYpe { set; get; }

    [HideInInspector]
    public List<GameObject> _Friends = new List<GameObject>();
    public List<GameObject> Friends
    {
        get { return _Friends; }
        set { _Friends = value; }
    }

    [HideInInspector]
    public List<GameObject> _Enemies = new List<GameObject>();
    public List<GameObject> Enemies
    {
        get { return _Enemies; }
        set { _Enemies = value; }
    }

    bool _isFiring;
    public bool isFiring
    {
        get { return _isFiring; }
        set { _isFiring = value; }
    }

    int _cost;
    [HideInInspector]
    public int Cost {
        get { return _cost; }
        set { _cost = value; }
    }


    int _paid;
    [HideInInspector]
    public int Paid
    {
        get { return _paid; }
        set { _paid = value; }
    }

    [SerializeField]
    Color _TeamColor;
    public Color TeamColor
    {
        get { return _TeamColor; }
        set { _TeamColor = value; }
    }


    public ObjectList Players;
    //public RuntimeSet Players;
 
    
   // public GameObject CurrentAbility = null;
    //  public ChosenAbility chosenAbility;
    public GameObject chosenAbility;
    public CellObject ThisCellObject;
    public Animator CellAnim;
    public GameObject thingthatchangescolor;
    public GameObject HitVFX;

    [SerializeField]
    int Energy = 0;
    [SerializeField]
    int maxEnergy = 1000;

    [SerializeField]
    int Health = 100;
    [SerializeField]
    int maxHealth = 100;

    int team;
    public GameManager gameManager;

    bool isTarget;

    public GameObject targetvfx;
    public FireAbility fireScript;
    public AudioSource SoundsComesFromHere;
    public SimpleAudioEvent _SpawnSound;
    public SimpleAudioEvent _GetHitSound;

    public GameObject chosenTarget;
    //public GameObjectDataEvent gameObjectDataEvent;

    public GameObjectData _GameObjectData;
    public GameObjectDataEvent _gameObjectDataEvent;
    public ObjectList StartingAbilities;


    #region Health
    public int GetHealth()
    { 
        return Health;
    }

    public void SetHealth(int h)
    {
        Health = h;

        if(Health <0)
        {
            if (ThisCellObject != null)
            ThisCellObject.IsAlive = false;

            kill();
        }

        if(Health > maxHealth)
        {
            Health = maxHealth;
        }

        if(ThisCellObject != null)
        ThisCellObject.Health = Health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }
    #endregion

    public void kill()
    {
        Destroy(gameObject);
    }

    #region Energy
    public int GetEnergy()
    {
        return Energy;
    }

    public void SetEnergy(int e)
    {
        Energy = e;
        if(Energy > maxEnergy)
        {
            Energy = maxEnergy;
        }
    }
    #endregion

    #region team
    public int GetTeam()
    {
        return team;
    }

    public void SetTeam(int t)
    {
        team = t;
    }
    #endregion team

    #region Target
    public GameObject GetTarget()
    {
        return chosenTarget;
    }

    public void SetTarget(GameObject t)
    {
        chosenTarget = t;
    }
    #endregion team

    #region gameManager
    public GameManager GetManager()
    {
        return gameManager;
    }

    public void SetManager(GameManager m)
    {
        gameManager = m;
    }
    #endregion

    #region isCurrentTarget
    public bool isCurrentTarget()
    {
        return isTarget;
    }

    public void SetisCurrentTarget(bool b)
    {
        isTarget = b;
    }
    #endregion

    public void CheckifisTarget() //VFX spawn on mouseover
    {
        if (isTarget)
        {
            if (targetvfx != null)
            {
                targetvfx.SetActive(true);
            }
        }
        else
        {
            if (targetvfx != null)
                targetvfx.SetActive(false);
        }

        isTarget = false;
    }

    public void HitAnim()
    {
        CellAnim.SetTrigger("TakeHit");
        if(HitVFX)
        {
            HitVFX.SetActive(true);
            _GetHitSound.Play(SoundsComesFromHere);
        }
    }

    public void UpdateColor()
    {
        if (_TeamColor != null)
            thingthatchangescolor.GetComponent<Renderer>().material.color = _TeamColor * (GetHealth()) / GetMaxHealth();
    }

    public void PlayUseAbilitySound()
    {
        _SpawnSound.Play(SoundsComesFromHere);
    }

    public void UpdateLists()
    {
        Friends.Clear();
        Enemies.Clear();

        if (Players.GetItems() != null)
            foreach (GameObject p in Players.Items)
            {
                if (p.GetComponent<Cell>().GetTeam() == gameObject.GetComponent<Cell>().GetTeam())
                {
                    Friends.Add(p);
                }
                if (p.GetComponent<Cell>().GetTeam() != gameObject.GetComponent<Cell>().GetTeam())
                {
                    Enemies.Add(p);
                }
            }

    }
}
