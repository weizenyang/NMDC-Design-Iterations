using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetExplode : MonoBehaviour
{
    bool explode = false;
    float yPosOffset;
    Vector3[] initPositions;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            initPositions[i] = transform.GetChild(i).position;
         }
    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            for (int i=0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = initPositions[i] + new Vector3(0f, yPosOffset, 0f);
            };
        }
    }

    public void startExplode()
    {
        explode = true;
    }

    public void stopExplode()
    {
        explode = false;
    }
}
