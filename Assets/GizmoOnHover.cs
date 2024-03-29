using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using static UnityEngine.Rendering.DebugUI;

public class GizmoOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject parentPanel;
    public RectTransform childCanvas;
    public SetRotation setRotation;
    public SetScale setScale;
    public SetPosition setPosition;
    public bool hoverEnteredTrigger;
    public bool hoverExitedTrigger;
    public GameObject targetGo;
    public GameObject textObject;
    public GameObject player;

    public float[] rotation = new float[30];
    public float currentRotation = 0;

    //Rotation Properties
    float targetYRotation = 0;
    float currentYRotation = 0;

    //Scale Properties
    float initScaleFactor = 0f;
    float targetScaleFactor = 0.01f;
    float currentScaleFactor = 0f;

    //Position Properties
    Vector3 initPosition;
    Vector3 targetPosition;
    Vector3 currentPosition;

    //Is using Gesture?
    bool isGesture = false;

    private bool showPanel = false;
    private bool hovering = false;
    private bool manipulating = false;
    private bool safeToRead = true;

    [SerializeField] bool rotate;
    [SerializeField] bool scale;
    [SerializeField] bool move;

    //Disable Poke Targets
    public XRPokeFilter[] filters;

    //Move Poke Targets
    public MoveBy[] UI;

    private bool accessedFromOtherCode = false;

    Coroutine waitCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;

        }

        Debug.Log("PointerEnter");
        if (hoverEnteredTrigger)
        {
            //RectTransform rt = GetComponent<RectTransform>();
            //rt.sizeDelta = new Vector2(15.59f, 0.5f);
            //parentPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 40);
            //parentPanel.gameObject.SetActive(true);
            //childCanvas.sizeDelta = new Vector2(0.7f, 0.05f);
            
            if (setRotation != null)
            {
                setRotation.hovered = true;
            }

            if (setScale != null)
            {
                setScale.hovered = true;
            }

        }

        hovering = true;
        showPanel = true;

    }

    IEnumerator WaitAndPrint(float time)
    {

        Debug.Log("Wait and print");
        yield return new WaitForSeconds(time);
        manipulating = false;

        //this.gameObject.GetComponent<ScalePanel>().ScaleDownRemainPos();
        this.gameObject.GetComponentInChildren<ButtonAnimation>().Revert();
        yield return new WaitForSeconds(time / 4);
        //Enable Target Poke Filters
        foreach (XRPokeFilter f in filters)
        {
            f.enabled = true;
        }

        foreach (MoveBy f in UI)
        {
            f.RevertPosition();
        }
        hovering = false;

    }

    void OnEnable()
    {
        if (rotate)
        {
            parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = targetGo.transform.localEulerAngles.y / 360f;
        }
        if (scale)
        {
            parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = targetGo.transform.localScale.y / 0.02f;
        }
        if (move)
        {

        }
        
    }




    public void OnPointerExit(PointerEventData eventData)
    {

        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;

        }

        waitCoroutine = StartCoroutine(WaitAndPrint(0.5f));
        hovering = false;
    }

    private void Update()
    {
        if (!hovering && !manipulating)
        {
            //parentPanel.gameObject.SetActive(false);
            //childCanvas.sizeDelta = new Vector2(0.7f, 0.0f);
        }
    }

    public void OnScrollValueChange(Vector2 value)
    {
        Debug.Log("Scroll Change Detected");

        if(safeToRead)
        {
            currentRotation = value.x;

            if (!hovering)
            {
                if (waitCoroutine != null)
                {
                    StopCoroutine(waitCoroutine);
                    waitCoroutine = null;

                }

                waitCoroutine = StartCoroutine(WaitAndPrint(0.5f));
            }

            if (textObject != null)
            {
                textObject.GetComponent<GetPercentage>().setValue(((value.x)).ToString("#%"));
            }

            if (move)
            {
                Debug.Log("Move: " + value.x + "," + value.y);
                targetPosition = Vector3.Scale(new Vector3(value.x, 0f, value.y), (player.transform.position - targetGo.transform.position));
                targetPosition.y = 0f;
                currentPosition += (targetPosition - currentPosition) * 0.05f;
                if (setPosition != null)
                {
                    Debug.Log("Calling Set Position");
                    setPosition.externalSetPosition(currentPosition);
                }
            }

            if (scale)
            {
                currentScaleFactor = (value.x * 0.02f);
                currentScaleFactor += (targetScaleFactor - currentScaleFactor) * 0.05f;
                if (setScale != null)
                {
                    setScale.externalSetScale(currentScaleFactor);
                }
            }


            if (rotate)
            {
                Debug.Log(value);
                targetYRotation = value.x * 360f;
                currentYRotation += (targetYRotation - currentYRotation) * 0.05f;
                if (setRotation != null)
                {
                    setRotation.externalSetRotation(currentYRotation);
                }
            }
        } else
        {
            if (rotate)
            {
                parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = targetGo.transform.localEulerAngles.y / 360f;
            }
            if (scale)
            {
                parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = targetGo.transform.localScale.y / 0.02f;
            }
        }




    }

    public void setScaleValue(float scrollValue)
    {
        Debug.Log("RotateValueNormalizedb4 " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollValue;
        Debug.Log("RotateValueNormalizedAfter " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        Debug.Log("RotateValue " + scrollValue);
    }
    public void setRotateValue(float scrollValue)
    {
        Debug.Log("RotateValueNormalizedb4 " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollValue;
        Debug.Log("RotateValueNormalizedAfter " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        Debug.Log("RotateValue " + scrollValue);
    }

    public void addScaleValue(float scrollValue)
    {
        Debug.Log("RotateValueNormalizedb4 " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition -= scrollValue;
        Debug.Log("RotateValueNormalizedAfter " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        Debug.Log("RotateValue " + scrollValue);
    }
    public void addRotateValue(float scrollValue)
    {
        Debug.Log("RotateValueNormalizedb4 " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition -= scrollValue;
        Debug.Log("RotateValueNormalizedAfter " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        Debug.Log("RotateValue " + scrollValue);
    }

    public void setSafeToRead(bool value)
    {
        safeToRead = value;
    }



    public float getValue()
    {
        return parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition;
        
    }

    public void isUsingGesture(bool state)
    {
        isGesture = state;
    }

}