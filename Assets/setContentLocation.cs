using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class setContentLocation : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    [SerializeField] InputActionReference aButton;
    [SerializeField] InputActionReference bButton;
    [SerializeField] InputActionReference joystick;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject targetObject;
    [SerializeField] GameObject rightController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        aButton.action.performed += OnAButtonPerformed;
        aButton.action.canceled += OnAButtonCancelled;
        bButton.action.performed += OnBButtonPerformed;
        bButton.action.canceled += OnBButtonCancelled;
        joystick.action.performed += OnJoystickPerformed;
        joystick.action.canceled += OnJoystickCancelled;
    }

    private void OnDisable()
    {
        aButton.action.performed -= OnAButtonPerformed;
        aButton.action.canceled -= OnAButtonCancelled;
        bButton.action.performed -= OnBButtonPerformed;
        bButton.action.canceled -= OnBButtonCancelled;
        joystick.action.performed -= OnJoystickPerformed;
        joystick.action.canceled -= OnBButtonCancelled;
    }

    private void Update()
    {
        RaycastHit res;
        if (rayInteractor.TryGetCurrent3DRaycastHit(out res))
        {
            if (res.transform.gameObject.layer == 10)
            {
                Vector3 groundPt = res.point; // the coordinate that the ray hits
                sphere.SetActive(true);
                sphere.transform.position = groundPt;
                Debug.Log(" name on the ground: " + res.transform.gameObject.layer);
                Debug.Log(" coordinates on the ground: " + groundPt);
            } else
            {
                sphere.SetActive(false);
            }

        }
    }

    void OnBButtonPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("button pressed on the ground");
        RaycastHit res;
        if (rayInteractor.TryGetCurrent3DRaycastHit(out res))
        {
            if (res.transform.gameObject.layer == 10)
            {
                Vector3 groundPt = res.point; // the coordinate that the ray hits
                targetObject.SetActive(true);
                targetObject.transform.position = groundPt + new Vector3(0f, 0.6f, 0f);
                Debug.Log(" name on the ground: " + res.transform.gameObject.layer);
                Debug.Log(" coordinates on the ground: " + groundPt);
            }

        }
    }

    void OnBButtonCancelled(InputAction.CallbackContext context)
    {
        // Code to execute when the jump action is performed
        Debug.Log("Jump!");
        // Add your jump logic here, like making the character jump
    }

    void OnAButtonPerformed(InputAction.CallbackContext context)
    {

        targetObject.transform.position = rightController.transform.position;

    }

    void OnAButtonCancelled(InputAction.CallbackContext context)
    {
        // Code to execute when the jump action is performed
        Debug.Log("Jump!");
        // Add your jump logic here, like making the character jump
    }

    void OnJoystickPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("joystick on the ground: " + context);
        Vector2 joystickValue = context.ReadValue<Vector2>();
        targetObject.transform.position += new Vector3(0f, joystickValue.y * 0.01f, 0f);
    }

    void OnJoystickCancelled(InputAction.CallbackContext context)
    {
        // Code to execute when the jump action is performed
        Debug.Log("Jump!");
        // Add your jump logic here, like making the character jump
    }
}
