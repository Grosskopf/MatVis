using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleto4kEsque : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        float scale = GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>().rect.width / 1440;
        GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
