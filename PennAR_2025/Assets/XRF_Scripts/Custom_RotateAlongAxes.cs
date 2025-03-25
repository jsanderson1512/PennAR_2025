using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_RotateAlongAxes : MonoBehaviour
{
    public Transform theThingToRotate;
    public float rotationSpeed = 5f; // Adjust this for desired speed
    private Quaternion targetRotation; // Stores the target rotation

    private bool isRotating = false;

    // Update is called once per frame
    void Update()
    {
        if(isRotating)
        {
            // Lerp the rotation
            theThingToRotate.rotation = Quaternion.Lerp(theThingToRotate.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (theThingToRotate.rotation == targetRotation)
            {
                isRotating = false;
            }
        }



    }

    public void RotateXPositive()
    {
        if (!isRotating)
        {
            Quaternion currentRot = theThingToRotate.rotation; //i want to add 90 in x direction to this...
            targetRotation = currentRot * Quaternion.Euler(90, 0, 0);

            isRotating = true;
        }
    }
    public void RotateXNegative()
    {
        if (!isRotating)
        {

            Quaternion currentRot = theThingToRotate.rotation; //i want to add 90 in x direction to this...
            targetRotation = currentRot * Quaternion.Euler(-90, 0, 0);


            isRotating = true;
        }
    }
    public void RotateYPositive()
    {
        if (!isRotating)
        {

            Quaternion currentRot = theThingToRotate.rotation; //i want to add 90 in x direction to this...
            targetRotation = currentRot * Quaternion.Euler(0, 0, 90);


            isRotating = true;
        }
    }
    public void RotateYNegative()
    {
        if (!isRotating)
        {
            Quaternion currentRot = theThingToRotate.rotation; //i want to add 90 in x direction to this...
            targetRotation = currentRot * Quaternion.Euler(0, 0, -90);


            isRotating = true;
        }
    }
}
