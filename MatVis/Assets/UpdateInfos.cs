using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InfoMenuItem : MonoBehaviour
{
    public VirtualCompnonent compnonent;
    public MenuButton menu;
    public bool hidden = false;

    public GameObject hidebutton;
    public void hide()
    {
        hidden = true;
        if(compnonent is VisibleComponent)
        {
            ((VisibleComponent)compnonent).Model3D.SetActive(false);
        }else if (compnonent is TrackedEbene)
        {
            ((TrackedEbene)compnonent).Transform.GetComponent<AddMarkerplane>().plane.SetActive(false);
        }
        foreach (Transform obj in transform)
        {
            if (obj.gameObject != hidebutton)
            {
                obj.gameObject.SetActive(false);
            }
            else
            {
                obj.gameObject.SetActive(true);
            }
        }
        menu.Rearrange();
    }
    public void show()
    {
        hidden = false;
        if (compnonent is VisibleComponent)
        {
            ((VisibleComponent)compnonent).Model3D.SetActive(true);
        }
        else if (compnonent is TrackedEbene)
        {
            ((TrackedEbene)compnonent).Transform.GetComponent<AddMarkerplane>().plane.SetActive(true);
        }
        foreach (Transform obj in transform)
        {
            if (obj.gameObject != hidebutton)
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }
        menu.Rearrange();
    }
}
public class UpdateInfos : InfoMenuItem
{
    public Transform Layertransform;
    public TextMeshProUGUI Position;
    public List<TextMeshProUGUI> PositionenVecs;
    public TextMeshProUGUI NormalenVec;
    public TextMeshProUGUI UVec;
    public TextMeshProUGUI VVec;
    public TextMeshProUGUI Koordinatenform;
    public TextMeshProUGUI Normale;
    public TextMeshProUGUI Richtung;
    public TextMeshProUGUI Name;
    public List<Transform> variants;
    public List<GameObject> allwaysActiveElements;
    private int activevariant=0;

    // Start is called before the first frame update
    void Start()
    {
        /*GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scalefactor * 1200);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scalefactor * 280);
        GetComponent<RectTransform>().ForceUpdateRectTransforms();*/
        /*
        int actwidth = Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Position != null)
        {
            Position.text = /*"Position:\n*/ "X: " + Math.Round(Layertransform.localPosition.x * 50, 2).ToString() + " \n Y: " + Math.Round(Layertransform.localPosition.z * 50, 2).ToString() + " \n Z: " + Math.Round(Layertransform.localPosition.y * 50, 2).ToString();
        }
        foreach(TextMeshProUGUI postxt in PositionenVecs)
        {
            postxt.text= "" + Math.Round(Layertransform.localPosition.x * 50, 2).ToString() + "\n" + Math.Round(Layertransform.localPosition.z * 50, 2).ToString() + "\n" + Math.Round(Layertransform.localPosition.y * 50, 2).ToString();
        }
        if (Normale != null)//normale hat nur eine Ebene
        {
            Normale.text = "Normale:\n X: " + Math.Round (Layertransform.parent.InverseTransformDirection(Layertransform.up).x, 2).ToString() + " \n Y: " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).z, 2).ToString() + " \n Z: " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).y, 2).ToString();
        }
        else if (NormalenVec != null)
        {
            NormalenVec.text = "" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).x, 2).ToString() + "\n" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).z, 2).ToString() + "\n" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).y, 2).ToString();
            UVec.text = "" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.right).x, 2).ToString() + "\n" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.right).z, 2).ToString() + "\n" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.right).y, 2).ToString();
            VVec.text = "" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.forward).x, 2).ToString() + "\n" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.forward).z, 2).ToString() + "\n" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.forward).y, 2).ToString();
            Koordinatenform.text = "" + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).x, 2).ToString() + "x + " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).z, 2).ToString() + "y + " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.up).y, 2).ToString() + "z +"+ Math.Round(-Vector3.Dot(Layertransform.localPosition, Layertransform.parent.InverseTransformDirection(Layertransform.up)), 2).ToString() + " = 0";
        }
        else if (Richtung !=null)//das ist also für geraden
        {
            Richtung.text = "Richtung:\n X: " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.forward).x, 2).ToString() + " \n Y: " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.forward).z, 2).ToString() + " \n Z: " + Math.Round(Layertransform.parent.InverseTransformDirection(Layertransform.forward).y, 2).ToString();
            if (Layertransform.GetComponent<SnapSchnittkante>() != null && Layertransform.GetComponent<SnapSchnittkante>().isparallel)
            {
                Position.text = "Die Ebenen sind Parallel";
                Richtung.text = "";
            }
        }
    }
    public void nextform()
    {

        if (variants.Count > 1)
        {
            Debug.Log("swapping...");
            activevariant = (activevariant + 1) % (variants.Count);
            foreach (Transform vart in variants)
            {
                vart.gameObject.SetActive(false);
            }
            variants[activevariant].gameObject.SetActive(true);
        }
    }
    public void prevform()
    {

        if (variants.Count > 1)
        {
            Debug.Log("swapping...");
            activevariant = (activevariant - 1) % (variants.Count);
            if (activevariant < 0)
            {
                activevariant = variants.Count - 1;
            }
            foreach (Transform vart in variants)
            {
                vart.gameObject.SetActive(false);
            }
            variants[activevariant].gameObject.SetActive(true);
        }
    }
    public new void show()
    {
        if (variants.Count > 0)
        {
            variants[activevariant].gameObject.SetActive(true);
            foreach (GameObject go in allwaysActiveElements)
            {
                go.SetActive(true);
            }
            hidebutton.gameObject.SetActive(false);
            base.show();
        }
        else
        {
            base.show();
        }
    }
}
