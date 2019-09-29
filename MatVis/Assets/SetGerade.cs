using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SetGerade : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Richtung;
    public Transform linetransform;

    public void setposX(string x)
    {
        Position.x = float.Parse(x, CultureInfo.InvariantCulture) / 50;
    }
    public void setposY(string y)
    {
        Position.z = float.Parse(y, CultureInfo.InvariantCulture) / 50;
    }
    public void setposZ(string z)
    {
        Position.y = float.Parse(z, CultureInfo.InvariantCulture) / 50;
    }
    public void setNormX(string x)
    {
        Richtung.x = float.Parse(x, CultureInfo.InvariantCulture);
    }
    public void setNormY(string y)
    {
        Richtung.z = float.Parse(y, CultureInfo.InvariantCulture);
    }
    public void setNormZ(string z)
    {
        Richtung.y = float.Parse(z, CultureInfo.InvariantCulture);
    }


    public void settransform()
    {
        linetransform.localPosition = Position;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 0, 1), Richtung);
        linetransform.localRotation = rotation;
    }
}
