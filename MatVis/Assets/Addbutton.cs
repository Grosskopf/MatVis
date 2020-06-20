using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Addbutton : MonoBehaviour
{
    ComponentsHandler Handler;
    string Status;

    public Texture2D EbenenIcon;
    public Texture2D GeradenIcon;
    public Texture2D PunkteIcon;
    public void PressButton()
    {
        switch (Status)
        {
            case "Ebene":
                Handler.AddPlane();
                break;
            case "Gerade":
                Handler.AddLine();
                break;
            case "Punkt":
                Handler.AddPunkt();
                break;
        }
    }
    public void SetStatus(string _status)
    {
        Status = _status;
        RawImage rawImage= gameObject.GetComponent<RawImage>();
        switch (Status)
        {
            case "Ebene":
                rawImage.enabled = true;
                rawImage.texture = GeradenIcon;
                break;
            case "Gerade":
                rawImage.enabled = true;
                rawImage.texture = EbenenIcon;
                break;
            case "Punkt":
                rawImage.enabled = true;
                rawImage.texture = PunkteIcon;
                break;
            default:
                rawImage.enabled = false;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Handler = GetComponentInParent<ComponentsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
