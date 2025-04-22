using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_PlayPartOfAMusicClip : MonoBehaviour
{

    //audio stuff
    public AudioSource theAudioSource;
    public AudioClip theAudioClip;
    public float startHereInSeconds = 60.0f;
    public float howLongDoIPlayInSeconds = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        theAudioSource.clip = theAudioClip;
        theAudioSource.playOnAwake = false;
        theAudioSource.loop = false;
        theAudioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFromTimestamp()
    {
        theAudioSource.clip = theAudioClip;
        theAudioSource.time = startHereInSeconds;
        theAudioSource.Play();
        StartCoroutine(waitAndTurnOff());

        //stop after some number of seconds...
    }

    public IEnumerator waitAndTurnOff()
    {
        yield return new WaitForSeconds(howLongDoIPlayInSeconds);
        theAudioSource.Stop();
    }
}
