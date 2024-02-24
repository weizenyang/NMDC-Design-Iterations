using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(go != null)
        {
            transform.position = go.transform.position;
            transform.rotation = go.transform.rotation;
        }
    }
}
