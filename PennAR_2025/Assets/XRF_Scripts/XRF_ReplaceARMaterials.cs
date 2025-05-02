using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRF_ReplaceARMaterials : MonoBehaviour
{
    public Material originalMaterial;
    public Material replacementMaterial;
    private GameObject AR_Anchor_Origin;
    private MeshRenderer[] meshRenderers;
    private bool searchComplete = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Update()
    {
        if (!searchComplete)
        {
            AR_Anchor_Origin = GameObject.Find("AR_Anchor_Origin");
            if (AR_Anchor_Origin != null)
            {
                meshRenderers = AR_Anchor_Origin.GetComponentsInChildren<MeshRenderer>();
                //Debug.Log("i this many mesh renderers: " + meshRenderers.Length);

                foreach (MeshRenderer renderer in meshRenderers)
                {
                    if (renderer.sharedMaterial == originalMaterial)
                    {
                        Debug.Log("hello i found a material to replace");
                        renderer.sharedMaterial = replacementMaterial;
                        searchComplete = true;

                    }
                    else if (renderer.material == originalMaterial)
                    {
                        Debug.Log("hello i found a material to replace");
                        renderer.material = replacementMaterial;
                        searchComplete = true;

                    }

                }
            }
            else
            {
                Debug.Log("i didn't find that");

            }
        }

    }
}
