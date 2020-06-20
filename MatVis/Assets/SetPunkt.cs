using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SetPunkt : MonoBehaviour
{
    public Vector3 Position;
    public Transform punkttransform;

    void Start()
    {

        int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);
    }

    public void setposX(string x)
    {
        Position.x = float.Parse(x, CultureInfo.InvariantCulture) / 50;
        settransform();
    }
    public void setposY(string y)
    {
        Position.z = float.Parse(y, CultureInfo.InvariantCulture) / 50;
        settransform();
    }
    public void setposZ(string z)
    {
        Position.y = float.Parse(z, CultureInfo.InvariantCulture) / 50;
        settransform();
    }


    public void settransform()
    {
        punkttransform.localPosition = Position;
    }
}
