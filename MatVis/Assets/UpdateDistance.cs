using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateDistance : InfoMenuItem
{
    public TextMeshProUGUI Distanz;
    public VirtualCompnonent Element1;
    public VirtualCompnonent Element2;
    //private float olddist=0;
    // Start is called before the first frame update
    void Start()
    {
        /*
        int actwidth = Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);*/
    }

    // Update is called once per frame
    void Update()
    {
        float distance = -1;
        if (Element1 is Punkt && Element2 is Gerade)
        {
            Punkt punkt = (Punkt)Element1;
            Gerade gerade = (Gerade)Element2;
            Vector3 punktpos = new Vector3((float)Math.Round(punkt.GetTransform().localPosition.x * 50, 2), (float)Math.Round(punkt.GetTransform().localPosition.z * 50, 2), (float)Math.Round(punkt.GetTransform().localPosition.y * 50, 2));// punkt.GetTransform().position;
            Vector3 punktposg = new Vector3((float)Math.Round(gerade.GetTransform().localPosition.x * 50, 2), (float)Math.Round(gerade.GetTransform().localPosition.z * 50, 2), (float)Math.Round(gerade.GetTransform().localPosition.y * 50, 2));// punkt.GetTransform().position;
            Vector3 richtung = new Vector3((float)Math.Round(gerade.GetTransform().parent.InverseTransformDirection(gerade.GetTransform().forward).x, 2), (float)Math.Round(gerade.GetTransform().parent.InverseTransformDirection(gerade.GetTransform().forward).z, 2), (float)Math.Round(gerade.GetTransform().parent.InverseTransformDirection(gerade.GetTransform().forward).y, 2));
            float b = Vector3.Dot(richtung, punktpos);
            float a = (Vector3.Dot(richtung, punktposg) - b) / (-Vector3.Dot(richtung, richtung));
            distance = (punktpos - (punktposg + a * richtung)).magnitude;
        }
        if (Element1 is Punkt && Element2 is Ebene)
        {
            Punkt punkt = (Punkt)Element1;
            Ebene ebene = (Ebene)Element2;
            Vector3 punktpos = new Vector3((float)Math.Round(punkt.GetTransform().localPosition.x * 50, 2), (float)Math.Round(punkt.GetTransform().localPosition.z * 50, 2), (float)Math.Round(punkt.GetTransform().localPosition.y * 50, 2));// punkt.GetTransform().position;
            Vector4 koordinatenformvars = new Vector4((float)Math.Round(ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up).x, 2), (float)Math.Round(ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up).z, 2), (float)Math.Round(ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up).y, 2), (float)Math.Round(-Vector3.Dot(ebene.GetTransform().localPosition, ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up)), 2)*50);
            distance = Math.Abs( punktpos.x * koordinatenformvars.x + punktpos.y * koordinatenformvars.y + punktpos.z * koordinatenformvars.z + koordinatenformvars.w);

            
        }
        if (Element1 is Gerade && Element2 is Gerade)
        {
            Gerade gerade1 = (Gerade)Element1;
            Gerade gerade2 = (Gerade)Element2;
            Vector3 punktpos1 = new Vector3((float)Math.Round(gerade1.GetTransform().localPosition.x * 50, 2), (float)Math.Round(gerade1.GetTransform().localPosition.z * 50, 2), (float)Math.Round(gerade1.GetTransform().localPosition.y * 50, 2));// punkt.GetTransform().position;
            Vector3 punktpos2 = new Vector3((float)Math.Round(gerade2.GetTransform().localPosition.x * 50, 2), (float)Math.Round(gerade2.GetTransform().localPosition.z * 50, 2), (float)Math.Round(gerade2.GetTransform().localPosition.y * 50, 2));// punkt.GetTransform().position;
            Vector3 richtung1 = new Vector3((float)Math.Round(gerade1.GetTransform().parent.InverseTransformDirection(gerade1.GetTransform().forward).x, 2), (float)Math.Round(gerade1.GetTransform().parent.InverseTransformDirection(gerade1.GetTransform().forward).z, 2), (float)Math.Round(gerade1.GetTransform().parent.InverseTransformDirection(gerade1.GetTransform().forward).y, 2));
            Vector3 richtung2 = new Vector3((float)Math.Round(gerade2.GetTransform().parent.InverseTransformDirection(gerade2.GetTransform().forward).x, 2), (float)Math.Round(gerade2.GetTransform().parent.InverseTransformDirection(gerade2.GetTransform().forward).z, 2), (float)Math.Round(gerade2.GetTransform().parent.InverseTransformDirection(gerade2.GetTransform().forward).y, 2));
            Vector3 normale = Vector3.Cross(richtung1, richtung2).normalized;
            Vector4 koordinatenformvars = new Vector4(normale.x, normale.y, normale.z, Vector3.Dot(normale, punktpos2));
            distance = Math.Abs(punktpos1.x * koordinatenformvars.x + punktpos1.y * koordinatenformvars.y + punktpos1.z * koordinatenformvars.z + koordinatenformvars.w);

        }
        if (Element1 is Gerade && Element2 is Ebene)
        {
            Gerade gerade = (Gerade)Element1;
            Ebene ebene = (Ebene)Element2;
            Vector4 koordinatenformvars = new Vector4((float)Math.Round(ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up).x, 2), (float)Math.Round(ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up).z, 2), (float)Math.Round(ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up).y, 2), (float)Math.Round(-Vector3.Dot(ebene.GetTransform().localPosition, ebene.GetTransform().parent.InverseTransformDirection(ebene.GetTransform().up)), 2));
            Vector3 punktpos = new Vector3((float)Math.Round(gerade.GetTransform().localPosition.x * 50, 2), (float)Math.Round(gerade.GetTransform().localPosition.z * 50, 2), (float)Math.Round(gerade.GetTransform().localPosition.y * 50, 2));// punkt.GetTransform().position;
            Vector3 richtung = new Vector3((float)Math.Round(gerade.GetTransform().parent.InverseTransformDirection(gerade.GetTransform().forward).x, 2), (float)Math.Round(gerade.GetTransform().parent.InverseTransformDirection(gerade.GetTransform().forward).z, 2), (float)Math.Round(gerade.GetTransform().parent.InverseTransformDirection(gerade.GetTransform().forward).y, 2));
            if (koordinatenformvars.x * richtung.x + koordinatenformvars.y * richtung.y + koordinatenformvars.z * richtung.z == 0)
            {
                distance = Math.Abs(punktpos.x * koordinatenformvars.x + punktpos.y * koordinatenformvars.y + punktpos.z * koordinatenformvars.z + koordinatenformvars.w);
            }
            else
            {
                distance = 0;
            }
        }
        if (distance != -1)
        {
            Distanz.text = "Distanz:\n" + Math.Round(distance, 2).ToString() + "cm\n" + Element1.Name + "\n" + Element2.Name;
            //sDebug.Log("Distance of " + Math.Round(distance, 2).ToString());
        }
    }

}
