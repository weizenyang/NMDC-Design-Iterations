using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIViewability : MonoBehaviour
{
    Coroutine waitCoroutine;
    Vector3 initPosition;
    public bool transition;
    public float timeout;
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
        gameObject.SetActive(false);
        

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

    IEnumerator WaitAndHide(float time)
    {

        Debug.Log("Wait and print");
        yield return new WaitForSeconds(time / 2);
        tState = transitionState.Backward;
        

        
    }

    public static float EaseInOut(float x)
    {
        return x < 0.5f
            ? (1 - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f
            : (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
    }


}
