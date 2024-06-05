using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public SetScale setScale;
    public SetRotation setRotation;
    public SetPosition setPosition;
    public SetLayer setLayer;
    public GameObject player;
    private string leftGesture;
    private string rightGesture;
    private string leftGestureSuffix;
    private string rightGestureSuffix;
    private float initDistance = 9999f;
    private Vector3 initLeftHandPosition;
    private Vector3 initRightHandPosition;
    private float layer;
    private int layerCount;
    Vector3 delta;

    public GameObject layerPanelLeft;
    public GameObject layerPanelRight;
    public GameObject movePanelLeft;
    public GameObject movePanelRight;
    public GameObject rotatePanelLeft;
    public GameObject rotatePanelRight;
    public GameObject scalePanel;

    public bool leftHandInView;
    public bool rightHandInView;

    //Update UI on Gesture State to update scrollbar value
    [SerializeField] GameObject scaleUI;
    [SerializeField] GameObject rotateUI;


    // Start is called before the first frame update
    void Start()
    {
        layerCount = setLayer.getLayerCount();
    }

    // Update is called once per frame
    void Update()
    {

        if (!GetComponent<CheckObjectInViewport>().isInViewport(leftHand))
        {
            leftGesture = "";
        }

        if (!GetComponent<CheckObjectInViewport>().isInViewport(rightHand))
        {
            rightGesture = "";
        }

        string compLeftGesture = leftGesture + " " + leftGestureSuffix;
        string compRightGesture = rightGesture + " " + rightGestureSuffix;

        Debug.Log("Gesture: " + compLeftGesture + " " + compRightGesture);
        if (leftGesture == "Pinch" && rightGesture == "Pinch")
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
            //setScale.externalAddScale(distanceChange * 0.035f);
            //Update UI
            scaleUI.GetComponent<GizmoOnHover>().addScaleValue((-distanceChange * 0.035f) / 0.02f);
            scaleUI.GetComponent<GizmoOnHover>().isUsingGesture(true);
            initDistance = distance;

            scalePanel.GetComponent<ToggleUIViewability>().showPanelForDuration();
            Vector3 uiMidpoint = (leftHand.transform.position + rightHand.transform.position) / 2;
            uiMidpoint.y += 0.1f;
            scalePanel.GetComponent<ToggleUIViewability>().setPosition(uiMidpoint);


        }
        else if (leftGesture != rightGesture && rightGesture == "Pinch")
        {

            if (initRightHandPosition == null || initRightHandPosition == new Vector3(99f, 99f, 99f))
            {
                initRightHandPosition = rightHand.transform.position;
            }

            delta = (rightHand.transform.position - initRightHandPosition);
            delta.y = 0f;
            Debug.Log("Gesture Delta: " + delta);

            movePanelRight.GetComponent<ToggleUIViewability>().showPanelForDuration();

            setPosition.externalAddPosition(delta);

            initRightHandPosition = rightHand.transform.position;



        }
        else if (leftGesture != rightGesture && leftGesture == "Pinch")
        {
            if (initLeftHandPosition == null || initLeftHandPosition == new Vector3(99f, 99f, 99f))
            {
                initLeftHandPosition = leftHand.transform.position;
            }

            delta = (leftHand.transform.position - initLeftHandPosition);
            delta.y = 0f;
            Debug.Log("Gesture Delta: " + delta);

            movePanelLeft.GetComponent<ToggleUIViewability>().showPanelForDuration();

            setPosition.externalAddPosition(delta);

            initLeftHandPosition = leftHand.transform.position;


        }

        else if (leftGesture != rightGesture && leftGesture == "2 Finger Forward")
        {
            if (initLeftHandPosition == null || initLeftHandPosition == new Vector3(99f, 99f, 99f))
            {
                initLeftHandPosition = leftHand.transform.position;
            }

            Vector3 AB = player.transform.position - initLeftHandPosition;
            Vector3 BC = player.transform.position - leftHand.transform.position;
            Vector3 AC = BC - AB;

            float rotationDelta = (AC.x + AC.z);

            rotatePanelLeft.GetComponent<ToggleUIViewability>().showPanelForDuration();

            setRotation.externalAddRotation(rotationDelta * 360f);

            initLeftHandPosition = leftHand.transform.position;


        }

        else if (leftGesture != rightGesture && rightGesture == "2 Finger Forward")
        {

            if (initRightHandPosition == null || initRightHandPosition == new Vector3(99f, 99f, 99f))
            {
                initRightHandPosition = rightHand.transform.position;
            }

            Vector3 AB = player.transform.position - initRightHandPosition;
            Vector3 BC = player.transform.position - rightHand.transform.position;
            Vector3 AC = BC - AB;

            float rotationDelta = (AC.x + AC.z);

            rotatePanelRight.GetComponent<ToggleUIViewability>().showPanelForDuration();

            setRotation.externalAddRotation(rotationDelta * 360f);

            initRightHandPosition = rightHand.transform.position;



        }
        //else if (leftGesture == "Pinch" && rightGesture == "Pinch")
        //{
        //    //Get midpoint between 2 hands
        //    Vector3 midpoint = (leftHand.transform.position + rightHand.transform.position) / 2;

        //    if (initLeftHandPosition == null || initLeftHandPosition == new Vector3(99f, 99f, 99f))
        //    {
        //        initLeftHandPosition = leftHand.transform.position;
        //    }

        //    if (initRightHandPosition == null || initRightHandPosition == new Vector3(99f, 99f, 99f))
        //    {
        //        initRightHandPosition = rightHand.transform.position;
        //    }

        //    rotatePanel.GetComponent<ToggleUIViewability>().showPanelForDuration();

        //    //Get movement vectors of each hand
        //    Vector3 leftHandVector = leftHand.transform.position - initLeftHandPosition;
        //    Vector3 rightHandVector = rightHand.transform.position - initRightHandPosition;

        //    Vector3 leftA = midpoint - leftHand.transform.position;
        //    Vector3 leftB = midpoint - initLeftHandPosition;

        //    Vector3 rightA = midpoint - rightHand.transform.position;
        //    Vector3 rightB = midpoint - initRightHandPosition;

        //    //float leftAngleChange = Mathf.Acos((leftA * leftB));
        //    float leftAngleChange = Vector3.SignedAngle(leftA, leftB, Vector3.up);
        //    float rightAngleChange = Vector3.SignedAngle(rightA, rightB, Vector3.up);
        //    float totalAngleChange = (leftAngleChange + rightAngleChange);

        //    //setRotation.externalAddRotation(totalAngleChange);
        //    //Update UI
        //    rotateUI.GetComponent<GizmoOnHover>().addRotateValue((totalAngleChange) / 360f);
        //    rotateUI.GetComponent<GizmoOnHover>().isUsingGesture(true);

        //    initLeftHandPosition = leftHand.transform.position;
        //    initRightHandPosition = rightHand.transform.position;

        //    Debug.Log("Angle Change: Left(" + leftAngleChange + "), Right(" + rightAngleChange + ")");



        //}

        else if (rightGesture == "2 Finger Stretch" && leftGesture != rightGesture)
        {
            if (initRightHandPosition == null || initRightHandPosition == new Vector3(99f, 99f, 99f))
            {
                initRightHandPosition = rightHand.transform.position;
            }



            Vector3 AB = player.transform.position - initRightHandPosition;
            Vector3 BC = player.transform.position - rightHand.transform.position;
            Vector3 AC = Vector3.Scale(BC - AB, new Vector3(4f, 0f, 4f));

            layer -= (AC.x + AC.z) * 3f;
            layer = Mathf.Clamp(layer, 0, layerCount);

            Debug.Log("Gesture Layer: " + AC + " " + layer);

            setLayer.showLayer((int)layer);
            layerPanelRight.GetComponent<ToggleUIViewability>().showPanelForDuration();


            initRightHandPosition = rightHand.transform.position;
        }
        else if (leftGesture == "2 Finger Stretch" && leftGesture != rightGesture)
        {
            if (initLeftHandPosition == null || initLeftHandPosition == new Vector3(99f, 99f, 99f))
            {
                initLeftHandPosition = leftHand.transform.position;
            }



            Vector3 AB = player.transform.position - initLeftHandPosition;
            Vector3 BC = player.transform.position - leftHand.transform.position;
            Vector3 AC = Vector3.Scale(BC - AB, new Vector3(4f, 0f, 4f));

            layer -= (AC.x + AC.z) * 3f;
            layer = Mathf.Clamp(layer, 0, layerCount);

            Debug.Log("Gesture Layer: " + AC);

            setLayer.showLayer((int)layer);
            layerPanelLeft.GetComponent<ToggleUIViewability>().showPanelForDuration();


            initLeftHandPosition = leftHand.transform.position;
        }
        else
        {
            initDistance = 9999f;
            initLeftHandPosition = new Vector3(99f, 99f, 99f);
            initRightHandPosition = new Vector3(99f, 99f, 99f);
            scaleUI.GetComponent<GizmoOnHover>().isUsingGesture(false);
            rotateUI.GetComponent<GizmoOnHover>().isUsingGesture(false);
            if (delta.x + delta.z > 0f)
            {
                delta -= delta * 0.1f;
            }
            layer = Mathf.Clamp(layer, 0, layerCount - 1);
            layer = (int)layer;
            //Remove decimal once gesture is not detected


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

    public void updateLeftGestureSuffix(string gesture)
    {
        leftGestureSuffix = gesture;
    }

    public void updateRightGestureSuffix(string gesture)
    {
        rightGestureSuffix = gesture;
    }
}
