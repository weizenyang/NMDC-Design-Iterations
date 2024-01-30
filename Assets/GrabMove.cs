using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabMove : XRGrabInteractable
{

    // Start is called before the first frame update
    [SerializeField] bool constrainX;
    [SerializeField] bool constrainY;
    [SerializeField] bool constrainZ;
    // Start is called before the first frame update
    [SerializeField] bool constrainXRot;
    [SerializeField] bool constrainYRot;
    [SerializeField] bool constrainZRot;
    Vector3 initPosition;
    Vector3 initRotation;
    void Start()
    {
        initPosition = transform.localPosition;
        initRotation = transform.localEulerAngles;
        if (constrainX)
        {
            transform.localPosition = new Vector3(initPosition.x, transform.localPosition.y, transform.localPosition.z);
        }

        if (constrainY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, initPosition.y, transform.localPosition.z);
        }

        if (constrainZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initPosition.z);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Remove or reset constraints here
        base.OnSelectExited(args);
    }
}
