using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_ComeToMeAndShrink : MonoBehaviour
{
    public GameObject thingToShrink;
    public Transform cameraLocation;
    public float howFastToMove;
    public float howSmallToShrink;


    private bool AmIShrinking = false;
    private Vector3 endScale;



    // Update is called once per frame
    void Update()
    {
        if(!AmIShrinking)
        {
            return;
        }
        else
        {
            //do the moving
            var step = howFastToMove * Time.deltaTime; // calculate distance to move
            thingToShrink.transform.position = Vector3.MoveTowards(thingToShrink.transform.position, cameraLocation.position, step);

            //do the shrinking...
            thingToShrink.transform.localScale = Vector3.MoveTowards(thingToShrink.transform.localScale, endScale, step);
            //transform.localScale = Vector3.Lerp(thingToShrink.transform.localScale, endScale, fractionOfJourney);

            if (Vector3.Distance(thingToShrink.transform.position, cameraLocation.position) < 0.1f)
            {
                AmIShrinking = false;
            }
        }
    }

    public void StartShrinking()
    {
        AmIShrinking=true;
        endScale = new Vector3(howSmallToShrink, howSmallToShrink, howSmallToShrink);
    }
}
