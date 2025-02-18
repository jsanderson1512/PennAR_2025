using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRF_TurnOffGeometry : MonoBehaviour
{
    public GameObject AR_Anchor_Origin;

    public void Button_ToggleGeometry()
    {
        if (AR_Anchor_Origin.activeSelf)
        {
            AR_Anchor_Origin.SetActive(false);

        }
        else 
        {
            AR_Anchor_Origin.SetActive(true);
        }
    }
}
