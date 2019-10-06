using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AddMarkerplane : MonoBehaviour
{
    // Start is called before the first frame update

    public bool istracked;
    public Menu InfoMenu;
    private GameObject InfoField;
    public GameObject InfoFieldPrefab;
    public ComponentsHandler ac;
    public GameObject plane;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(
        (GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
        GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)&&
        !istracked
        )
        {
            istracked = true;

            if (InfoField == null || InfoField.transform.parent != InfoMenu.transform)
            {
                InfoField = GameObject.Instantiate(InfoFieldPrefab);
                InfoField.transform.SetParent(InfoMenu.transform);
                InfoField.GetComponent<UpdateInfos>().Layertransform = transform;
                InfoField.GetComponent<UpdateInfos>().Name.SetText(transform.name);
                InfoMenu.MenuItems.Add(InfoField);
                InfoMenu.rearrange();
                if (!InfoMenu.isopen)
                {
                    InfoField.SetActive(false);
                }
                ac.AddPlaneTracked(transform);
            }
            plane.SetActive(true);
        }
        else if(istracked &&
        !(GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
        GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        )
        {
            istracked = false;
            InfoMenu.MenuItems.Remove(InfoField);
            GameObject.Destroy(InfoField);
            InfoMenu.rearrange();
            plane.SetActive(true);
            ac.RemovePlaneTracked(transform);
        }
    }
}
