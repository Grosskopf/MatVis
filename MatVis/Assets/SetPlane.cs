using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SetPlane : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Normale;
    public Transform planetransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        Normale.x = float.Parse(x, CultureInfo.InvariantCulture);
    }
    public void setNormY(string y)
    {
        Normale.z = float.Parse(y, CultureInfo.InvariantCulture);
    }
    public void setNormZ(string z)
    {
        Normale.y = float.Parse(z, CultureInfo.InvariantCulture);
    }


    public void settransform()
    {
        planetransform.localPosition = Position;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normale);
        planetransform.localRotation = rotation;
    }
}
