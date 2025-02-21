using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using static UnityEngine.UI.Image;

public class XRF_RaycastInteractions_VRController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float laserDistance = 100.0f;
    public GameObject controllerGameObject;
    public GameObject pointerPrefab;
    public Material LaserMaterial_Missed;
    public Material LaserMaterial_Hit;
    public bool isCanvasRaycast = false;
    public Camera raycastCamera;
    #endregion

    #region PRIVATE VARIABLES
    private bool dontHighlight;
    private Material[] tempMaterialsHigh;
    private Material[] matsHigh;
    private GameObject tempSelectedObject;
    private GameObject hitObject;
    private bool isClickable;
    private Vector3 endPoint;
    private bool somethingIsGrabbed;
    private bool somethingIsHandGrabbed;
    private bool isGrabable;
    private bool isHandGrabbable;
    private GameObject grabbedObject;
    private GameObject handGrabParent;
    private float moveLength;
    private float tempDistance;
    private Vector3 basePosObject;
    private Vector3 clickOrigin;
    private LineRenderer lineRend;

    private Vector3 origin;
    private Vector3 direction;
    #endregion

    void Start()
    {
        //create line renderer and pointer objects
        lineRend = controllerGameObject.gameObject.AddComponent<LineRenderer>();
        lineRend.material = LaserMaterial_Missed;
        lineRend.startWidth = 0.002f;
        lineRend.endWidth = 0.002f;
        lineRend.positionCount = 2;
        pointerPrefab = Instantiate(pointerPrefab);
    }

    private void Update()
    {
        Ray ray = new Ray();

        if (isCanvasRaycast)
        {
            //using keyboard and mouse
            ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            origin = ray.origin;
            direction = ray.direction;
        }
        else
        {
            //using vr controller
            origin = controllerGameObject.transform.position;
            direction = controllerGameObject.transform.forward;
            ray = new Ray(origin, direction);
        }



        if (somethingIsGrabbed)
        {
            Debug.Log("hey i grabbed is true");
            pointerPrefab.SetActive(false);
            lineRend.enabled = false;
            Vector3 grabEndPoint = origin + direction * moveLength;
            Vector3 movePosition = basePosObject + (grabEndPoint - clickOrigin);
            grabbedObject.transform.position = movePosition;
        }
        else if (somethingIsHandGrabbed)
        {
            Debug.Log("hey i hand grabbed is true");
            pointerPrefab.SetActive(false);
            lineRend.enabled = false;
        }
        else
        {
            lineRend.enabled = true;
            RaycastHit myRayHit;

            if (Physics.Raycast(ray, out myRayHit, laserDistance) && controllerGameObject.activeSelf)
            {
                pointerPrefab.SetActive(true);

                //i shot out a ray and it hit something
                //Debug.Log("I hit something");
                hitObject = myRayHit.transform.gameObject;
                endPoint = myRayHit.point;

                if (!hitObject.GetComponent<Collider>().isTrigger && hitObject.GetComponent<XRF_UPenn_InteractionController>())
                {
                    //i shot out a ray and hit something with an interaction controller
                    Debug.Log("I hit something with an interaction controller on it");
                    lineRend.material = LaserMaterial_Hit;

                    if (hitObject.GetComponent<XRF_UPenn_InteractionController>().isGrabbable)
                    {
                        Debug.Log("hey i hit a grabbable");
                        tempDistance = Vector3.Distance(origin, myRayHit.point);
                        isClickable = false;
                        isGrabable = true;
                        isHandGrabbable = false;
                        RayHit(hitObject);
                    }
                    else if (hitObject.GetComponent<XRF_UPenn_InteractionController>().isHandGrabbable)
                    {
                        Debug.Log("hey i hit a grabbable");
                        tempDistance = Vector3.Distance(origin, myRayHit.point);
                        isClickable = false;
                        isGrabable = false;
                        isHandGrabbable = true;
                        RayHit(hitObject);
                    }
                    else
                    {
                        isClickable = true;
                        isGrabable = false;
                        isHandGrabbable = false;
                        RayHit(hitObject);
                    }
                }
                else
                {
                    RayMissed();
                }
            }
            else
            {
                pointerPrefab.SetActive(false);
                endPoint = origin + direction * 0.5f;
                RayMissed();
            }

            pointerPrefab.transform.position = endPoint;
            lineRend.SetPosition(0, origin);
            lineRend.SetPosition(1, endPoint);
        }
    }

    public void ClickTheButton(GameObject hitObject)
    {
        XRF_UPenn_InteractionController[] myInteractions = hitObject.GetComponents<XRF_UPenn_InteractionController>();
        foreach (XRF_UPenn_InteractionController t in myInteractions)
        {
            t.DoTheThing();
        }
    }

    void RayHit(GameObject touchObject)
    {
        if (tempSelectedObject != touchObject)
        {
            if (tempSelectedObject != null)
            {
                UnHighlightObj(tempSelectedObject);
            }
        }
        tempSelectedObject = touchObject;
        HighlightObj(tempSelectedObject);
    }
    void RayMissed()
    {
        lineRend.material = LaserMaterial_Missed;
        isGrabable = false;
        isHandGrabbable = false;
        isClickable = false;

        if (tempSelectedObject != null)
        {
            UnHighlightObj(tempSelectedObject);
            tempSelectedObject = null;
        }
    }
    void HighlightObj(GameObject highlightThis)
    {
        MeshRenderer rend = highlightThis.transform.gameObject.GetComponent<MeshRenderer>();
        if (rend != null)
        {
            if (!dontHighlight && highlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected == false)
            {
                tempMaterialsHigh = highlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials;
                matsHigh = new Material[tempMaterialsHigh.Length];

                Material highlightMaterial = highlightThis.GetComponent<XRF_UPenn_InteractionController>().HighlightMaterial;

                for (int i = 0; i < tempMaterialsHigh.Length; i++)
                {
                    matsHigh[i] = highlightMaterial;
                }
                highlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials = matsHigh;
                dontHighlight = true;
                highlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected = true;
            }
        }
    }
    void UnHighlightObj(GameObject unHighlightThis)
    {
        MeshRenderer rend = unHighlightThis.GetComponent<MeshRenderer>();
        if (rend != null && unHighlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected == true)
        {
            unHighlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials = tempMaterialsHigh;
            dontHighlight = false;
            unHighlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected = false;
        }
    }


    public void TriggerClick()
    {
        Debug.Log("hey i clicked the trigger button");

        if (isClickable)
        {
            ClickTheButton(hitObject);
        }
        else if (isGrabable)
        {
            //move based on laser length
            somethingIsGrabbed = true;
            grabbedObject = hitObject;
            moveLength = tempDistance;
            basePosObject = grabbedObject.transform.position;
            clickOrigin = endPoint;

        }
        else if (isHandGrabbable)
        {
            //make this a child of the hand...
            somethingIsHandGrabbed = true;
            grabbedObject = hitObject;
            handGrabParent = hitObject.transform.parent.gameObject;
            basePosObject = grabbedObject.transform.position;
            grabbedObject.transform.SetParent(controllerGameObject.transform);
        }
    }

    public void TriggerUnClick()
    {
        Debug.Log("hey i UN clicked the trigger button");

        if (somethingIsGrabbed)
        {
            if(grabbedObject != null)
            {
                if (grabbedObject.GetComponent<XRF_UPenn_InteractionController>().returnToStart)
                {
                    grabbedObject.transform.position = basePosObject;
                    //grabbedObject.transform.position = grabbedObject.GetComponent<XRF_UPenn_InteractionController>().originalPos;
                }
            }
        }
        else if (somethingIsHandGrabbed)
        {
            if (grabbedObject != null)
            {
                grabbedObject.transform.SetParent(handGrabParent.transform);
                //grabbedObject.transform.position = grabbedObject.GetComponent<XRF_UPenn_InteractionController>().originalPos;
            }
        }
        somethingIsGrabbed = false;
        somethingIsHandGrabbed = false;
        grabbedObject = null;
        RayMissed();//clear everything after you let go
    }
}