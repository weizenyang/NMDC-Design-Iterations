using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableRayInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] XRRayInteractor interactor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        interactor.enabled = false;
    }

    private void OnDisable()
    {
        interactor.enabled = true;
    }
}
