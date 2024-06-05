using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectInViewport : MonoBehaviour
{
    //[SerializeField] private GameObject checkedObject;
    [SerializeField] private Camera cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        if (cameraObject == null)
        {
            cameraObject = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 viewPos = cameraObject.WorldToViewportPoint(checkedObject.transform.position);
    }

    public bool isInViewport(GameObject gameObject)
    {
        Vector3 viewPos = cameraObject.WorldToViewportPoint(gameObject.transform.position);

        //if(viewPos.x > -0.5f && viewPos.x < 1.5f && viewPos.y > -0.07f && viewPos.y < 1.5f)
        //{
        //    return true;
        //} else
        //{
        //    return false;
        //}

        return true;

    }
}

