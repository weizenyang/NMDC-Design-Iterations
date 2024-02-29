using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor1;
    [SerializeField] private XRRayInteractor rayInteractor2;
    [SerializeField] private GameObject rightController;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private InputActionReference activate1;
    [SerializeField] private InputActionReference activate2;
    [SerializeField] private bool allowScale = true;
    [SerializeField] private bool allowMove = true;

    Vector3 initPosition = Vector3.zero;
    Vector3 currentPosition;
    bool triggerDown = false;
    bool transforming = false;
    float initYPosition = 0f;

    float scaleFactor = 1f;
    List<GameObject> list = new List<GameObject>();
    List<Vector3> initScales = new List<Vector3>();

    private void Start()
    {
        initYPosition = targetObject.transform.position.y;
        list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Transformable"));
        foreach (GameObject go in list)
        {
            Vector3 temp = go.transform.localScale;
            initScales.Add(temp);
        }
    }

    public void OnRayHoverEntered(XRBaseInteractable interactable)
    {
        // Access information about the interactable object

        // You can perform other actions here based on the interactable object
    }

    private void OnEnable()
    {
        activate1.action.Enable();
        activate1.action.started += OnTriggerPress;
        activate1.action.canceled += OnTriggerRelease;
    }

    void OnDisable()
    {
        // Disable the input action
        activate1.action.Disable();

        // Unsubscribe from the trigger press event
        activate1.action.started -= OnTriggerPress;
        activate1.action.canceled -= OnTriggerRelease;
    }

    void OnTriggerPress(InputAction.CallbackContext context)
    {
        // Handle trigger press event
        Debug.Log("Trigger pressed!");
        triggerDown = true;
        initPosition = rightController.transform.position;
    }

    void OnTriggerRelease(InputAction.CallbackContext context)
    {
        // Handle trigger press event
        Debug.Log("Trigger pressed!");
        triggerDown = false;
    }

    private void Update()
    {
        Vector3 point = new Vector3(0, 0, 0);
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (triggerDown)
        {
            if (rayInteractor1.enabled)
            {
                if (rayInteractor1.TryGetCurrent3DRaycastHit(out RaycastHit hit))
                {

                    Debug.Log(hit.transform.gameObject.layer);
                    if (hit.transform.gameObject.layer == 10 && !isOverUI)
                    {
                        transforming = true;
                    }
                    
                }
            }
        } else
        {
            transforming = false;
        }

        if (transforming)
        {
            currentPosition = rightController.transform.position;
            if (allowMove)
            {
                targetObject.transform.position += currentPosition - initPosition;
            }

            if (allowScale)
            {
                scaleFactor += (currentPosition.y - initPosition.y) * 1f;
            }
            
            for (int i=0; i<list.Count; i++)
            {
                GameObject transformable = list[i];
                Vector3 currentScale = Vector3.Scale(initScales[i], new Vector3(scaleFactor, scaleFactor, scaleFactor));
                Debug.Log(scaleFactor);
                transformable.transform.localScale = currentScale;
            }
            
            //targetObject.transform.position.Scale(new Vector3(1, 1, 0));
            targetObject.transform.position = new Vector3(targetObject.transform.position.x, initYPosition, targetObject.transform.position.z);
            initPosition = rightController.transform.position;
        }
        


    }

    public void startTransform()
    {
        transforming = true;
        initPosition = currentPosition;
    }

    public void stopTransform()
    {
        transforming = false;
        initPosition = currentPosition;
    }

}
