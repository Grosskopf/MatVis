using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class splitpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Mesh> meshlist = new List<Mesh>();
        List<List<Vector3>> verts = new List<List<Vector3>>();
        List<Vector3> centers = new List<Vector3>();
        List<List<Vector3>> norms = new List<List<Vector3>>();
        List<List<Vector2>> uvs = new List<List<Vector2>>();
        List<List<int>> tris = new List<List<int>>();
        for (int i = 0; i < 8; i++)
        {
            verts.Add(new List<Vector3>());
            centers.Add(new Vector3(0, 0, 0));
            norms.Add(new List<Vector3>());
            uvs.Add(new List<Vector2>());
            tris.Add(new List<int>());
        }
        for(int i=0; i < 33; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                double theta = 2.0f * 3.1415926f * (float)i / (float)32;
                double delta = (((float)j / 8.0f) - 1.0f) * 3.1415926f * 0.5f;
                double x = (Math.Cos(theta)) * Math.Cos(delta);
                double y = (Math.Sin(theta)) * Math.Cos(delta);
                double z = Math.Sin(delta);
                Vector3 actvec = new Vector3((float)x, (float)y, (float)z);
                //Debug.Log(actvec);
                //Debug.Log("is " + i + " / " + j);
                int meshnum = (i / 8 + (j / 8) * 4);
                int meshnumi = (((i - 1)) / 8 + (j / 8) * 4);
                int meshnumj = (i / 8 + (((j - 1)) / 8) * 4);
                int meshnumij = (((i - 1)) / 8 + (((j - 1)) / 8) * 4);
                if (i != 32&&j!=16)
                {
                    verts[meshnum].Add(actvec);
                    norms[meshnum].Add(actvec);
                    uvs[meshnum].Add(new Vector2(0, 0));
                    centers[meshnum] += actvec;
                }
                if (meshnumi != meshnum && i!=0 && j != 16)
                {
                    verts[meshnumi].Add(actvec);
                    norms[meshnumi].Add(actvec);
                    uvs[meshnumi].Add(new Vector2(0, 0));
                    centers[meshnumi] += actvec;
                }
                if (meshnumj != meshnum &&j!=0&&i!=32)
                {
                    verts[meshnumj].Add(actvec);
                    norms[meshnumj].Add(actvec);
                    uvs[meshnumj].Add(new Vector2(0, 0));
                    centers[meshnumj] += actvec;
                }
                if(meshnumi!=meshnumij && meshnumj != meshnumij && i != 0 && j!=0)
                {
                    verts[meshnumij].Add(actvec);
                    norms[meshnumij].Add(actvec);
                    uvs[meshnumij].Add(new Vector2(0, 0));
                    centers[meshnumij] += actvec;
                }
                //int vecnum = i % 8 + (j* 8;
            }
        }
        /*for(int i=0; i < verts[0].Count;i++)
        {
            Debug.Log(verts[0][i]);
            Debug.Log("is " + i);
        }*/
        for(int i =0; i < verts.Count; i++)
        {
            centers[i] = centers[i].normalized;
            for (int j=0; j < 8; j++)
            {
                for(int k = 0; k < 8; k++)
                {
                    tris[i].Add(k + j * 9);
                    tris[i].Add(k + (j + 1) * 9);
                    tris[i].Add((k + 1) + (j + 1) * 9);

                    tris[i].Add(k + j * 9);
                    tris[i].Add((k + 1) + (j + 1) * 9);
                    tris[i].Add((k + 1) + j * 9);
                    
                }
            }

            Mesh meshI = new Mesh();
            meshI.vertices = verts[i].ToArray();
            meshI.triangles = tris[i].ToArray();
            meshI.uv = uvs[i].ToArray();

            Vector3 dirtmp = centers[i] - meshI.bounds.center;
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
            Vector3 addcoords = 2 * centers[i] + closestcorner;
            verts[i].Add(addcoords);
            uvs[i].Add(new Vector2(0, 0));
            for (int j = 0; j < verts[i].Count; j++)
            {
                verts[i][j] -= centers[i];
            }
            meshI.vertices = verts[i].ToArray();
            meshI.uv = uvs[i].ToArray();
            GameObject gameObjI = new GameObject();
            gameObjI.transform.parent = transform;
            gameObjI.transform.localPosition = centers[i];
            gameObjI.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            gameObjI.transform.localRotation = new Quaternion();
            gameObjI.AddComponent<MeshRenderer>();
            gameObjI.AddComponent<MeshFilter>();
            gameObjI.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
            gameObjI.GetComponent<MeshFilter>().mesh = meshI;
        }
        transform.localScale =new Vector3(0.0125f,0.0125f,0.0125f);
        GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
