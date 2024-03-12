using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    float targetYRotation = 0;
    float currentYRotation = 0;
    public bool hovered = false;
    public float[] rotation = new float[30];
    public float currentRotation = 0;
    [SerializeField] private GameObject targetPlinth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnScrollValueChange(Vector2 value)
    {
            Debug.Log(value);
            targetYRotation = value.x * 360f;
            currentYRotation += (targetYRotation - currentYRotation) * 0.05f;
            transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
            targetPlinth.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);

        if (rotation.Length >= 30 )
        {
            for(int i = 0; i < rotation.Length - 2; i++)
            {
                rotation[i] = rotation[i + 1];
            }
            rotation[rotation.Length - 1] = value.x;
        } else
        {
            for (int i = 0; i < rotation.Length; i++)
            {
                rotation[rotation.Length] = value.x;
            }
            
        }

        currentRotation = value.x;
    }

    public void externalSetRotation(float rotation)
    {
        //transform.rotation = Quaternion.Euler(0, rotation, 0);
        //targetPlinth.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void externalAddRotation(float rotation)
    {
        transform.localEulerAngles -= new Vector3(0, rotation, 0);
        targetPlinth.transform.localEulerAngles -= new Vector3(0, rotation, 0);
    }
}
