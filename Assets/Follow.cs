using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject go;
    public Vector3 offset;
    public bool position = true;
    public bool rotation = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(go != null)
        {
            
            if(position)
            {
                transform.position = go.transform.position;
                
            }

            if (rotation)
            {
                transform.rotation = go.transform.rotation;
            }
            
        }
    }
}
