using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    // Screen width and height
    int width;
    int height;
    public Camera MainCamera;
    public Vector3 CameraZoomVariable=new Vector3(0,0,0); // Zoomlocation = Gameobject.location + CamerazoomVariable
    private Vector3 CameraTargetLocation;
    private Vector3 CameraOutLocation;

    //Clicking
    public LayerMask mask;
    public GameObject target;

    //public GameObject BacktoOverView;
    //public GameObject BacktoPlayerView;

    public bool CanRayCast = true;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

   // static float t = 0.5f;
    float speed = 50f;

    GameObject CameraDefault;
    public GameObject startMarker;
    public GameObject endMarker;

    public CellObject PlayerObject;
    public ChosenAbility PlayerChosenAbility;

    public bool CameraCanMove = true;
    public bool CameraCanRetarget = true;

   // public GameObject Overview;
  //  public GameObject InspectionScreen;

    public GameObject CurrentTargetVFX;
    public GameObject ColorHolder;
    public GameObject ZoominTarget = null;

    public GameEvent CameraMoveTargetEvent;
    public GameEvent CameraMoveBackEvent;

    public CellObject InspectTarget;

    public GameEvent inspectionScreen;

    private void Start()
    {
        MainCamera = Camera.main;
        CameraDefault = new GameObject();
        startMarker = new GameObject(); 
        endMarker = new GameObject(); 

        // Keep a note of the time the movement started.
        startTime = Time.time;
        CameraDefault.transform.position =startMarker.transform.position=endMarker.transform.position= MainCamera.transform.position;


       // BrektT = BigScreenButton.image.rectTransform;
      //  Brotation = BrektT.rotation;
        
        width = UnityEngine.Screen.width;
        height = UnityEngine.Screen.height;

       // XPosMax = width / 2;
       // YPosMax = height / 2;

      //  MainCamera.transform.Rotate(180, 0, 0);
        CameraOutLocation = MainCamera.transform.position;

    }


    private void Update() // Camera moves to cell and Back
    {
        if (!PlayerObject.IsAlive)
        {
            Debug.Log("noplayer");
            return;
        }

        //if (!CameraCanMove)
        //{ return; }

        if (ZoominTarget)// && ZoominTarget != PlayerObject.Player)
        {
            Vector3 targetpos = ZoominTarget.transform.position;
            targetpos.y = +0.1f;
            CurrentTargetVFX.transform.position = targetpos;

            // CurrentTargetVFX.transform.parent = ZoominTarget.transform;
            if (ZoominTarget != null && ZoominTarget.GetComponent<Cell>())
            {
                CurrentTargetVFX.GetComponent<OnTargetVFXScript>().colorholder = ZoominTarget.GetComponent<Cell>().thingthatchangescolor;
                CurrentTargetVFX.SetActive(true);
            }
        }
        else
        { 
           // CurrentTargetVFX.transform.parent = this.gameObject.transform;
            CurrentTargetVFX.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") )//&& CameraCanRetarget && CanRayCast)//&& player.GetComponent<Player>().GetCanSelectTarget())
        {
            if (PlayerObject.CanSelectTarget ==false)
                return;

            Raycastfunction();
           
            if (ZoominTarget == null)
                return;
            else
            {
                CameraMoveTarget();
                inspectionScreen.Raise();
                //open the inspectionscreen info and call the function that 
                //Overview.GetComponent<Canvas>().enabled = false;
                //InspectionScreen.GetComponent<Canvas>().enabled = true;
                //InspectionScreen.GetComponent<InspectionScreen>().InfoCanvasSetActive(ZoominTarget);
            }
             
        }
       // InspectionScreen.GetComponent<InspectionScreen>().EnableConfirm();
        CameraMove();
    }

    public void OnTick()
    {
        if(ZoominTarget)
        InspectTarget.Refresh(ZoominTarget);
    }
    

    public void Raycastfunction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //check whatever you clicked on
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (!hit.collider.gameObject.GetComponent<Cell>())
                return;

            ZoominTarget = hit.collider.gameObject;
            if(ZoominTarget.GetComponent<Cell>().ThisCellObject != PlayerObject)
            ZoominTarget.GetComponent<Cell>().ThisCellObject = InspectTarget;
            InspectTarget.Refresh(ZoominTarget);
            PlayerChosenAbility.SetTarget(ZoominTarget);
        }

    }

    //public void CameraMovePlayer()
    //{
    //    // CanRayCast = false;
    //    PlayerObject.CanSelectTarget = false;

    //    BacktoOverView.SetActive(true);
    //    //BacktoPlayerView.SetActive(false);


    //    startTime = Time.time;
    //    //   Vector3 Moveto = t.transform.position 
    //    startMarker.transform.position = MainCamera.transform.position;


    //    endMarker.transform.position = PlayerObject.Pos;
    //    endMarker.transform.position += new Vector3(1, 3, -1f);
    //    //Calculate the journey length.
    //    journeyLength = Vector3.Distance(MainCamera.transform.position, PlayerObject.Pos);

    //    //  player.GetComponent<Player>().SetCanSelectTarget(false);

    //}

    public void CameraMoveBack()
    {
        PlayerObject.CanSelectTarget = true;
        //  CanRayCast = true;
        // UIbase.GetComponent<UIBase>().DisableAll();
        ZoominTarget = null;
      //  player.GetComponent<Player>().SetCanSelectTarget(true);
        
       // BacktoOverView.SetActive(false);
        //BacktoPlayerView.SetActive(true);

        target = null;
        startTime = Time.time;
        startMarker.transform.position = MainCamera.transform.position;
        endMarker.transform.position = CameraDefault.transform.position;

        journeyLength = Vector3.Distance(MainCamera.transform.position, endMarker.transform.position);
    }

    public void CameraMoveTarget()
    {
        CanRayCast = false;
        PlayerObject.CanSelectTarget = false;
        Vector3 t;
        if (ZoominTarget == null)
        {
            t = PlayerObject.Pos;
        }
        else
        {
            t = ZoominTarget.transform.position;
        }
      //  player.GetComponent<Player>().SetCanSelectTarget(false);

     //   BacktoOverView.SetActive(true);
        //BacktoPlayerView.SetActive(false);

        startTime = Time.time;
     //   Vector3 Moveto = t.transform.position 
        startMarker.transform.position = MainCamera.transform.position;
       

        endMarker.transform.position = t;
        endMarker.transform.position += new Vector3( 1, 3, -1f);
         //Calculate the journey length.
        journeyLength = Vector3.Distance(MainCamera.transform.position, t);

    }
    public void CameraMove()
    {
        //to stop it from constantly having to move
        if (Vector3.Distance(startMarker.transform.position, endMarker.transform.position) < 0.01f)
        {
            ZoominTarget = null;
            return;
        }

        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        MainCamera.transform.position = Vector3.Lerp(startMarker.transform.position, endMarker.transform.position, fracJourney);
    }

   

    public void SetCameraCanRetarget(bool f)
    {
        CameraCanRetarget = f;
    }
}
