using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveOnTouch : MonoBehaviour
{
    bool transforming = false;
    [SerializeField] private GameObject targetObject;
    Vector3 initPosition = Vector3.zero;
    Vector3 currentPosition;
    public GameObject userPosition;
    GameObject activeController;
    [SerializeField] GameObject UIObject;
    // Start is called before the first frame update
    void Start()
    {
        UIObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 255);
    }

    void OnEnable()
    {
        UIObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 255);
    }

    void OnDisable()
    {
        UIObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {

        if(transforming)
        {
            
            currentPosition = activeController.transform.position;

            Vector3 tempPos = (currentPosition - initPosition);
            targetObject.transform.position += tempPos;
            Debug.Log("Transforming" + tempPos);
            initPosition = activeController.transform.position;
            targetObject.transform.localPosition = new Vector3(targetObject.transform.localPosition.x, 0f, targetObject.transform.localPosition.z);
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
