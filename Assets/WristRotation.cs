using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class WristRotation : MonoBehaviour
{
    XRHandSubsystem m_HandSubsystem;
    public XRHandTrackingEvents handTracking;
    public bool allowRotation;
    // Start is called before the first frame update
    Vector3 prevEulerAngles;
    Vector3 currentEulerAngles;

    [SerializeField] 
    private GameObject targetObject;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Wrist Direction" + (transform.forward - transform.position));
        

        currentEulerAngles = handTracking.rootPose.rotation.eulerAngles;

        Vector3 deltaEulerAngles = currentEulerAngles - prevEulerAngles;
        // Handle wrapping around 360 degrees (adjust based on your desired axis)
        if (deltaEulerAngles.z > 180f)
        {
            deltaEulerAngles.z -= 360f;
        }
        else if (deltaEulerAngles.z < -180f)
        {
            deltaEulerAngles.z += 360f;
        }

        //Debug.Log("Wrist Rotation" + deltaEulerAngles.z);

        // Update previous angles for next frame
        prevEulerAngles = currentEulerAngles;

        if (allowRotation)
        {
            targetObject.transform.localEulerAngles += new Vector3(0, deltaEulerAngles.z, 0);
        }

    }

    public void startRotation()
    {
        allowRotation = true;
    }

    public void endRotation()
    {
        allowRotation = false;
    }
}
