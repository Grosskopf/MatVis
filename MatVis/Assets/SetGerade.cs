using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SetGerade : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Richtung;
    public Transform linetransform;
    public TextMeshProUGUI yPlaceholder;

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
    public void setNormX(string x)
    {
        Richtung.x = float.Parse(x, CultureInfo.InvariantCulture);
        yPlaceholder.text = "0";
        settransform();
    }
    public void setNormY(string y)
    {
        Richtung.z = float.Parse(y, CultureInfo.InvariantCulture);
        settransform();
    }
    public void setNormZ(string z)
    {
        Richtung.y = float.Parse(z, CultureInfo.InvariantCulture);
        yPlaceholder.text = "0";
        settransform();
    }


    public void settransform()
    {
        linetransform.localPosition = Position;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 0, 1), Richtung);
        linetransform.localRotation = rotation;
    }
}
