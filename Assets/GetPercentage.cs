using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetPercentage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(string text)
    {
        GetComponent<TMP_Text>().text = text;
    }
}
