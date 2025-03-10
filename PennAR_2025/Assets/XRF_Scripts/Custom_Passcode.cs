using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Custom_Passcode : MonoBehaviour
{
    public GameObject[] winningNumbers;
    public GameObject[] wrongNumbers;

    //animation stuff
    public Animator ObjectWithAnimation;

    private bool youWin = false;

    private void Start()
    {
        if (ObjectWithAnimation != null)
        {
            string animName = ObjectWithAnimation.runtimeAnimatorController.animationClips[0].name;
            //Debug.Log("my animation is called: " + animName);
            //play on start but set to false so it stops
            ObjectWithAnimation.Play(animName, 0, 0);
            ObjectWithAnimation.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (youWin)
        {
            return;
        }


        youWin = true;
        foreach (GameObject go in winningNumbers)
        { 
            if(go.activeSelf == false)
            {
                //you lose
                youWin = false;
            }
        }

        foreach(GameObject go in wrongNumbers)
        {
            if(go.activeSelf)
            {
                //you lose
                youWin = false;
            }
        }


        if (youWin)
        {
            Debug.Log("you win");
            string animName = ObjectWithAnimation.runtimeAnimatorController.animationClips[0].name;

            if (ObjectWithAnimation.runtimeAnimatorController.animationClips[0].isLooping) //if loop is true
            {
                if (ObjectWithAnimation.isActiveAndEnabled) //if i am currently on, turn off
                {
                    Debug.Log("my animation was playing and enabled, it will stop now");
                    ObjectWithAnimation.enabled = false;
                }
                else //if i am currently off, turn on
                {
                    ObjectWithAnimation.enabled = true;
                }
            }
            else //loop is false
            {
                if (ObjectWithAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //if i am done with my animation sequence (at the end)
                {
                    ObjectWithAnimation.Play(animName, 0, 0);
                    ObjectWithAnimation.enabled = true;
                }
                else //either i am paused or i am playing and not done
                {
                    if (ObjectWithAnimation.isActiveAndEnabled) //if i am currently on, turn off
                    {
                        Debug.Log("my animation was playing and enabled, it will stop now");
                        ObjectWithAnimation.enabled = false;
                    }
                    else //if i am currently off, turn on
                    {
                        ObjectWithAnimation.enabled = true;
                    }
                }
            }
        }

        
    }
}
