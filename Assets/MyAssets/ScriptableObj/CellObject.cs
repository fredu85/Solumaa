using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CellObject", order = 52)]
public class CellObject : ScriptableObject
{

    //string
    [SerializeField]
    string _name="Heikki";
    public string CellName
        {
        get { return _name; }
        set {_name = value;}
    }

    [SerializeField]
    string _action = "Playing Hearthstone";
    public string CurrentAction
    {
        get { return _action; }
        set { _action = value; }
    }

    //Vector3

    [SerializeField]
    Vector3 _pos;
    public Vector3 Pos
    {
        get { return _pos; }
        set { _pos = value; }
    }

    //int

    [SerializeField]
    int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    [SerializeField]
    int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    int _energy;
    public int Energy
    {
        get { return _energy; }
        set { _energy = value; }
    }

    [SerializeField]
    int _gems;
    public int Gems
    {
        get { return _gems; }
        set { _gems = value; }
    }

    [SerializeField]
    int _paid;
    public int Paid
    {
        get { return _paid; }
        set { _paid = value; }
    }

    [SerializeField]
    int _cost;
    public int Cost
    {
        get { return _cost; }
        set { _cost = value; }
    }


    [SerializeField]
    int _steps;
    public int Steps
    {
        get { return _steps; }
        set { _steps = value; }
    }

    //bool

    [SerializeField]
    bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    [SerializeField]
    bool _isFiring = false;
    public bool IsFiring
    {
        get { return _isFiring; }
        set { _isFiring = value; }
    }

    [SerializeField]
    bool _hasTarget = true;
    public bool HasTarget
    {
        get { return _hasTarget; }
        set { _hasTarget = value; }
    }

    [SerializeField]
    bool _canSelectTarget;
    public bool CanSelectTarget
    {
        get { return _canSelectTarget; }
        set { _canSelectTarget = value; }
    }

    public void Refresh(GameObject go)
    {
        if (!go.GetComponent<Cell>())
            return;

        _name = go.GetComponent<Cell>().CellName;
        _health = go.GetComponent<Cell>().GetHealth();
        _gems = go.GetComponent<Cell>().Gems;
        _energy = go.GetComponent<Cell>().GetEnergy();
        _cost = go.GetComponent<Cell>().Cost;
        _paid = go.GetComponent<Cell>().Paid;
        if (go.GetComponent<Cell>().chosenAbility)
            _action = go.GetComponent<Cell>().chosenAbility.GetComponent<Ability>().GetName();
        else
            _action = " - ";
       // ThisCellObject.Pos = transform.position;
      //  ThisCellObject.IsFiring = false;
       // ThisCellObject.IsAlive = true;
       // ThisCellObject.CanSelectTarget = true;
    }

}
