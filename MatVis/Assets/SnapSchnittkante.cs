using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSchnittkante : MonoBehaviour
{
    public Transform layer1;
    public Transform layer2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 upL2 = layer2.up;

        float dL2 = Vector3.Dot(layer2.position,upL2);

        Vector3 rL1 = layer1.right;
        Vector3 fL1 = layer1.forward;

        Vector3 posL1 = layer1.position;


        if( Vector3.Dot(upL2,rL1)!=0 && Vector3.Dot(upL2,fL1) != 0)
        {
            Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
            Vector3 outDir = rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1;
            transform.position = outPos;
            transform.LookAt(outPos + outDir);
            //transform.up = outDir;

        }
    }
}
