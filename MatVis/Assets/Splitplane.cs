using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Vuforia;

public class Splitplane : MonoBehaviour
{
    public VirtualCompnonent ebeneninfos;
    List<List<Vector3>> vertices_split = new List<List<Vector3>>();
    Vector3[] vertices = new Vector3[4] {
            new Vector3(-5.0f, 0, -5.0f),
            new Vector3(-5.0f, 0, 5.0f),
            new Vector3(5.0f, 0, 5.0f),
            new Vector3(5.0f, 0, -5.0f) };
    Matrix4x4 worldToLocal;
    // Start is called before the first frame update
    void Start()
    {
        worldToLocal = transform.worldToLocalMatrix;
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        
        mesh.vertices = vertices;
        int[] tris = new int[6]
        {
            // upper left triangle
            0, 1, 2,
            // lower right triangle
            0, 2, 3
        };

        mesh.triangles = tris;
        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };
        mesh.uv = uv;
        mf.mesh = mesh;
        
        /*if (ebeneninfos is TrackedEbene)
        {
            ((TrackedEbene)ebeneninfos).SomethingChanged.AddListener(SomethingChanged);
        } else if (ebeneninfos is Ebene)
        {
            ((Ebene)ebeneninfos).SomethingChanged.AddListener(SomethingChanged);
        }
        else
        {
            Debug.LogWarning("WTF is ebeneninfos?");
        }*/
    }
    public float Checkhit(Vector3 startborder, Vector3 endborder,Vector3 startpoint, Vector3 direction)
    {
        Vector3 directionborder = endborder - startborder;
        float result = -1.0f;
        float dividend = directionborder.z * direction.x - directionborder.x * direction.z;
        if (dividend!=0)
        {
            result = (startborder.x * direction.z + startpoint.z * direction.x - startborder.z * direction.x - startpoint.x * direction.z) / dividend;
        }
        if(result>1.0f || result <= 0.0f)
        {
            result = -1.0f;
        }/*
        else
        {
            Debug.Log("i think " + startpoint + " a * " + direction + " is between " + startborder + " and " + endborder);
        }*/
        return result;
    }
    public void SomethingChanged()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GetComponent<MeshRenderer>().enabled = false;
        vertices_split = new List<List<Vector3>> { vertices.ToList() };
        foreach(VirtualCompnonent virtc in ebeneninfos.GetChildren())
        //foreach (Schnittkante schnittkante in ebeneninfos.GetChildren())
        {
            
            if(virtc is Schnittkante)
            {
                Schnittkante schnittkante = (Schnittkante)virtc;
                Transform schnittrans = schnittkante.GetTransform();
                Vector3 posSchnittW = schnittrans.position;
                Vector3 posLayer = transform.InverseTransformPoint(posSchnittW);
                Vector3 fwSchnittW = schnittrans.forward;
                Vector3 fwSchnittLayer = transform.InverseTransformDirection(fwSchnittW);
                if (fwSchnittLayer == new Vector3(0, 0, 0))
                {
                    //Debug.Log("fwSchnittLayer was 0");
                    fwSchnittLayer = worldToLocal.MultiplyPoint3x4(fwSchnittW);
                    //Debug.Log("WorldToLocal: " + worldToLocal);
                }
                /*
                Debug.Log("Ebene: "+ebeneninfos.Name);
                Debug.Log("Before");
                Debug.Log("positionschnittkante: " + posSchnittW);
                Debug.Log("forwardschnittkante: " + fwSchnittW);
                Debug.Log("After");
                Debug.Log("positionschnittkante: " + posLayer);
                Debug.Log("forwardschnittkante: " + fwSchnittLayer);*/
                int i = 0;
                while (i < vertices_split.Count)
                {
                    float hitA = -1.0f;
                    int hitBorderA = -1;
                    float hitB = -1.0f;
                    int hitBorderB = -1;
                    for (int j = 0; j < vertices_split[i].Count; j++)
                    {
                        //Debug.Log("had vertex at " + vertices_split[i][j]);
                        float acthit = Checkhit(vertices_split[i][j], vertices_split[i][(j + 1) % vertices_split[i].Count], posLayer, fwSchnittLayer);
                        if (hitBorderA == -1 && acthit != -1.0f)
                        {
                            hitA = acthit;
                            hitBorderA = j;
                        }
                        else if (acthit != -1.0f)
                        {
                            hitB = acthit;
                            hitBorderB = j;
                        }
                    }
                    if (hitBorderB != -1)
                    {
                        List<Vector3> Part1Vectors = new List<Vector3>();
                        List<Vector3> Part2Vectors = new List<Vector3>();
                        //Debug.Log("had hit " + hitA + " at " + hitBorderA);
                        //Debug.Log("had hit " + hitB + " at " + hitBorderB);
                        bool tooclose = false;
                        for (int j = 0; j <= hitBorderA; j++)
                        {
                            tooclose = false;

                            foreach (Vector3 v in Part1Vectors)
                            {
                                if (Vector3.Distance(v, vertices_split[i][j]) < 0.01)
                                {
                                    tooclose = true;
                                }
                            }
                            if (!tooclose)
                            {
                                Part1Vectors.Add(vertices_split[i][j]);
                            }
                        }
                        tooclose = false;
                        Vector3 possiblevector = vertices_split[i][hitBorderA] + hitA * (vertices_split[i][(hitBorderA + 1) % vertices_split[i].Count] - vertices_split[i][hitBorderA]);
                        foreach (Vector3 v in Part1Vectors)
                        {
                            if (Vector3.Distance(v, possiblevector) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part1Vectors.Add(possiblevector);
                        }
                        tooclose = false;
                        possiblevector = vertices_split[i][hitBorderA] + hitA * (vertices_split[i][(hitBorderA + 1) % vertices_split[i].Count] - vertices_split[i][hitBorderA]);
                        foreach (Vector3 v in Part2Vectors)
                        {
                            if (Vector3.Distance(v, possiblevector) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part2Vectors.Add(possiblevector);
                        }
                        for (int j = hitBorderA + 1; j <= hitBorderB; j++)
                        {

                            tooclose = false;
                            foreach (Vector3 v in Part2Vectors)
                            {
                                if (Vector3.Distance(v, vertices_split[i][j]) < 0.01)
                                {
                                    tooclose = true;
                                }
                            }
                            if (!tooclose)
                            {
                                Part2Vectors.Add(vertices_split[i][j]);
                            }
                            //Part2Vectors.Add(vertices_split[i][j]);
                        }
                        tooclose = false;
                        possiblevector = vertices_split[i][hitBorderB] + hitB * (vertices_split[i][(hitBorderB + 1) % vertices_split[i].Count] - vertices_split[i][hitBorderB]);
                        foreach (Vector3 v in Part1Vectors)
                        {
                            if (Vector3.Distance(v, possiblevector) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part1Vectors.Add(possiblevector);
                        }
                        tooclose = false;
                        foreach (Vector3 v in Part2Vectors)
                        {
                            if (Vector3.Distance(v, possiblevector) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part2Vectors.Add(possiblevector);
                        }
                        //Part2Vectors.Add();
                        for (int j = hitBorderB + 1; j < vertices_split[i].Count; j++)
                        {
                            tooclose = false;

                            foreach (Vector3 v in Part1Vectors)
                            {
                                if (Vector3.Distance(v, vertices_split[i][j]) < 0.01)
                                {
                                    tooclose = true;
                                }
                            }
                            if (!tooclose)
                            {
                                Part1Vectors.Add(vertices_split[i][j]);
                            }
                        }
                        vertices_split[i] = Part1Vectors;
                        /*for(int j=0; j < Part1Vectors.Count; j++)
                        {
                            Debug.Log("Part1Vector " + Part1Vectors[j]);
                        }*/
                        vertices_split.Insert(i + 1, Part2Vectors);
                        /*for (int j = 0; j < Part1Vectors.Count; j++)
                        {
                            Debug.Log("Part2Vector " + Part1Vectors[j]);
                        }*/
                    }
                    i++;
                    i++;
                }
            }


        }

        for (int angle = 0; angle < 3; angle++)
        {

            Vector3 upL2 = transform.parent.parent.InverseTransformDirection(transform.up);
            //Debug.Log("upL2: " + upL2);

            float dL2 = Vector3.Dot(transform.localPosition, upL2);
            //Debug.Log("dL2: " + dL2);
            Vector3 rL1 = new Vector3(0, 0, 0);
            Vector3 fL1 = new Vector3(0, 0, 0);
            Vector3 uL1 = new Vector3(0, 0, 0);
            if (angle == 0)
            {

                rL1 = transform.parent.parent.InverseTransformDirection(new Vector3(0, 1, 0));
                //Debug.Log("rL1: " + rL1);
                uL1 = transform.parent.parent.InverseTransformDirection(new Vector3(1, 0, 0));
                //Debug.Log("uL1: " + uL1);
                fL1 = transform.parent.parent.InverseTransformDirection(new Vector3(0, 0, 1));
                //Debug.Log("fL1: " + fL1);
            }
            else if (angle == 1)
            {

                uL1 = transform.parent.parent.InverseTransformDirection(new Vector3(0, 1, 0));
                //Debug.Log("uL1: " + uL1);
                rL1 = transform.parent.parent.InverseTransformDirection(new Vector3(1, 0, 0));
                //Debug.Log("rL1: " + rL1);
                fL1 = transform.parent.parent.InverseTransformDirection(new Vector3(0, 0, 1));
                //Debug.Log("fL1: " + fL1);
            }
            else if (angle == 2)
            {

                uL1 = transform.parent.parent.InverseTransformDirection(new Vector3(0, 0, 1));
                //Debug.Log("uL1: " + uL1);
                rL1 = transform.parent.parent.InverseTransformDirection(new Vector3(1, 0, 0));
                //Debug.Log("rL1: " + rL1);
                fL1 = transform.parent.parent.InverseTransformDirection(new Vector3(0, 1, 0));
                //Debug.Log("fL1: " + fL1);
            }

            Vector3 posL1 = transform.parent.parent.localPosition;
            //Debug.Log("posL1: " + posL1);
            bool isparallel = Vector3.Dot(uL1, upL2) > 0.995;
            GameObject virtualcut = new GameObject();
            Transform virtct = virtualcut.transform;

            if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
            {
                Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                //Debug.Log("first case");
                //Debug.Log("outPos: " + outPos);
                //Debug.Log("outDir: " + outDir);
                virtct.localPosition = outPos;
                virtct.LookAt(transform.parent.parent.position + transform.parent.parent.TransformDirection(outDir));
                //transform.up = outDir;

            }
            else
            {
                rL1 = (transform.parent.parent.InverseTransformDirection(transform.right) + transform.parent.parent.InverseTransformDirection(transform.forward)).normalized;

                if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
                {
                    //Debug.Log("second case");
                    Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                    Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                    virtct.localPosition = outPos;
                    virtct.LookAt(transform.parent.parent.position + transform.parent.parent.TransformDirection(outDir));
                    //transform.up = outDir;

                }
                else
                {

                    fL1 = (transform.parent.parent.InverseTransformDirection(transform.right) - transform.parent.parent.InverseTransformDirection(transform.forward)).normalized;
                    if (Vector3.Dot(upL2, rL1) != 0 && Vector3.Dot(upL2, fL1) != 0)
                    {
                        //Debug.Log("third case");
                        Vector3 outPos = posL1 + (dL2 - Vector3.Dot(upL2, posL1)) / Vector3.Dot(upL2, fL1) * fL1;
                        Vector3 outDir = (rL1 - (Vector3.Dot(upL2, rL1) / Vector3.Dot(upL2, fL1)) * fL1).normalized;
                        virtct.localPosition = outPos;
                        virtct.LookAt(transform.parent.parent.position + transform.parent.parent.TransformDirection(outDir));
                        //transform.up = outDir;

                    }
                }
            }

            Vector3 newPos = virtct.localPosition - transform.parent.parent.InverseTransformDirection(transform.forward) * (float)(Vector3.Dot(transform.parent.parent.InverseTransformDirection(transform.forward), virtct.localPosition));
            virtct.localPosition = newPos;
            int i = 0;
            if (!isparallel) { 
            while (i < vertices_split.Count)
            {
                float hitA = -1.0f;
                int hitBorderA = -1;
                float hitB = -1.0f;
                int hitBorderB = -1;
                for (int j = 0; j < vertices_split[i].Count; j++)
                {
                    //Debug.Log("had vertex at " + vertices_split[i][j]);
                    float acthit = Checkhit(vertices_split[i][j], vertices_split[i][(j + 1) % vertices_split[i].Count], virtct.localPosition, virtct.forward);
                    if (hitBorderA == -1 && acthit != -1.0f)
                    {
                        hitA = acthit;
                        hitBorderA = j;
                    }
                    else if (acthit != -1.0f)
                    {
                        hitB = acthit;
                        hitBorderB = j;
                    }
                }
                if (hitBorderB != -1)
                {
                    List<Vector3> Part1Vectors = new List<Vector3>();
                    List<Vector3> Part2Vectors = new List<Vector3>();
                    //Debug.Log("had hit " + hitA + " at " + hitBorderA);
                    //Debug.Log("had hit " + hitB + " at " + hitBorderB);
                    bool tooclose = false;
                    for (int j = 0; j <= hitBorderA; j++)
                    {
                        tooclose = false;

                        foreach (Vector3 v in Part1Vectors)
                        {
                            if (Vector3.Distance(v, vertices_split[i][j]) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part1Vectors.Add(vertices_split[i][j]);
                        }
                    }
                    tooclose = false;
                    Vector3 possiblevector = vertices_split[i][hitBorderA] + hitA * (vertices_split[i][(hitBorderA + 1) % vertices_split[i].Count] - vertices_split[i][hitBorderA]);
                    foreach (Vector3 v in Part1Vectors)
                    {
                        if (Vector3.Distance(v, possiblevector) < 0.01)
                        {
                            tooclose = true;
                        }
                    }
                    if (!tooclose)
                    {
                        Part1Vectors.Add(possiblevector);
                    }
                    tooclose = false;
                    possiblevector = vertices_split[i][hitBorderA] + hitA * (vertices_split[i][(hitBorderA + 1) % vertices_split[i].Count] - vertices_split[i][hitBorderA]);
                    foreach (Vector3 v in Part2Vectors)
                    {
                        if (Vector3.Distance(v, possiblevector) < 0.01)
                        {
                            tooclose = true;
                        }
                    }
                    if (!tooclose)
                    {
                        Part2Vectors.Add(possiblevector);
                    }
                    for (int j = hitBorderA + 1; j <= hitBorderB; j++)
                    {

                        tooclose = false;
                        foreach (Vector3 v in Part2Vectors)
                        {
                            if (Vector3.Distance(v, vertices_split[i][j]) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part2Vectors.Add(vertices_split[i][j]);
                        }
                        //Part2Vectors.Add(vertices_split[i][j]);
                    }
                    tooclose = false;
                    possiblevector = vertices_split[i][hitBorderB] + hitB * (vertices_split[i][(hitBorderB + 1) % vertices_split[i].Count] - vertices_split[i][hitBorderB]);
                    foreach (Vector3 v in Part1Vectors)
                    {
                        if (Vector3.Distance(v, possiblevector) < 0.01)
                        {
                            tooclose = true;
                        }
                    }
                    if (!tooclose)
                    {
                        Part1Vectors.Add(possiblevector);
                    }
                    tooclose = false;
                    foreach (Vector3 v in Part2Vectors)
                    {
                        if (Vector3.Distance(v, possiblevector) < 0.01)
                        {
                            tooclose = true;
                        }
                    }
                    if (!tooclose)
                    {
                        Part2Vectors.Add(possiblevector);
                    }
                    //Part2Vectors.Add();
                    for (int j = hitBorderB + 1; j < vertices_split[i].Count; j++)
                    {
                        tooclose = false;

                        foreach (Vector3 v in Part1Vectors)
                        {
                            if (Vector3.Distance(v, vertices_split[i][j]) < 0.01)
                            {
                                tooclose = true;
                            }
                        }
                        if (!tooclose)
                        {
                            Part1Vectors.Add(vertices_split[i][j]);
                        }
                    }
                    vertices_split[i] = Part1Vectors;
                    /*for(int j=0; j < Part1Vectors.Count; j++)
                    {
                        Debug.Log("Part1Vector " + Part1Vectors[j]);
                    }*/
                    vertices_split.Insert(i + 1, Part2Vectors);
                    /*for (int j = 0; j < Part1Vectors.Count; j++)
                    {
                        Debug.Log("Part2Vector " + Part1Vectors[j]);
                    }*/
                }
                i++;
                i++;
            }
            }
        }
        for (int i=0; i < vertices_split.Count; i++)
        {
            Mesh meshI = new Mesh();
            List<int> trisI = new List<int>();
            List<Vector2> uvI = new List<Vector2>();
            Vector3 center = new Vector3();
            for (int j = 0; j < vertices_split[i].Count; j++)
            {
                uvI.Add(new Vector2(vertices_split[i][j].x * 0.1f + 0.5f, vertices_split[i][j].z * 0.1f + 0.5f));
                if (j < vertices_split[i].Count - 2)
                {
                    trisI.Add(0);
                    trisI.Add(j + 1);
                    trisI.Add(j + 2);
                }
                center += vertices_split[i][j];
            }
            center = center.normalized * 10;
            meshI.vertices = vertices_split[i].ToArray();
            meshI.triangles = trisI.ToArray();
            meshI.uv = uvI.ToArray();
            Vector3 dirtmp = center-meshI.bounds.center;
            Vector3 closestcorner = meshI.bounds.extents;
            if (dirtmp.x < 0)
            {
                closestcorner.x *= -1;
            }
            if (dirtmp.y < 0)
            {
                closestcorner.y *= -1;
            }
            if (dirtmp.z < 0)
            {
                closestcorner.z *= -1;
            }
            Vector3 addcoords = 2*center+closestcorner;
            vertices_split[i].Add(addcoords);
            uvI.Add(new Vector2(0, 0));
            for (int j = 0; j < vertices_split[i].Count; j++)
            {
                vertices_split[i][j] -= center;
            }
            meshI.vertices = vertices_split[i].ToArray();
            meshI.uv = uvI.ToArray();
            GameObject gameObjI = new GameObject();
            gameObjI.transform.parent = transform;
            gameObjI.transform.localPosition = center;
            gameObjI.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            gameObjI.transform.localRotation= new Quaternion();
            gameObjI.AddComponent<MeshRenderer>();
            gameObjI.AddComponent<MeshFilter>();
            gameObjI.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
            gameObjI.GetComponent<MeshFilter>().mesh = meshI;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
