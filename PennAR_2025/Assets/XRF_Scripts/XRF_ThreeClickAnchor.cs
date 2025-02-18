using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class XRF_ThreeClickAnchor : MonoBehaviour
{
    public Transform ControllerAnchor;
    public Transform AR_Anchor_Origin;
    public GameObject OriginMarker;
    public GameObject GeometryParent;
    public TextMeshPro suggestionText;
    public string ControllerMessage = "Press X";
    public float positionBelowController = 0.2f;
    private bool Step1;
    private bool Step2;
    public UnityEvent CancelOtherControllerAnchoring;

    private void Start()
    {
        suggestionText.text = ControllerMessage + " to anchor geometry.";

        OriginMarker.SetActive(false);
        GeometryParent.SetActive(false);
        Step1= false;
        Step2 = false;
    }

    public void Button_AnchorButtonPressed()
    {
        CancelOtherControllerAnchoring.Invoke();
        AR_Anchor_Origin.gameObject.SetActive(true);
        if (!Step1 && !Step2)
        {
            //fresh start
            suggestionText.text = ControllerMessage + " to set geometry origin.";
            OriginMarker.SetActive(true);
            GeometryParent.SetActive(false);
            Step1 = true;
        }
        else if (Step1)
        {
            suggestionText.text = ControllerMessage + " to set geometry direction.";
            Step1 = false;
            Step2 = true;
        }
        else if (Step2)
        {
            suggestionText.text = ControllerMessage + " to anchor geometry.";
            OriginMarker.SetActive(false);
            GeometryParent.SetActive(true);
            Step1 = false;
            Step2 = false;
        }
    }
    private void Update()
    {
        if (!Step1 && !Step2)
        {
            return;
        }
        if (Step1)
        {
            AR_Anchor_Origin.position = ControllerAnchor.position - new Vector3(0, positionBelowController, 0);

        }
        else if (Step2)
        {
            Vector3 lookAtMe = new Vector3(ControllerAnchor.position.x, AR_Anchor_Origin.position.y, ControllerAnchor.position.z);
            AR_Anchor_Origin.LookAt(lookAtMe);

        }
    }
    public void CancelAnchoring()
    {
        suggestionText.text = ControllerMessage + " to anchor geometry.";
        Step1 = false;
        Step2 = false;
    }
}
