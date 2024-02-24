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
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdState)
        {
            transform.position = holdPosition;
            transform.rotation = holdRotation;

        } else
        {
            transform.position = initPosition;
            transform.rotation = initRotation;
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
        holdState = false;
    }
}
