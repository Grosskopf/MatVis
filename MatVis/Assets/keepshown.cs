using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepshown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInChildren<MeshRenderer>().enabled)
        {
            foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = true;
            }
        }
    }
}
