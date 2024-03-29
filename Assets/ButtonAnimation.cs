using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public Vector2 targetDimensions;
    public Vector2 setInitDimensions;
    public ScalePanel scalePanel;
    public GizmoOnHover goh;
    Vector2 initDimensions;
    Vector2 currentDimensions;
    RectTransform thisRectTransform;
    float currentValue = 0;
    public float speed;
    enum transition {
        None,
        ScaleTo,
        Revert
    };
    transition transitionState;

    // Start is called before the first frame update
    void Start()
    {
        thisRectTransform = GetComponent<RectTransform>();
        if(setInitDimensions != new Vector2(0f, 0f))
        {
            initDimensions = setInitDimensions;
        } else
        {
            initDimensions = new Vector2(thisRectTransform.rect.width, thisRectTransform.rect.height);
        }
        
        transitionState = transition.None;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transitionState == transition.ScaleTo && currentValue < 1f)
        {
            currentValue += speed;
            currentDimensions = new Vector2(Mathf.Lerp(initDimensions.x, targetDimensions.x, EaseInOutCirc(currentValue)), Mathf.Lerp(initDimensions.y, targetDimensions.y, EaseInOutCirc(currentValue)));
            thisRectTransform.sizeDelta = currentDimensions;
            goh.setSafeToRead(false);

        }
        else if (transitionState == transition.Revert && currentValue > 0f)
        {
            currentValue -= speed;
            currentDimensions = new Vector2(Mathf.Lerp(initDimensions.x, targetDimensions.x, EaseInOutCirc(currentValue)), Mathf.Lerp(initDimensions.y, targetDimensions.y, EaseInOutCirc(currentValue)));
            thisRectTransform.sizeDelta = currentDimensions;
            goh.setSafeToRead(false);
        } else if (currentValue <= 0.001f)
        {
            scalePanel.ScaleDownRemainPos();
            goh.setSafeToRead(true);
        }
        else if (currentValue >= 0.999f)
        {
            goh.setSafeToRead(true);
        }

    }
    
    public void TransitionTo() {

        transitionState = transition.ScaleTo;
        scalePanel.ScaleUpRemainPos();
    }

    public void Revert()
    {

        transitionState = transition.Revert;
        //scalePanel.ScaleUpRemainPos();
    }

    public static float EaseInOutCirc(float x)
    {
        return x < 0.5f
            ? (1 - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f
            : (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
    }
}
