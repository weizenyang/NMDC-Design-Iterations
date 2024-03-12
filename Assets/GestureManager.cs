using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public SetScale setScale;
    public SetRotation setRotation;
    private string leftGesture;
    private string rightGesture;
    private float initDistance = 9999f;
    private Vector3 initLeftHandPosition;
    private Vector3 initRightHandPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Gesture: " + leftGesture + " " + rightGesture);
        if (leftGesture == "Stretch" && rightGesture == "Stretch")
        {
            float distance = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            Debug.Log("Gesture Distance: " + distance);
            if (initDistance == 9999f)
            {
                initDistance = distance;
            }

            //Add scale delta in setScale
            float distanceChange = distance - initDistance;
            Debug.Log("Gesture Distance Change: " + distanceChange);
            setScale.externalAddScale(distanceChange * 0.035f);

            initDistance = distance;
        } else if (leftGesture == "Pinch" && rightGesture == "Pinch")
        {
            //Get midpoint between 2 hands
            Vector3 midpoint = (leftHand.transform.position + rightHand.transform.position) / 2;

            if(initLeftHandPosition == null || initLeftHandPosition == new Vector3(99f, 99f, 99f))
            {
                initLeftHandPosition = leftHand.transform.position;
            }

            if (initRightHandPosition == null || initRightHandPosition == new Vector3(99f, 99f, 99f))
            {
                initRightHandPosition = rightHand.transform.position;
            }

            //Get movement vectors of each hand
            Vector3 leftHandVector = leftHand.transform.position - initLeftHandPosition;
            Vector3 rightHandVector = rightHand.transform.position - initRightHandPosition;

            Vector3 leftA = midpoint - leftHand.transform.position;
            Vector3 leftB = midpoint - initLeftHandPosition;

            Vector3 rightA = midpoint - rightHand.transform.position;
            Vector3 rightB = midpoint - initRightHandPosition;

            //float leftAngleChange = Mathf.Acos((leftA * leftB));
            float leftAngleChange = Vector3.SignedAngle(leftA, leftB, Vector3.up);
            float rightAngleChange = Vector3.SignedAngle(rightA, rightB, Vector3.up);
            float totalAngleChange = (leftAngleChange + rightAngleChange);

            setRotation.externalAddRotation(totalAngleChange);

            //setRotation

            initLeftHandPosition = leftHand.transform.position;
            initRightHandPosition = rightHand.transform.position;

            Debug.Log("Angle Change: Left(" + leftAngleChange + "), Right(" + rightAngleChange + ")");

        }
        else
        {
            initDistance = 9999f;
            initLeftHandPosition = new Vector3(99f, 99f, 99f);
            initRightHandPosition = new Vector3(99f, 99f, 99f);
        }
    }

    public void updateLeftGesture(string gesture)
    {
        leftGesture = gesture;
    }

    public void updateRightGesture(string gesture)
    {
        rightGesture = gesture;
    }
}
