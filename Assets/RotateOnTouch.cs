using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateOnTouch : MonoBehaviour
{
    bool transforming = false;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject plinth;
    Vector3 initPosition = Vector3.zero;
    Vector3 currentPosition;
    Vector3 tempRot;
    public GameObject userPosition;
    GameObject activeController;
    [SerializeField] GameObject UIObject;
    // Start is called before the first frame update
    void OnEnable()
    {
        UIObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 255);
        tempRot.y = 0;
    }

    void OnDisable()
    {
        UIObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {

        if (transforming)
        {

            currentPosition = activeController.transform.position;

            Vector3 v1 = currentPosition - transform.position;
            Vector3 v2 = initPosition - transform.position;
            float signedAngle = Vector3.SignedAngle(v1, v2, Vector3.up);
            float tempVal = signedAngle;
            tempRot = new Vector3(0f, tempVal, 0f);
            targetObject.transform.localEulerAngles -= tempRot;
            plinth.transform.localEulerAngles -= tempRot;
            Debug.Log("Transforming" + tempVal);
            initPosition = currentPosition;
           
        } else if(tempRot.y != 0f)
        {
            tempRot -= new Vector3(0f, tempRot.y * 1f, 0f) * Time.deltaTime;
            targetObject.transform.localEulerAngles -= tempRot;
            plinth.transform.localEulerAngles -= tempRot;
            Debug.Log("TempRot: " + tempRot.y);
        }
    }

    public void StartTransform()
    {
        transforming = true;
        Debug.Log("Transforming");
    }

    public void StopTransform()
    {
        transforming = false;
        Debug.Log("Transforming");
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log($"Transforming {args.interactorObject.transform.gameObject} position {args.interactableObject.transform.position}", this);
        activeController = args.interactorObject.transform.gameObject;
        initPosition = args.interactorObject.transform.position;
        transforming = true;
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log($"Transforming {args.interactorObject.transform.gameObject} position {args.interactableObject.transform.position}", this);
        activeController = args.interactorObject.transform.gameObject;
        transforming = false;
    }
}
