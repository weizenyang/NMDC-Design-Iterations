using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleUIViewability : MonoBehaviour
{
    Coroutine waitCoroutine;
    Vector3 initPosition;
    public bool transition;
    public float timeout;
    [SerializeField] private bool StartVisibility;
    float transitionSlider = 0f;

    enum transitionState
    {
        Forward,
        Backward,
        None
    }

    transitionState tState;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = gameObject.transform.localPosition;
        transform.localPosition = initPosition - new Vector3(0f, 0.1f, 0f);
        gameObject.SetActive(StartVisibility);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transition && tState == transitionState.Forward)
        {
            transitionSlider += 0.1f;
        }
        else if (transition && tState == transitionState.Backward)
        {

            transitionSlider -= 0.1f;
        }

        //Set UI color
        Color c = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, EaseInOut(transitionSlider));

        
        Image[] childrenComponents = gameObject.GetComponentsInChildren<Image>();
        foreach (Image child in childrenComponents)
        {
            Color ct = child.color;
            child.color = new Color(ct.r, ct.g, ct.b, EaseInOut(transitionSlider));
        }

        TMP_Text[] childrenText = gameObject.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text child in childrenText)
        {
            Color ct = child.color;
            child.color = new Color(ct.r, ct.g, ct.b, EaseInOut(transitionSlider));
        }


        Debug.Log("transition slider " + transitionSlider);

        if(tState == transitionState.Backward && transitionSlider <= 0f)
        {
            tState = transitionState.None;
            gameObject.SetActive(false);
        }
        transitionSlider = Mathf.Clamp(transitionSlider, 0f, 1f);
        float yPos = Mathf.Lerp(initPosition.y - 0.1f, initPosition.y, EaseInOut(transitionSlider));
        transform.localPosition = new Vector3(initPosition.x, yPos, initPosition.z);
    }
    public void showPanel()
    {
        //if (waitCoroutine != null)
        //{
        //    StopCoroutine(waitCoroutine);
        //    waitCoroutine = null;
        //}
        //waitCoroutine = StartCoroutine(WaitAndHide(timeout));

        gameObject.SetActive(true);
        tState = transitionState.Forward;


    }

    public void showPanelForDuration()
    {
        gameObject.SetActive(true);
        tState = transitionState.Forward;

        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
        waitCoroutine = StartCoroutine(WaitAndHide(timeout));

    }

    public void hidePanel()
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
        waitCoroutine = StartCoroutine(WaitAndHide(timeout));

    }

    public void toggleVisibility()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            tState = transitionState.Forward;
        } else
        {
            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            waitCoroutine = StartCoroutine(WaitAndHide(timeout));
        }
    }

    IEnumerator WaitAndHide(float time)
    {

        Debug.Log("Wait and print");
        yield return new WaitForSeconds(time / 2);
        tState = transitionState.Backward;

    }

    public void setPosition(Vector3 position)
    {
        gameObject.GetComponentInParent<Follow>().setPosition(position, timeout);
    }

    public void setRotation(Vector3 rotation)
    {
        gameObject.GetComponentInParent<Follow>().setRotation(rotation, timeout);
    }

    public static float EaseInOut(float x)
    {
        return x < 0.5f
            ? (1 - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f
            : (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
    }


}
