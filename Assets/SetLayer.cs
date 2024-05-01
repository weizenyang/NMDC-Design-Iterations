using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : MonoBehaviour
{
    GameObject[] child;
    public GameObject layerPanel;
    // Start is called before the first frame update
    void Awake()
    {
        child = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            child[i] = transform.GetChild(i).gameObject;
            child[i].SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showLayer(int layer)
    {
        int layerIndex = Mathf.Clamp(layer, 0, child.Length - 1);
        foreach (GameObject child in child)
        {
            child.SetActive(false);
        }
        transform.GetChild(layerIndex).gameObject.SetActive(true);
        layerPanel.GetComponent<UpdateText>().updateText((layerIndex + 1).ToString());
    }

    public int getLayerCount()
    {

        return child.Length;
    }
}
