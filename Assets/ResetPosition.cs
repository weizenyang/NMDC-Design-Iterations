using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResetPosition : MonoBehaviour
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
        initPosition = transform.position;
        initRotation = transform.localEulerAngles;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPos;
        Vector3 targetRot;

        if (constrainX)
        {
            targetPos.x = initPosition.x;
     //Debug.Log("constraining X");
        } else
        {
            targetPos.x = transform.position.x;
        }

        if (constrainY)
        {
            targetPos.y = initPosition.y;
     //Debug.Log("constraining Y");
        }
        else
        {
            targetPos.y = transform.position.y;
        }

        if (constrainZ)
        {
            targetPos.z = initPosition.z;
     //Debug.Log("constraining Z");
        }
        else
        {
            targetPos.z = transform.position.z;
        }


        if (constrainXRot)
        {
            targetRot.x = initRotation.x;
     //Debug.Log("constraining X");
        } else
        {
            targetRot.x = transform.localEulerAngles.x;
        }

        if (constrainYRot)
        {
            targetRot.y = initRotation.y;
     //Debug.Log("constraining Y");
        }
        else
        {
            targetRot.y = transform.localEulerAngles.y;
        }

        if (constrainZRot)
        {
            targetRot.z = initRotation.z;
     //Debug.Log("constraining Z");
        }
        else
        {
            targetRot.z = transform.localEulerAngles.z;
        }


        transform.position = targetPos;
        transform.localEulerAngles = targetRot;
    }
}
