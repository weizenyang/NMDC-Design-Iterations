using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    float targetYRotation = 0;
    float currentYRotation = 0;
    public bool hovered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentYRotation += (targetYRotation - currentYRotation) * 0.05f;
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
    public void OnScrollValueChange(Vector2 value)
    {
        if (hovered)
        {
            Debug.Log(value);
            targetYRotation = value.x * 360f;
        }
        
    }
}
