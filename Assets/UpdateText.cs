using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    public TMP_Text tmpro;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText(string text)
    {
        if (tmpro != null)
        {
            tmpro.text = text;
        }
    }
}
