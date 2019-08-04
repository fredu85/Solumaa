using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class Ability : AbilityBase
{
    /* Inheriting from AbilityBase
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject sender;
    [SerializeField]
    int team = -1;
    */

    public LineRenderer TravelPath;
    Vector3 point1;
    Vector3 point2;
    Vector3 point3;

    public int Cost;
    public bool ready = false;

    public string description = "Nothing said yet";
    public string AbilityName = "Unknown Name";
    public Sprite picture;

    public bool bShowTravelPath;

    [SerializeField]
    GameObject shadow;

    [SerializeField]
    int _AbilityInt;
    public int AbilityInt
    {
        get { return _AbilityInt; }
        set { }
    }

    [System.Serializable]
    public enum AbilityPayEnum
    {
        Charge, Channel
    }
    public AbilityPayEnum PayMethod;

    public ObjectList Players;

    public new void Awake()
    {
        base.Awake();
        point1 = gameObject.transform.position;
    }


    public new void OnEnable()
    {
        base.OnEnable();
        if (shadow)
            shadow.SetActive(true);
    }

    public new void OnDisable()
    {
        base.OnDisable();
        if (shadow)
            shadow.SetActive(false);
    }

    //all the setters and getters for the above variables
    #region Getters and Setters

    #region Cost
    public int GetCost()
    {
        return Cost;
    }

    public void SetCost(int c)
    {
        Cost = c;
    }

    #endregion

    #region Description
    public string GetDescription()
    {
        return description;
    }

    public void SetDescription(string d)
    {
        description = d;
    }
    #endregion

    #region Name
    public string GetName()
    {
        return AbilityName;
    }

    public void SetName(string n)
    {
        AbilityName = n;
    }
    #endregion

    #endregion

    public new void RecieveTick()
    {
        base.RecieveTick();

        if (target == null)
        {
            GetNewTarget();
        }
    }

    public void GetNewTarget()
    {
        if (!sender)
        {
            gameObject.SetActive(false);
            return;
        }
        List<GameObject> newTarget = new List<GameObject>();
        if (TargetType == AbilityTargetEnum.Friend)
        {

            foreach (GameObject go in Players.Items)
            {
                if (go.GetComponent<Cell>().GetTeam() == GetTeam())
                {
                    newTarget.Add(go);
                }


            }
            int rng = Random.Range(0, newTarget.Count);
            SetTarget(newTarget[rng]);
            return;

        }
        if (TargetType == AbilityTargetEnum.Enemy)
        {
            foreach (GameObject go in Players.Items)
            {
                if (go.GetComponent<Cell>().GetTeam() != GetTeam())
                {
                    newTarget.Add(go);
                }
            }
            int rng = Random.Range(0, newTarget.Count);
            SetTarget(newTarget[rng]);
            return;
        }
        //if I get here set it it false
        gameObject.SetActive(false);

    }

    public void HideTravelPath()
    {
        bShowTravelPath = false;
    }

    public void ShowTravelPath()
    {
        if (bShowTravelPath)
        {
            // Debug.Log("showing travelpath for: " + GetName());
            if (!GetTarget())
            { return; }
            point3 = GetTarget().transform.position;

            TravelPath.enabled = true;
            int vertexCount = Mathf.RoundToInt(Vector3.Distance(transform.position, GetTarget().transform.position)) + 5;
            if (GetTarget())
            {
                point1 = transform.position;
                point2.x = (point1.x + point3.x) / 2;
                point2.z = (point1.z + point3.z) / 2;

                point2.y = transform.position.y;
            }
            List<Vector3> pointlist = new List<Vector3>();
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(point1, point2, ratio);
                Vector3 tangentLineVertex2 = Vector3.Lerp(point2, point3, ratio);
                Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
                pointlist.Add(bezierPoint);
            }
            TravelPath.positionCount = pointlist.Count;
            TravelPath.SetPositions(pointlist.ToArray());
            HideTravelPath();

        }
        else
            TravelPath.enabled = false;
    }
}

