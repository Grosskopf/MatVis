using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Findschnittpunkt : MonoBehaviour
{
    public Transform Ebene;
    public Transform Gerade;

    // Update is called once per frame
    void Update()
    {
        double d = Vector3.Dot(Ebene.parent.InverseTransformDirection(Ebene.up), Ebene.position);//d aus ebenengleichung
        Vector3 n = Ebene.parent.InverseTransformDirection(Ebene.up);//normale aus ebenengleichung

        Vector3 pos = Gerade.position;//position der geraden
        Vector3 dir = Gerade.parent.InverseTransformDirection(Gerade.forward);//richtung der geraden
        //(pos.x + a * dir.x)=x1
        //(pos.y + a * dir.y)=x2
        //(pos.z + a * dir.z)=x3
        //x1 * n.x + x2 * n.y + x3 * n.z = d
        //(pos.x + a * dir.x) * n.x + (pos.y + a * dir.y) * n.y + (pos.z + a * dir.z) * n.z = d
        //n.x * pos.x + n.y * pos.y + n.z * pos.z - d = - a (dir.x * n.x + dir.y * n.y + dir.z * n.z)
        //(dot( n, pos ) - d) / dot( n, dir ) = - a
        //if dot( n, dir ) == 0 Line is parallel to plane (has no or infinite points)
        //if dot( n, pos ) == d Line starts in plane
        if(Vector3.Dot(n,dir) != 0)
        {
            Vector3 PointPos= pos - dir * (float) ((Vector3.Dot(n, pos) - d) / Vector3.Dot(n, dir));
            transform.position = PointPos;
            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponentInChildren<SphereCollider>().enabled = true;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponentInChildren<SphereCollider>().enabled = false;
        }
    }
}
