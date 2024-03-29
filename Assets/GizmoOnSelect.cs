using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GizmoOnSelect : MonoBehaviour
{
    public GameObject parentPanel;
    public RectTransform childCanvas;
    public SetRotation setRotation;
    public SetScale setScale;
    public GameObject targetGo;

    [SerializeField] bool rotate;
    [SerializeField] bool scale;
    [SerializeField] bool move;

    private void Start()
    {
        //OnShrink();
    }
    public void OnExpand()
    {
        RectTransform rt = GetComponent<RectTransform>();
        //rt.sizeDelta = new Vector2(15.59f, 0.2f);
        parentPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 40);
        parentPanel.gameObject.SetActive(true);
        //childCanvas.sizeDelta = new Vector2(0.7f, 0.05f);
        //childCanvas.sizeDelta = new Vector2(0.7f, 0.05f);
        if (setRotation != null)
        {
            setRotation.hovered = true;
        }

        if (setScale != null)
        {
            setScale.hovered = true;
        }

        //setScaleValue(targetGo.transform.localScale.x / 0.02f);
        Debug.Log("SetScaleValue" + targetGo.transform.localScale.x);


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
        //setRotateValue(targetGo.transform.localEulerAngles.y / 360f);
    }

    public void OnShrink()
    {
        RectTransform rt = GetComponent<RectTransform>();
        //rt.sizeDelta = new Vector2(-16f, 0f);
        //parentPanel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        //parentPanel.gameObject.SetActive(false);
        //childCanvas.sizeDelta = new Vector2(-10.77f, 0.03f);
        if (setRotation != null)
        {
            setRotation.hovered = false;
        }

        if (setScale != null)
        {
            setScale.hovered = false;
        }
    }

    public void setRotateValue(float scrollValue)
    {
        Debug.Log("RotateValueEuler " + targetGo.transform.localEulerAngles.y);
        Debug.Log("RotateValueNormalizedb4 " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
        //parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollValue;
        Debug.Log("RotateValueNormalizedAfter " + parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition);
    }

    public void setScaleValue(float scrollValue)
    {
        //parentPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollValue;
    }
}
