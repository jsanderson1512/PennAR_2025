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

    // Start is called before the first frame update
    void Start()
    {
        //make a function that repeats over and over, every x seconds...
        StartCoroutine(PlayEverySeconds());
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

    private IEnumerator PlayEverySeconds()
    {
        yield return new WaitForSeconds(timePeriod);


        //my code here
        //Debug.Log("hello, i am in play every seconds");

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


        StartCoroutine(PlayEverySeconds());
    }


}
