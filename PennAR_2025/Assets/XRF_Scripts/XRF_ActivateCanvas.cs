using UnityEngine;

public class XRF_ActivateCanvas : MonoBehaviour
{
    private GameObject canvas;
    // Start is called before the first frame update

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        if(canvas != null )
            canvas.SetActive(false);
    }
    void Start()
    {
        if (Application.isEditor)
        {
            if (canvas != null)
                canvas.SetActive(true);
        }
    }
}
