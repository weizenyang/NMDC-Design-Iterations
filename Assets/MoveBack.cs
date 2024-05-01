using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBy : MonoBehaviour
{
    public GameObject[] targetObjects;
    public Vector3 position;
    public Vector3 initPosition;
    public List<Image> images;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;

        if (images.Count <= 0)
        {
            foreach (GameObject go in targetObjects)
            {
                foreach (Image obj in go.GetComponentsInChildren<Image>())
                {
                    images.Add(obj);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {

        GameObject[] hands = GameObject.FindGameObjectsWithTag("Hands");
        foreach (GameObject go in hands)
        {
            transform.localPosition = initPosition + position;

            foreach (Image image in images)
            {
                image.color = new Color(255, 255, 255, 0);
            }
        }



    }

    public void RevertPosition()
    {
        transform.localPosition = initPosition;
        foreach (Image image in images)
        {
            image.color = new Color(255, 255, 255, 255);
        }

        Debug.Log("Revert Position");
    }
}
