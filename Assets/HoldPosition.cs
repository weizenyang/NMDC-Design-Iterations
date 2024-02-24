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

    public bool testHoldState = false;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
        initRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdState)
        {
            transform.position = holdPosition;
            transform.rotation = holdRotation;

        }
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
        transform.localPosition = initPosition;
        transform.localRotation = initRotation;

    }
}
