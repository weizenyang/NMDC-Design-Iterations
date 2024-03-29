using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SetScale : MonoBehaviour
{
    float initScaleFactor = 0f;
    float targetScaleFactor = 0.01f;
    float currentScaleFactor = 0f;
    public bool hovered = false;
    public bool manipulating = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //public void OnScrollValueChange(Vector2 value)
    //{
    //    Debug.Log("Set Scale: " + value);   
    //    currentScaleFactor = (value.x * 0.02f);
    //    manipulating = true;
    //    Debug.Log("Manipulating " + manipulating);
    //    currentScaleFactor += (targetScaleFactor - currentScaleFactor) * 0.05f;
    //    transform.localScale = new Vector3(currentScaleFactor, currentScaleFactor, currentScaleFactor);
    //}

    public void externalSetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void externalAddScale(float scale)
    {
        transform.localScale += new Vector3(scale, scale, scale);
    }
}
