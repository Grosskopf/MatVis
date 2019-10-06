using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SetPlane : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Normale;
    public Transform planetransform;
    public TextMeshProUGUI zPlaceholder;
    // Start is called before the first frame update
    void Start()
    {

        int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Normale.x = float.Parse(x, CultureInfo.InvariantCulture);
        zPlaceholder.text = "0";
        settransform();
    }
    public void setNormY(string y)
    {
        Normale.z = float.Parse(y, CultureInfo.InvariantCulture);
        zPlaceholder.text = "0";
        settransform();
    }
    public void setNormZ(string z)
    {
        Normale.y = float.Parse(z, CultureInfo.InvariantCulture);
        settransform();
    }


    public void settransform()
    {
        planetransform.localPosition = Position;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normale);
        planetransform.localRotation = rotation;
    }
}
