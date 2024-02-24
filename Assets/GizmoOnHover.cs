using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GizmoOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject parentPanel;
    public RectTransform childCanvas;
    public SetRotation setRotation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(15.59f, 0.2f);
        parentPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 40);
        childCanvas.sizeDelta = new Vector2(0.7f, 0.05f);
        setRotation.hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(-10.77f, 0.03f);
        parentPanel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        childCanvas.sizeDelta = new Vector2(-10.77f, 0.03f);
        setRotation.hovered = false;
    }
}