using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateInfos : MonoBehaviour
{
    public Transform Layertransform;
    public TextMeshProUGUI Position;
    public TextMeshProUGUI Normale;
    public TextMeshProUGUI Richtung;
    public TextMeshProUGUI Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Position.text = "Position:\n X: " + Math.Round(Layertransform.localPosition.x * 50, 2).ToString() + " \n Y: " + Math.Round(Layertransform.localPosition.z*50,2).ToString() + " \n Z: " + Math.Round(Layertransform.localPosition.y * 50, 2).ToString();
        if (Normale != null)
        {
            Normale.text = "Normale:\n X: " + Math.Round(Layertransform.up.x, 2).ToString() + " \n Y: " + Math.Round(Layertransform.up.z, 2).ToString() + " \n Z: " + Math.Round(Layertransform.up.y, 2).ToString();
        }
        else
        {
            Richtung.text = "Richtung:\n X: " + Math.Round(Layertransform.forward.x, 2).ToString() + " \n Y: " + Math.Round(Layertransform.forward.z, 2).ToString() + " \n Z: " + Math.Round(Layertransform.forward.y, 2).ToString();
        }
    }
}
