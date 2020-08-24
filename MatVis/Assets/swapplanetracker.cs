using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapplanetracker : MonoBehaviour
{
    public Transform pivot;

    // Start is called before the first frame update
    public void maketrackerself()
    {
        pivot.SetParent(transform);
        pivot.localPosition = new Vector3(0, 0.12f,0);
        pivot.localRotation = new Quaternion(0, 0, 0, 0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
