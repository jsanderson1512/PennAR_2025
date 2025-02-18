 using UnityEngine;
 using UnityEditor;
using System.Linq;
using UnityEditor.Events;

[CustomEditor(typeof(XRF_UPenn_InteractionController))]
public class XRF_InteractionControllerEditor : Editor
{
    private int previousOnCount;
    private int previousOffCount;

    public override void OnInspectorGUI()
    {
        XRF_UPenn_InteractionController script = (XRF_UPenn_InteractionController)target;
        script.myType = (XRF_UPenn_InteractionController.InteractionType)EditorGUILayout.EnumPopup("Interaction Type", script.myType);

        //turn on highlight material if raycast is selected
        if (script.gameObject.GetComponent<Collider>().isTrigger == false)
        {
            script.HighlightMaterial = (Material)EditorGUILayout.ObjectField("Highlight Material", script.HighlightMaterial, typeof(Material), true);
            //if this is empty, place a standard material there
            if(script.HighlightMaterial == null)
            {
                script.HighlightMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                script.HighlightMaterial.name = "Empty Material";
                script.HighlightMaterial.color = Color.magenta;
            }
        }
        //deactivate certain tools if trigger is on
        else
        {
            //don't allow you to do grab if you are a trigger
            if (script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndReturn || script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndStay)
            {
                script.myType = XRF_UPenn_InteractionController.InteractionType.AnimationController;
            }
        }
        //fields for each interaction type below
        if (script.myType == XRF_UPenn_InteractionController.InteractionType.OnOffController)
        {
            script.NumberOfThingsToTurnON = EditorGUILayout.IntField("Number of Things to Turn ON", script.NumberOfThingsToTurnON);
            GameObject[] tempOns = script.StartOFFClickON;
            if (previousOnCount != script.NumberOfThingsToTurnON)
            {
                script.StartOFFClickON = new GameObject[script.NumberOfThingsToTurnON];
            }
            for (int i = 0; i < script.NumberOfThingsToTurnON; i++)
            {
                if (tempOns != null)
                {
                    if (tempOns.Length > i)
                    {
                        script.StartOFFClickON[i] = tempOns[i];
                    }
                }
                script.StartOFFClickON[i] = (GameObject)EditorGUILayout.ObjectField("Start OFF Click ON", script.StartOFFClickON[i], typeof(GameObject), true);
            }

            script.NumberOfThingsToTurnOFF = EditorGUILayout.IntField("Number of Things to Turn OFF", script.NumberOfThingsToTurnOFF);
            GameObject[] tempOffs = script.StartONClickOFF;
            if (previousOffCount != script.NumberOfThingsToTurnOFF)
            {
                script.StartONClickOFF = new GameObject[script.NumberOfThingsToTurnOFF];
            }
            for (int i = 0; i < script.NumberOfThingsToTurnOFF; i++)
            {
                if (tempOffs != null)
                {
                    if (tempOffs.Length > i)
                    {
                        script.StartONClickOFF[i] = tempOffs[i];
                    }
                }

                script.StartONClickOFF[i] = (GameObject)EditorGUILayout.ObjectField("Start ON Click OFF", script.StartONClickOFF[i], typeof(GameObject), true);
            }
            previousOnCount = script.NumberOfThingsToTurnON;
            previousOffCount = script.NumberOfThingsToTurnOFF;
        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.AnimationController)
        {
            script.ObjectWithAnimation = (Animator)EditorGUILayout.ObjectField("Object with Animation", script.ObjectWithAnimation, typeof(Animator), true);
        }        
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndReturn)
        {

        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndStay)
        {

        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.AudioPlayer)
        {
            script.theAudioSource = (AudioSource)EditorGUILayout.ObjectField("Audio Source in Scene", script.theAudioSource, typeof(AudioSource), true);
            script.theAudioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip in Project Folder", script.theAudioClip, typeof(AudioClip), true);
        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.CallEvent)
        {
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("EventToCall"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}