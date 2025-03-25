using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_PhysicsMovements : MonoBehaviour
{
    public Rigidbody body;
    private GameObject theObject;
    public float PlungerMoveUpAmount = 0.4f;
    public float PlungerSpeed = 100f;
    private float yPositionToMoveTo;
    private Vector3 startPosition;

    private bool plungerActivated = false;
    private bool plungerForward = false;


    private bool flipperActivated = false;
    private bool flipperForward = false;
    private Quaternion startRot;
    private float startRotation;
    private float endRotation;
    public float FlipperRotationAmount = 20.0f;
    public float FlipperSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        theObject = body.gameObject;

        startPosition = body.transform.position;
        startRot = body.transform.rotation;
        yPositionToMoveTo = startPosition.y + PlungerMoveUpAmount; 

        startRotation = body.transform.localEulerAngles.z;
        endRotation = startRotation + FlipperRotationAmount;

    }

    // Update is called once per frame
    void Update()
    {
        if (plungerActivated)
        {
            if(plungerForward)
            {
                Debug.Log("moving forward");

                body.velocity = theObject.transform.up * PlungerSpeed * Time.fixedDeltaTime;
                if(theObject.transform.position.y > yPositionToMoveTo)
                {
                    plungerForward = false;
                }
            }
            else
            {
                Debug.Log("moving backward");

                body.velocity = -theObject.transform.up * PlungerSpeed * Time.fixedDeltaTime;

                if (theObject.transform.position.y < startPosition.y)
                {
                    plungerActivated = false;
                    plungerForward = false;
                }
            }
        }


        else if (flipperActivated)
        {
            if (flipperForward)
            {
                Debug.Log("moving forward");
                body.AddTorque(transform.forward * FlipperSpeed);
                
                if (body.transform.localEulerAngles.z > endRotation)
                {
                    flipperForward = false;
                }
                
            }
            else
            {
                Debug.Log("moving backward");
                body.AddTorque(transform.forward * -FlipperSpeed);

                if (body.transform.localEulerAngles.z < startRotation)
                {
                    flipperActivated = false;
                    flipperForward = false;
                }
            }
        }
        else
        {
            theObject.transform.position = startPosition;
            theObject.transform.rotation = startRot;
            body.angularVelocity = Vector3.zero;
        }
    }


    public void ActivatePlunger()
    {
        Debug.Log("the click worked");
        if (!plungerActivated)
        {
            plungerActivated = true;
            plungerForward = true;
        }

    }

    public void ActivateFlipper()
    {
        Debug.Log("the click worked");
        if (!flipperActivated)
        {
            flipperActivated = true;
            flipperForward = true;
        }

    }
}
