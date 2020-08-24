using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SetPunkt : EditCompUI
{
    public Vector3 Position;

    void Start()
    {

        Name.text = component.Name;
        Name.color = component.color;
        /*int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);*/
    }

    public void setposX(string x)
    {
        float tmppos_out;
        float.TryParse(x, out tmppos_out);
        Position.x = tmppos_out / 50;
        settransform();
    }
    public void setposY(string y)
    {
        float tmppos_out;
        float.TryParse(y, out tmppos_out);
        Position.z = tmppos_out / 50;
        settransform();
    }
    public void setposZ(string z)
    {
        float tmppos_out;
        float.TryParse(z, out tmppos_out);
        Position.y = tmppos_out / 50;
        settransform();
    }


    public void settransform()
    {
        Transform.localPosition = Position;
    }
}
