using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR_LINUX
using Vuforia;
#endif
public class AddMarkerplane : MonoBehaviour
{
    // Start is called before the first frame update

    public bool istracked;
    //public MenuButton InfoMenu;
    //private GameObject InfoField;
    //public GameObject InfoFieldPrefab;
    public ComponentsHandler ac;
    public GameObject plane;
    void Start()
    {
        
    }
    public void tracked()
    {

        ac.AddPlaneTracked(transform);
        plane.SetActive(true);
        istracked = true;
    }
    public void untracked()
    {

        ac.RemovePlaneTracked(transform);
        plane.SetActive(false);
        istracked = false;
    }
    // Update is called once per frame
    void Update()
    {
/*#if !UNITY_EDITOR_LINUX
        if(
        (GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
        GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)&&
        !istracked
        )
        {
            istracked = true;

            /*if (InfoField == null || InfoField.transform.parent != InfoMenu.transform)
            {
                InfoField = GameObject.Instantiate(InfoFieldPrefab);
                InfoField.transform.SetParent(InfoMenu.transform);
                InfoField.GetComponent<UpdateInfos>().Layertransform = transform;
                InfoField.GetComponent<UpdateInfos>().Name.SetText(transform.name);
                InfoMenu.MenuItems.Add(InfoField);
                InfoMenu.Rearrange();
                if (!InfoMenu.isopen)
                {
                    InfoField.SetActive(false);
                }
                ac.AddPlaneTracked(transform);
            }*//*
            ac.AddPlaneTracked(transform);
            plane.SetActive(true);
        }
        else if(istracked &&
        !(GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED ||
        GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        )
        {
            istracked = false;
            /*InfoMenu.MenuItems.Remove(InfoField);
            GameObject.Destroy(InfoField);
            InfoMenu.Rearrange();*//*
            plane.SetActive(false);
            ac.RemovePlaneTracked(transform);
        }
#endif
        */
    }
}
