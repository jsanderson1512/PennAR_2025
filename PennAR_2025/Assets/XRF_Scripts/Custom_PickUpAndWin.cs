using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_PickUpAndWin : MonoBehaviour
{
    public GameObject[] pickups;
    public GameObject youWinGameObject;
    private bool gameDone = false;
    // Start is called before the first frame update
    void Start()
    {
        youWinGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameDone)
        { return; }


        gameDone = true;
        foreach (GameObject pickup in pickups)
        {
            if (pickup.activeSelf)
            {
                gameDone = false;
            }
        }

        if (gameDone)
        {
            youWinGameObject.SetActive(true);
        }

    }
}
