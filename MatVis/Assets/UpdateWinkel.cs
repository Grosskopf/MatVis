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

        int actwidth = Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        double winkel = Vector3.Angle(transform1.up, transform2.up);
        if (winkel > 90)
        {
            winkel = 180 - winkel;
        }
        Winkel.text = "Winkel:\n " + Math.Round(winkel, 2).ToString() + "° \n " + Transform1Description + " \n " + Transform2Description;
    }
}
