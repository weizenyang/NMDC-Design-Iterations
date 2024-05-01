using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowSphere : MonoBehaviour
{

    public bool startGrow = false;
    public GameObject targetGo;
    private float scale = 0f;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private float distanceToTrigger;
    [SerializeField] private GrowSphere growSphere;
    public Vector3 updatedPosition = Vector3.zero;  

    [SerializeField]
    private float maxScale = 0.4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetGo != null)
        {
            if (startGrow)
            {

                scale += (maxScale - scale) * 0.05f;
                targetGo.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                scale += (0.1f - scale) * 0.05f;
                targetGo.transform.localScale = new Vector3(scale, scale, scale);
            }
        }

        if (Vector3.Distance(targetObject.transform.position, transform.position) < distanceToTrigger)
        {
            growSphere.startGrowFunc();
            targetGo.transform.localPosition = new Vector3(0f, 0f, 0f);
            updatedPosition = transform.position;
        }
        else
        {
            growSphere.stopGrowFunc();
            targetGo.transform.position = updatedPosition;
        }
    }

    public void startGrowFunc()
    {
        startGrow = true;
    }

    public void stopGrowFunc()
    {
        startGrow = false;
    }



    private void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.gameObject.layer);
        if(c.gameObject.layer == 9)
        {
            startGrow = true;
        }
        
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.layer == 9)
        {
            startGrow = false;

        }
    }
}
