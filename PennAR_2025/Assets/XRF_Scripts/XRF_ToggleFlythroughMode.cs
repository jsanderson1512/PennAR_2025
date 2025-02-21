using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XRF_ToggleFlythroughMode : MonoBehaviour
{
    public GameObject OriginMarker;
    public GameObject GeometryParent;
    public GameObject LeftController;
    public GameObject RightController;
    private bool keyboardActive = false;
    public XRF_FlythroughCameraController CameraController;
    public UnityEvent ButtonEvent_Mouse_Down;
    public UnityEvent ButtonEvent_Mouse_Up;

    // Start is called before the first frame update
    void Start()
    {
        CameraController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyboardActive)
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                ButtonEvent_Mouse_Down.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ButtonEvent_Mouse_Up.Invoke();
            }
        }
    }
    public void StartKeyboardMouse()
    {
        keyboardActive = true;
        OriginMarker.SetActive(true);
        GeometryParent.SetActive(true);
        CameraController.enabled = true;
        LeftController.SetActive(false);
        RightController.SetActive(false);
    }
}
