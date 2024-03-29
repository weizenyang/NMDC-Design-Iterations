using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ScaleOnTouch : MonoBehaviour
{

    bool transforming = false;
    [SerializeField] private GameObject targetObject;
    Vector3 initPosition = Vector3.zero;
    Vector3 currentPosition;
    Vector3 tempRot;
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

        if (transforming)
        {

            currentPosition = activeController.transform.position;
            Vector3 v1 = currentPosition - transform.position;
            Vector3 v2 = initPosition - transform.position;
            float signedAngle = Vector3.SignedAngle(v1, v2, Vector3.up);
            float tempVal = signedAngle;
            Vector3 tempScale = new Vector3(tempVal, tempVal, tempVal);
            targetObject.transform.localScale += tempScale * 0.0002f;
            Debug.Log("Transforming" + signedAngle);
            initPosition = activeController.transform.position;
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
