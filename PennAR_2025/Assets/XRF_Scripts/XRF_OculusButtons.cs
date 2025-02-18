using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XRF_OculusButtons : MonoBehaviour
{
    //Oculus Touch Triggers
    public UnityEvent ButtonEvent_Trigger_Left_Down;
    public UnityEvent ButtonEvent_Trigger_Left_Up;
    private bool Trigger_Left_Down;
    public UnityEvent ButtonEvent_Trigger_Right_Down;
    public UnityEvent ButtonEvent_Trigger_Right_Up;
    private bool Trigger_Right_Down;

    //Oculus Touch Grips
    public UnityEvent ButtonEvent_Grip_Left_Down;
    private bool Grip_Left_Down;
    public UnityEvent ButtonEvent_Grip_Right_Down;
    private bool Grip_Right_Down;

    //Oculus Touch Face Buttons
    public UnityEvent ButtonEvent_XButton_Left;
    public UnityEvent ButtonEvent_YButton_Left;
    public UnityEvent ButtonEvent_AButton_Right;
    public UnityEvent ButtonEvent_BButton_Right;

    //Listen for Button Inputs
    private void Update()
    {
        HandleOculusTouchFaceButtons();
        HandleOculusTriggerButtons();
        HandleOculusGripButtons();
        HandleOculusJoysticks();
    }
    private void HandleOculusTouchFaceButtons()
    {
        //OCULUS TOUCH FACE BUTTONS
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            Debug.Log("I pressed down the X button on the Left Controller");
            ButtonEvent_XButton_Left.Invoke();
        }
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            Debug.Log("I pressed down the Y button on the Left Controller");
            ButtonEvent_YButton_Left.Invoke();
        }
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("I pressed down the A button on the Right Controller");
            ButtonEvent_AButton_Right.Invoke();
        }
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            Debug.Log("I pressed down the B button on the Right Controller");
            ButtonEvent_BButton_Right.Invoke();
        }
    }
    private void HandleOculusTriggerButtons()
    {
        //OCULUS TOUCH TRIGGERS Left Controller
        float LTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);//(range of 0.0f to 1.0f)
        if(Trigger_Left_Down)
        {
            //if trigger is down, wait for trigger to let go
            if (LTrigger < 0.3f)
            {
                Debug.Log("Left Controller Trigger let go.");
                ButtonEvent_Trigger_Left_Up.Invoke();
                Trigger_Left_Down = false;
            }
        }
        else
        {
            //if trigger is up, wait for trigger to be pressed
            if (LTrigger > 0.6f)
            {
                Debug.Log("Left Controller Trigger touched.");
                ButtonEvent_Trigger_Left_Down.Invoke();
                Trigger_Left_Down = true;
            }
        }
        //OCULUS TOUCH TRIGGERS Right Controller
        float RTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);//(range of 0.0f to 1.0f)
        if (Trigger_Right_Down)
        {
            //if trigger is down, wait for trigger to let go
            if (RTrigger < 0.3f)
            {
                Debug.Log("Right Controller Trigger let go.");
                ButtonEvent_Trigger_Right_Up.Invoke();
                Trigger_Right_Down = false;
            }
        }
        else
        {
            //if trigger is up, wait for trigger to be pressed
            if (RTrigger > 0.6f)
            {
                Debug.Log("Right Controller Trigger touched.");
                ButtonEvent_Trigger_Right_Down.Invoke();
                Trigger_Right_Down = true;
            }
        }
    }

    private void HandleOculusGripButtons()
    {
        //OCULUS TOUCH GRIPS Left Controller
        float LGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);//(range of 0.0f to 1.0f)
        if (Grip_Left_Down)
        {
            //if trigger is down, wait for trigger to let go
            if (LGrip < 0.3f)
            {
                Debug.Log("Left Controller Grip let go.");
                Grip_Left_Down = false;
            }
        }
        else
        {
            //if trigger is up, wait for trigger to be pressed
            if (LGrip > 0.6f)
            {
                Debug.Log("Left Controller Grip touched.");
                ButtonEvent_Grip_Left_Down.Invoke();
                Grip_Left_Down = true;
            }
        }
        //OCULUS TOUCH GRIPS Right Controller
        float RGrip = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);//(range of 0.0f to 1.0f)
        if (Grip_Right_Down)
        {
            //if trigger is down, wait for trigger to let go
            if (RGrip < 0.3f)
            {
                Debug.Log("Right Controller Grip let go.");
                Grip_Right_Down = false;
            }
        }
        else
        {
            //if trigger is up, wait for trigger to be pressed
            if (RGrip > 0.6f)
            {
                Debug.Log("Right Controller Grip touched.");
                ButtonEvent_Grip_Right_Down.Invoke();
                Grip_Right_Down = true;
            }
        }
    }
    private void HandleOculusJoysticks()
    {
        //OCULUS TOUCH JOYSTICKS
        Vector2 LJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);//(X/Y range of -1.0f to 1.0f)
        if (LJoystick.x != 0 || LJoystick.y != 0)
        {
            Debug.Log("Left Controller joystick touched.");
        }
        Vector2 RJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);//(X/Y range of -1.0f to 1.0f)
        if (RJoystick.x != 0 || RJoystick.y != 0)
        {
            Debug.Log("Right Controller joystick touched.");
        }
    }

}
