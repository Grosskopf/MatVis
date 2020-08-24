using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSchnittkante : MonoBehaviour
{
    public Transform layer1;
    public Transform layer2;
    public bool isparallel;
    Vector3 upL2;
    float dL2;
    Vector3 rL1;
    Vector3 fL1;
    Vector3 posL1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void updateposrot()
    {

        upL2 = layer2.parent.InverseTransformDirection(layer2.up);
        //Debug.Log("upL2: "+upL2);

        dL2 = Vector3.Dot(layer2.localPosition, upL2);
        //Debug.Log("dL2: " + dL2);

        rL1 = layer1.parent.InverseTransformDirection(layer1.right);
        //Debug.Log("rL1: " + rL1);
        fL1 = layer1.parent.InverseTransformDirection(layer1.forward);
        //Debug.Log("fL1: " + fL1);

        posL1 = layer1.localPosition;
        //Debug.Log("posL1: " + posL1);
        isparallel = Vector3.Dot(layer1.parent.InverseTransformDirection(layer1.up), layer2.parent.InverseTransformDirection(layer2.up)) > 0.995;

        if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
        {
            Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
            Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
            //Debug.Log("first case");
            //Debug.Log("outPos: " + outPos);
            //Debug.Log("outDir: " + outDir);
            transform.localPosition = outPos;
            transform.LookAt(transform.parent.position+transform.parent.TransformDirection(outDir));
            //transform.up = outDir;

            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
        else
        {
            rL1 = (layer1.parent.InverseTransformDirection(layer1.right) + layer1.parent.InverseTransformDirection(layer1.forward)).normalized;

            if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
            {
                //Debug.Log("second case");
                Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                transform.localPosition = outPos;
                transform.LookAt(transform.parent.position + transform.parent.TransformDirection(outDir));
                //transform.up = outDir;

                GetComponentInChildren<MeshRenderer>().enabled = true;
                GetComponentInChildren<CapsuleCollider>().enabled = true;
            }
            else
            {

                fL1 = (layer1.parent.InverseTransformDirection(layer1.right) - layer1.parent.InverseTransformDirection(layer1.forward)).normalized;
                if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
                {
                    //Debug.Log("third case");
                    Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                    Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                    transform.localPosition = outPos;
                    transform.LookAt(transform.parent.position + transform.parent.TransformDirection(outDir));
                    //transform.up = outDir;
                    GetComponentInChildren<MeshRenderer>().enabled = true;
                    GetComponentInChildren<CapsuleCollider>().enabled = true;

                }
                else
                {
                    GetComponentInChildren<MeshRenderer>().enabled = false;
                    GetComponentInChildren<CapsuleCollider>().enabled = false;
                }
            }
        }

        Vector3 newPos = transform.localPosition - transform.parent.InverseTransformDirection(transform.forward) * (float)(Vector3.Dot(transform.parent.InverseTransformDirection(transform.forward), transform.localPosition));
        transform.localPosition = newPos;
    }
    // Update is called once per frame
    void Update()
    {
        if(upL2!=layer2.parent.InverseTransformDirection(layer2.up) || dL2!= Vector3.Dot(layer2.localPosition, layer2.parent.InverseTransformDirection(layer2.up)) ||!(rL1==layer1.parent.InverseTransformDirection(layer1.right) || rL1==(layer1.parent.InverseTransformDirection(layer1.right) + layer1.parent.InverseTransformDirection(layer1.forward)).normalized) ||!(fL1==layer1.parent.InverseTransformDirection(layer1.forward) || fL1 == (layer1.parent.InverseTransformDirection(layer1.right) - layer1.parent.InverseTransformDirection(layer1.forward)).normalized) || posL1!= layer1.localPosition)
        {
            updateposrot();
            /*if (upL2 != layer2.parent.InverseTransformDirection(layer2.up))
            {
                Debug.Log("case1");
            }
            if (dL2 != Vector3.Dot(layer2.localPosition, layer2.parent.InverseTransformDirection(layer2.up)))
            {
                Debug.Log("case2");
            }
            if (rL1 != layer1.parent.InverseTransformDirection(layer1.right))
            {
                Debug.Log("case3");
            }
            if (fL1 != layer1.parent.InverseTransformDirection(layer1.forward))
            {
                Debug.Log("case4");
            }
            if (posL1 != layer1.localPosition)
            {
                Debug.Log("case5");
            }
            Debug.Log("updated");*/
            GetComponentInParent<CompHandlerLink>().handler.UpdateAllLayers();
        }
    }
}
