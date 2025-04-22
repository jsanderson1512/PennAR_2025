using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_MySpecialScript : MonoBehaviour
{
    public GameObject mySpecialPublicGameObject;
    private GameObject mySpecialPrivateGameObject;
    public Vector3 myVector3;
    public Transform myTransform;
    public float myFloat;
    public Color myColor;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this is a global variable, its value will be remembered at the end
        //of the update loop
        myFloat = myFloat + 500.0f;
        //value will be 500, 1000 ,1500,2000,2500...


        //this is a local variable, it is recreated from scratch
        //every update loop
        float myLocalFloat =  0.0f;
        myLocalFloat = myLocalFloat + 500.0f;
        //value will be 500,500,500,500,500

        MySpecialVoidFunction(55.5f);
        float aNewLocalFloat1 = MySpecialFloatFunction(55.5f);
        float aNewLocalFloat2 = MySpecialFloatFunction(23.5f);
        float aNewLocalFloat3 = MySpecialFloatFunction(55432.5f);
        float aNewLocalFloat4 = MySpecialFloatFunction(5626.5f);
        float aNewLocalFloat5 = MySpecialFloatFunction(634255.5f);
        float aNewLocalFloat6 = MySpecialFloatFunction(55124.5f);
        float aNewLocalFloat7 = MySpecialFloatFunction(515.5f);

    }

    public void MySpecialVoidFunction(float aLocalFloat)
    {
        //do the thing
        myFloat += aLocalFloat;
    }

    public float MySpecialFloatFunction(float aLocalFloat)
    {
        //return some value of type "float"
        float x = 50 + aLocalFloat;
        return x;
    }

}
