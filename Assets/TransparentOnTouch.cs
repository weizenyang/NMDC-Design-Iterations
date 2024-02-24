using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentOnTouch : MonoBehaviour
{

    public float fadeSpeed = 1.0f; // Adjust this value to control the speed of the fade

    private Renderer[] renderers;

    private bool translucent = false;
    float translucency = 1f;
    public float targetTranslucency = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials; // Get all materials of the renderer

            for (int i = 0; i < materials.Length; i++)
            {
                Color materialColor = materials[i].color;
                
                if(translucent)
                {
                    translucency = 0.1f;
                } else
                {
                    translucency = 1f;
                }
                //materialColor.a = Mathf.Lerp(materialColor.a, translucency, fadeSpeed * Time.deltaTime);
                //materials[i].color = materialColor;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        //if (other.gameObject.layer == 11) {
            //translucent = true;
            if (GetComponent<MeshRenderer>() != null)
            {

                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        //}
        
        
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.layer == 11){
            //translucent = false;
            if (GetComponent<MeshRenderer>() != null)
            {
                GetComponent<MeshRenderer>().enabled = true;

            }
            else
            {
                GetComponentInChildren<MeshRenderer>().enabled = true;
            }
        //}


    }
}




