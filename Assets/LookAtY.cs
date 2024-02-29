using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtY : MonoBehaviour
{
    public Transform target;
    private Vector3 initRotation;
    // Start is called before the first frame update
    void Start()
    {
        initRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        transform.localEulerAngles = new Vector3(initRotation.x, transform.localEulerAngles.y + 270f, initRotation.z);
    }
}
