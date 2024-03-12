using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementManager1 : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor1;
    [SerializeField] private XRRayInteractor rayInteractor2;
    [SerializeField] private GameObject rightController;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject targetPlinth;
    [SerializeField] private InputActionReference activate1;
    [SerializeField] private InputActionReference activate2;
    private Vector3 initUIPosition;
    public bool allowScale;
    public bool allowMove;
    public bool allowRotate;
    public bool allowInteraction;

    private float scaleMagnitude;
    private float rotateMagnitude;
    private GameObject[] sphereInstances = new GameObject[2];
    public GameObject spherePrefab;

    Vector3 initPosition = Vector3.zero;
    Vector3 currentPosition;
    bool triggerDown = false;
    bool transforming = false;
    float initYPosition = 0f;

    float scaleFactor = 1f;
    List<GameObject> list = new List<GameObject>();
    List<Vector3> initScales = new List<Vector3>();

    //Touch Controls
    GameObject rotateControls;


    private void Start()
    {
        initYPosition = targetObject.transform.localPosition.y;
        initUIPosition = transform.localPosition;
        list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Transformable"));
        foreach (GameObject go in list)
        {
            Vector3 temp = go.transform.localScale;
            initScales.Add(temp);
        }
        // Create a new GameObject for the sphere
        //sphereInstances[1] = GameObject.Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);
        //sphereInstances[0] = GameObject.Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);

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
        //bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        


        
        if (rayInteractor1.enabled)
        {
            if (rayInteractor1.TryGetCurrent3DRaycastHit(out RaycastHit hit1))
            {

                //I hate myself for this
                    if (hit1.transform.gameObject.layer == 5 && hit1.transform.gameObject == this.gameObject)
                    {
                        GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 255);

                    }
             }


            
            if (triggerDown)
            {
                GetComponent<BoxCollider>().enabled = false;

                if (rayInteractor1.TryGetCurrent3DRaycastHit(out RaycastHit hit))
                {
                Debug.Log(hit.transform.gameObject);

                
                    if (allowInteraction)
                    {
                        if (hit.transform.gameObject.layer == 5 && hit.transform.gameObject == this.gameObject)
                        {
                            allowInteraction = false;
                            transforming = true;
                            GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 255);

                        }
                    }
                    
                
                }
            }
            else
            {
                allowInteraction = true;
                GetComponent<BoxCollider>().enabled = true;
                transform.parent.gameObject.transform.Find("Border").gameObject.SetActive(false);
                transforming = false;
                GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 0);



            }
        } 
        
        if (transforming)
        {
            currentPosition = rightController.transform.position;
            transform.parent.gameObject.transform.Find("Border").gameObject.SetActive(true);

            if (allowMove)
            {
                Vector3 tempPos = (currentPosition - initPosition);
                targetObject.transform.position += currentPosition - initPosition;
                

                transform.localPosition = initUIPosition + new Vector3(Mathf.Clamp(tempPos.z * 100f, -0.5f, 0.5f), Mathf.Clamp(tempPos.x * 100f, -0.5f, 0.5f), 0f);
            }

            if (allowScale)
            {
                Vector3 buttonForward = transform.forward;
                scaleMagnitude = MagnitudeBetweenProjectedPoints(initPosition, currentPosition, transform.position, buttonForward);

                scaleFactor -= scaleMagnitude;
                for (int i = 0; i < list.Count; i++)
                {
                    GameObject transformable = list[i];
                    //Vector3 currentScale = Vector3.Scale(initScales[i], new Vector3(scaleFactor, scaleFactor, scaleFactor));
                    Vector3 currentScale = transformable.transform.localScale * scaleMagnitude;
                    Vector3 tempLocalScale = transformable.transform.localScale;
                    tempLocalScale -= currentScale;
                    transformable.transform.localScale -= currentScale;
                    

                }
                transform.localPosition = initUIPosition + new Vector3(Mathf.Clamp(-scaleMagnitude * 100f, -0.45f, 0.45f), Mathf.Clamp(-scaleMagnitude * 100f, -0.45f, 0.45f), 0f);

            }

            if (allowRotate)
            {
                Vector3 buttonForward = transform.forward;
                rotateMagnitude = MagnitudeBetweenProjectedPoints(initPosition, currentPosition, transform.position, buttonForward);
                targetObject.transform.localEulerAngles += new Vector3(0, rotateMagnitude * 1800f, 0);
                targetPlinth.transform.localEulerAngles += new Vector3(0, rotateMagnitude * 1800f, 0);
                transform.localPosition = initUIPosition + new Vector3(0f, Mathf.Clamp(rotateMagnitude * 300f, -0.45f, 0.45f), 0f);
                Vector3 tempLocalRotation = targetObject.transform.localEulerAngles;
                

            }



            
            //targetObject.transform.position.Scale(new Vector3(1, 1, 0));
            targetObject.transform.localPosition = new Vector3(targetObject.transform.localPosition.x, initYPosition, targetObject.transform.localPosition.z);
            initPosition = rightController.transform.position;
        } else
        {
            transform.localPosition = initUIPosition;

        }
        


    }

    public Vector3 ProjectPointOnPlane(Vector3 point, Vector3 origin, Vector3 normal)
    {
        // Calculate the vector from the origin to the point
        Vector3 v = point - origin;

        // Ensure the normal vector has a magnitude of 1 (unit vector)
        normal = normal.normalized;

        // Calculate the distance from the point to the plane along the normal vector
        float distance = Vector3.Dot(v, normal);

        // Project the point onto the plane by subtracting the distance along the normal vector
        Vector3 projectedPoint = point - distance * normal;

        return projectedPoint;
    }

    public float MagnitudeBetweenProjectedPoints(Vector3 point1, Vector3 point2, Vector3 origin, Vector3 normal)
    {
        // Project both points onto the plane
        Vector3 projectedPoint1 = ProjectPointOnPlane(point1, origin, normal);
        Vector3 projectedPoint2 = ProjectPointOnPlane(point2, origin, normal);

        //sphereInstances[0].transform.position = projectedPoint1;
        //sphereInstances[1].transform.position = projectedPoint2;

        // Calculate the magnitude between the projected points
        Vector3 difference = projectedPoint1 - projectedPoint2;
        Debug.Log("Difference: " + difference);
        float magnitude = projectedPoint1.magnitude - projectedPoint2.magnitude;

        return magnitude;
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

    public Vector3 getMoveSignedMagnitude() {
        Vector3 vector3 = currentPosition;
        if (transforming)
        {
            vector3 = currentPosition - initPosition;

        } else
        {
            vector3 = new Vector3(0f,0f,0f);
        }

        vector3 = new Vector3(vector3.x, 0, vector3.y);

        return vector3;
    }

    public Vector3 getRotateSignedMagnitude()
    {
        Vector3 vector3 = currentPosition;
        if (transforming)
        {
            vector3 = currentPosition - initPosition;

        }
        else
        {
            vector3 = new Vector3(0f, 0f, 0f);
        }

        vector3 = new Vector3(vector3.x, 0, vector3.y);

        return vector3;
    }

    public Vector3 getScaleSignedMagnitude()
    {
        Vector3 vector3 = currentPosition;
        if (transforming)
        {
            vector3 = currentPosition - initPosition;

        }
        else
        {
            vector3 = new Vector3(0f, 0f, 0f);
        }

        vector3 = new Vector3(vector3.x, 0, vector3.y);

        return vector3;
    }

}
