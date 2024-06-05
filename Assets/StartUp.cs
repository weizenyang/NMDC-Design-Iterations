using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MixedReality.Toolkit.UX.Experimental.NonNativeFunctionKey;

public class StartUp : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1f);// Wait for one second
        gameObject.transform.position = getSpawnPosition(cameraTransform);

    }

    Vector3 getSpawnPosition(Transform cameraTransform)
    {
        Vector3 position = cameraTransform.position + cameraTransform.forward * 1.2f;
        position.y = 1.67f;

        Debug.Log("Spawn Pos: " + position);

        return position;

    }


}
