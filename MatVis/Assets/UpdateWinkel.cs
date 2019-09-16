using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateWinkel : MonoBehaviour
{
    public Transform transform1;
    public Transform transform2;
    public TextMeshProUGUI Winkel;
    public string Transform1Description;
    public string Transform2Description;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        double winkel = Vector3.Angle(transform1.up, transform2.up);
        Winkel.text = "Winkel:\n " + Math.Round(winkel, 2).ToString() + "° \n " + Transform1Description + " \n " + Transform2Description;
    }
}
