using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSchnittkante : MonoBehaviour
{
    public Transform layer1;
    public Transform layer2;
    public bool isparallel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 upL2 = layer2.up;

        float dL2 = Vector3.Dot(layer2.localPosition,upL2);

        Vector3 rL1 = layer1.right;
        Vector3 fL1 = layer1.forward;

        Vector3 posL1 = layer1.localPosition;
        isparallel = Vector3.Dot(layer1.up, layer2.up) > 0.995;

        if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
        {
            Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
            Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
            transform.localPosition = outPos;
            transform.LookAt(outPos + outDir);
            //transform.up = outDir;

            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<SphereCollider>().enabled = true;
        }
        else { 
            rL1 = (layer1.right + layer1.forward).normalized;
            
            if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
            {
                Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                transform.localPosition = outPos;
                transform.LookAt(transform.position + outDir);
                //transform.up = outDir;

                GetComponent<MeshRenderer>().enabled = true;
                GetComponent<SphereCollider>().enabled = true;
            }
            else
            {

                fL1 = (layer1.right - layer1.forward).normalized;
                if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
                {
                    Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                    Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                    transform.localPosition = outPos;
                    transform.LookAt(transform.position + outDir);
                    //transform.up = outDir;
                    GetComponent<MeshRenderer>().enabled = true;
                    GetComponent<SphereCollider>().enabled = true;

                }
                else
                {
                        GetComponent<MeshRenderer>().enabled = false;
                        GetComponent<SphereCollider>().enabled = false;
                }
            }
        }

        Vector3 newPos = transform.localPosition - transform.forward * (float)(Vector3.Dot(transform.forward, transform.localPosition));
        transform.localPosition = newPos;
    }
}
