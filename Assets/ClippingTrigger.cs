using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float distanceToTrigger;
    [SerializeField] private GrowSphere growSphere;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(targetObject.transform.position, transform.position) < distanceToTrigger)
        {
            growSphere.startGrowFunc();
        } else
        {
            growSphere.stopGrowFunc();
        }
    }
}
