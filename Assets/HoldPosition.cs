using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPosition : MonoBehaviour
{
    public Vector3 initPosition;
    public Quaternion initRotation;
    public Vector3 currentPosition;
    public Quaternion currentRotation;
    public Vector3 holdPosition;
    public Quaternion holdRotation;
    public bool holdState = false;
    public GameObject go;

    public bool testHoldState = false;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (holdState)
        {
            transform.position = holdPosition;
            transform.rotation = holdRotation;

        } else
        {
            var step = 0.4f * Time.deltaTime; // calculate distance to move
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, initPosition, step);
            //transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0,0,0), step * 10);
            if (go != null)
            {
                float magnitude = Vector3.Magnitude(transform.position - go.transform.position);
                Debug.Log("Magnitude" + magnitude);
                transform.position -= Vector3.ClampMagnitude((transform.position - go.transform.position) * 0.5f, 0.01f);
                
            }
            //Vector3 tempPos = transform.localPosition + initPosition;
            //transform.localPosition = tempPos;
        }
        transform.rotation = initRotation;

        
    }

    public void gestureDetected(Pose p)
    {
        Debug.Log(p);
    }



    public void holdStart()
    {
        Debug.Log("Holding");
        holdState = true;
        holdPosition = transform.position;
        holdRotation = transform.rotation;
    }

    public void holdStop()
    {
        Debug.Log("Stopped Holding");
        holdState = false;
        //transform.localPosition = initPosition;
        //transform.localRotation = initRotation;

    }
}
