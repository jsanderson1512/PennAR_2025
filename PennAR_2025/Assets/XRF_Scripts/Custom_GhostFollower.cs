using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_GhostFollower : MonoBehaviour
{
    private List<Vector3> positionRecordings = new List<Vector3>();
    private bool isRecording = false;
    private bool isPlaying = false;
    public float timePeriod = 0.5f;
    public GameObject thingToRecord;
    public GameObject theGhostFollower;
    private int currentPosition;

    private float timestamp;
    public int interval = 5000;
   




    private void Update()
    {
        float theTime = Time.time;

        if (theTime-timestamp > timePeriod)
        {
            //it has been some time;
            //Debug.Log("hello, i am in play every seconds");
            timestamp = theTime;
            if (isRecording)
            {
                //do recording stuff
                positionRecordings.Add(thingToRecord.transform.position);
            }
            else if (isPlaying)
            {
                //do playing stuff
                if (currentPosition < positionRecordings.Count)
                {
                    theGhostFollower.transform.position = positionRecordings[currentPosition];
                    currentPosition++;
                }
                else
                {
                    isRecording = false;
                    isPlaying = false;
                }

            }
        }

    }

    public void Button_StartRecordingPath()
    {
        if(isPlaying)
        {
            return;
        }

        isRecording = true;
        isPlaying = false;

        positionRecordings = new List<Vector3>();
    }

    public void Button_ReplayPath()
    {
        isRecording = false;
        isPlaying = true;
        currentPosition = 0;
    }



}
