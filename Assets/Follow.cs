using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject go;
    public Vector3 offset;
    public bool position;
    public bool rotation;
    //Bool to activate follow
    public bool followPos;
    public bool followRot;
    Coroutine waitCoroutine;
    Coroutine waitCoroutine2;

    // Start is called before the first frame update
    void Start()
    {
        followPos = true;
        followRot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (go != null)
        {

            if (position && followPos)
            {
                transform.position = go.transform.position;

            }

            if (rotation && followRot)
            {
                transform.rotation = go.transform.rotation;
            }

        }
    }


    IEnumerator WaitAndHide(float time, string type)
    {

        Debug.Log("Wait and print");
        yield return new WaitForSeconds(time);
        if(type == "position")
        {
            followPos = true;
        } else if (type == "rotation")
        {
            followRot = true;
        }

    }

    public void setPosition(Vector3 position, float? timeOut)
    {
        followPos = false;
        transform.position = position;

        if (timeOut.HasValue)
        {
            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            waitCoroutine = StartCoroutine(WaitAndHide(timeOut.Value * 2, "position"));
        }



    }

    public void setRotation(Vector3 rotation, float? timeOut)
    {
        followRot = false;
        transform.eulerAngles = rotation;

        if (timeOut.HasValue)
        {
            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            waitCoroutine2 = StartCoroutine(WaitAndHide(timeOut.Value * 2, "position"));
        }
    }
}
