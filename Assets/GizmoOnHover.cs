using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class GizmoOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject parentPanel;
    public RectTransform childCanvas;
    public SetRotation setRotation;
    public SetScale setScale;
    public bool hoverEnteredTrigger;
    public bool hoverExitedTrigger;
    public GameObject targetGo;

    public float[] rotation = new float[30];
    public float currentRotation = 0;

    private bool showPanel = false;
    private bool hovering = false;
    private bool manipulating = false;

    private bool accessedFromOtherCode = false;

    Coroutine waitCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEnteredTrigger)
        {
            //RectTransform rt = GetComponent<RectTransform>();
            //rt.sizeDelta = new Vector2(15.59f, 0.5f);
            //parentPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 40);
            parentPanel.gameObject.SetActive(true);
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
        yield return new WaitForSeconds(time); // Wait for 2 seconds
        manipulating = false;
    }





    public void OnPointerExit(PointerEventData eventData)
    {
        //if (hoverExitedTrigger)
        //{
        //    RectTransform rt = GetComponent<RectTransform>();
        //    //rt.sizeDelta = new Vector2(-16f, 0f);
        //    parentPanel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        //    //childCanvas.sizeDelta = new Vector2(-16f, 0f);
        //    parentPanel.gameObject.SetActive(false);

        //    if (setRotation != null)
        //    {
        //        setRotation.hovered = false;
        //    }

        //    if (setScale != null)
        //    {
        //        setScale.hovered = false;
        //    }
        //}

        hovering = false;
    }

    private void Update()
    {
        if (!hovering && !manipulating)
        {
            parentPanel.gameObject.SetActive(false);
            //childCanvas.sizeDelta = new Vector2(0.7f, 0.0f);
        }
    }

    public void OnScrollValueChange(Vector2 value)
    {
        Debug.Log("Scroll Change Detected");

        currentRotation = value.x;
        showPanel = true;
        manipulating = true;
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
        waitCoroutine = StartCoroutine(WaitAndPrint(0.5f));
    }

    public void setRotateValue(float scrollValue)
    {
        Debug.Log("RotateValueNormalizedb4 " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollValue;
        Debug.Log("RotateValueNormalizedAfter " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        Debug.Log("RotateValue " + scrollValue);
    }

    public void setScaleValue(float scrollValue)
    {
        parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollValue;
        Debug.Log("ScaleValue " + scrollValue);
    }

}