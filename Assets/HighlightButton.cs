using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButton : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        targetObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void Highlight()
    {
        targetObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(255, 255, 0, 0);
    }
}
