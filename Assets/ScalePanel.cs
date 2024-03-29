using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalePanel : MonoBehaviour
{

    [SerializeField] GameObject UIObject;
    Vector3 initPos;

    
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.localPosition;
        ScaleDownRemainPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleUp()
    {
        GameObject[] hands = GameObject.FindGameObjectsWithTag("Hands");
        foreach (GameObject go in hands)
        {
            if (go.activeSelf)
            {
                Vector3 position = transform.localPosition;
                transform.localPosition = new Vector3(position.x, position.y, 0.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            }

        }
        
    }

    public void ScaleDown()
    {
        Vector3 position = transform.localPosition;
        transform.localPosition = new Vector3(position.x, position.y, 0.5f);
        transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);


    }

    public void ScaleUpRemainPos()
    {
        GameObject[] hands = GameObject.FindGameObjectsWithTag("Hands");
        foreach (GameObject go in hands)
        {
            transform.localPosition = new Vector3(initPos.x, initPos.y, initPos.z);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }


    }

    public void ScaleDownRemainPos()
    {

        transform.localPosition = new Vector3(initPos.x, initPos.y, initPos.z + 0.3f);
        transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);



    }

}
