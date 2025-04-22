using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Custom_CanvasManager : MonoBehaviour
{
    public List<GameObject> CanvasItemsInOrder = new List<GameObject>();
    private int myCurrentItemIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChangePage(myCurrentItemIndex);
    }
    public void Button_NextPage()
    {
        myCurrentItemIndex++;
        //constrain value
        ChangePage(myCurrentItemIndex);
    }
    public void Button_PrevPage() 
    { 
        myCurrentItemIndex--;
        //constrain value
        ChangePage(myCurrentItemIndex);
    }
    private void ChangePage(int whatPage)
    {
        for (int i = 0; i < CanvasItemsInOrder.Count; i++)
        {
            if (i == whatPage)
            {
                CanvasItemsInOrder[i].SetActive(true);
            }
            else
            {
                CanvasItemsInOrder[i].SetActive(false);
            }
        }
    }
}
