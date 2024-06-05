using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.GraphicsTools;
using UnityEngine;

public class Recalibration : MonoBehaviour
{
    [SerializeField] private Transform transformParent;
    Vector3 initPosition;
    Vector3 initRotation;
    Vector3 targetInitRotation;
    bool calibration = false;
    Coroutine coroutine = null;
    Coroutine coroutine2 = null;
    public ClippingSphere clippingSphere;
    float targetScale = 1f;
    float currentScale = 1f;
    float animProgress = 0f;
    Vector3 targetPosition = Vector3.zero;
    Vector3 currentPosition = Vector3.zero;
    [SerializeField] AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (calibration)
        {
            animProgress += 0.05f;
            Debug.Log("Calibratng");
            initPosition = gameObject.transform.position;
            initRotation = gameObject.transform.localEulerAngles;


            

            if (transformParent != null)
            {
                //gameObject.transform.position = transformParent.position + new Vector3(0f, 0.03f, 0f);
                targetPosition = transformParent.position;
                //gameObject.transform.localEulerAngles += Vector3.Scale(new Vector3(0, 1, 0), rotationDelta);

            }
            //targetInitRotation = transformParent.transform.eulerAngles - targetInitRotation;
        }
        else
        {
            animProgress -= 0.07f;
        }
        Debug.Log("animProgress: " + animProgress);
        animProgress = Mathf.Clamp(animProgress, 0f, 1f);
        currentScale = EaseInOutQuad(1f, 0.2f, animProgress);


        var normalizedProgress = animProgress; // 0-1
        var easing = curve.Evaluate(normalizedProgress);

        gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);


        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + new Vector3(0f, 0.06f, 0f), easing);





    }

    IEnumerator Waitfor(float time)
    {
        yield return new WaitForSeconds(time);
        calibration = true;

    }

    IEnumerator WaitToSpawnSphere(float time)
    {
        yield return new WaitForSeconds(time);
        clippingSphere.enabled = true;

    }

    public void startRecalibration()
    {
        Debug.Log("Calibration called");
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(Waitfor(1));
        clippingSphere.enabled = false;
    }

    void recalibrationSequence()
    {
        Debug.Log("Calibratng");
        //initPosition = gameObject.transform.position;
        //initRotation = gameObject.transform.localEulerAngles;

        //if (targetInitRotation == new Vector3(999, 999, 999))
        //{
        //    targetInitRotation = transformParent.transform.eulerAngles;
        //}

        //Vector3 rotationDelta = transformParent.transform.eulerAngles - targetInitRotation;

        //gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //if (transformParent != null)
        //{
        //    gameObject.transform.position = transformParent.position;
        //    gameObject.transform.localEulerAngles += Vector3.Scale(new Vector3(0, 1, 0), rotationDelta);

        //}
        //targetInitRotation = transformParent.transform.eulerAngles - targetInitRotation;
    }

    public void stopRecalibration()
    {
        Debug.Log("Stop calibration called");
        targetInitRotation = new Vector3(999, 999, 999);
        //gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        calibration = false;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if (coroutine2 != null)
        {
            StopCoroutine(coroutine2);
            coroutine2 = null;
        }

        coroutine2 = StartCoroutine(WaitToSpawnSphere(1.5f));
    }

    public static float EaseInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }
}
