using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComponentsHandler : MonoBehaviour
{
    public GameObject pivot;
    public GameObject prefabEbenen;
    public GameObject prefabEbenenUI;
    public GameObject prefabEbenenInfo;
    public GameObject prefabGeraden;
    public GameObject prefabGeradenUI;
    public GameObject prefabGeradenInfo;
    public GameObject prefabSchnittkante;
    public GameObject prefabSchnittpunkt;
    public GameObject prefabSchnittpunktInfo;
    public GameObject prefabSchnittWinkelInfo;

    public Menu Ebenen;
    public Menu Ebeneninfos;
    public Menu Geraden;
    public Menu Geradeninfos;
    public Menu Punktinfos;
    public Menu Winkelinfos;

    public List<Transform> trackedPlanes;
    public TextMeshProUGUI text;

    public void AddPlaneTracked(Transform tracker)
    {
        for (int i = 0; i < Ebenen.Items3D.Count; i++)
        {
            GameObject ebenef1 = Ebenen.Items3D[i];
            string nameEbene2 = "Ebene " + Ebenen.Items3D.IndexOf(ebenef1);
            AddSchnittkante(ebenef1.transform, tracker, nameEbene2, tracker.name);
        }
        foreach (Transform trackedPlane in trackedPlanes)
        {
            if (trackedPlane.GetComponent<AddMarkerplane>().istracked && trackedPlane!=tracker)
            {
                AddSchnittkante(trackedPlane, tracker, trackedPlane.name, tracker.name);
            }
        }
        trackedPlanes.Add(tracker);
    }
    public void RemovePlaneTracked(Transform tracker)
    {
        text.text = "1";
        List<GameObject> geradenToRemove = new List<GameObject>();
        foreach (GameObject geradeninfo in Geradeninfos.MenuItems)
        {
            SnapSchnittkante kante = geradeninfo.GetComponent<UpdateInfos>().Layertransform.GetComponent<SnapSchnittkante>();
            if(kante != null)
            {
                if (kante.layer1 == tracker || kante.layer2 == tracker)
                {
                    geradenToRemove.Add(geradeninfo);
                }
            }
        }
        text.text = "2";
        List<GameObject> winkelToRemove = new List<GameObject>();
        foreach (GameObject winkel in Winkelinfos.MenuItems)
        {
            if (winkel.GetComponent<UpdateWinkel>().transform1 == tracker || winkel.GetComponent<UpdateWinkel>().transform2 == tracker)
            {
                winkelToRemove.Add(winkel);
            }
        }

        text.text = "3";
        foreach (GameObject winkel in winkelToRemove)
        {
            Winkelinfos.MenuItems.Remove(winkel);
            GameObject.Destroy(winkel);
        }

        text.text = "4";
        foreach (GameObject gerade in geradenToRemove)
        {
            Geradeninfos.MenuItems.Remove(gerade);
            text.text = "5";
            GameObject.Destroy(
            gerade.GetComponent<UpdateInfos>().Layertransform.gameObject);
            text.text = "6";
            GameObject.Destroy(gerade);
            text.text = "7";
        }

        trackedPlanes.Remove(tracker);
    }

    void AddPlane()
    {
        GameObject ebene = Instantiate(prefabEbenen);
        GameObject ebeneUI = Instantiate(prefabEbenenUI);
        GameObject ebeneInfo = Instantiate(prefabEbenenInfo);
        ebeneUI.transform.SetParent(Ebenen.transform);
        ebeneInfo.transform.SetParent(Ebeneninfos.transform);
        ebene.transform.SetParent(pivot.transform);
        ebene.transform.localPosition = new Vector3(0, 0, 0);
        string nameEbene = "Ebene " + Ebenen.Items3D.Count;
        ebeneInfo.GetComponent<UpdateInfos>().Name.SetText(nameEbene);
        foreach (GameObject ebenef1 in Ebenen.Items3D)
        {
            string nameEbene2 = "Ebene " + Ebenen.Items3D.IndexOf(ebenef1);
            AddSchnittkante(ebenef1.transform, ebene.transform, nameEbene2,nameEbene);
        }
        foreach (Transform trackedPlane in trackedPlanes)
        {
            if (trackedPlane.GetComponent<AddMarkerplane>().istracked)
            {
                AddSchnittkante(trackedPlane, ebene.transform, trackedPlane.name, nameEbene);
            }
        }
        Ebenen.Items3D.Add(ebene);
        ebeneInfo.GetComponent<UpdateInfos>().Layertransform = ebene.transform;
        if (!Ebeneninfos.isopen)
        {
            ebeneInfo.SetActive(false);
        }
        ebeneUI.GetComponent<SetPlane>().planetransform = ebene.transform;
        Ebeneninfos.MenuItems.Add(ebeneInfo);
        Ebenen.MenuItems.Add(ebeneUI);
        Ebenen.rearrange();
        Ebeneninfos.rearrange();
    }
    void AddLine()
    {
        GameObject geraden = Instantiate(prefabGeraden);
        GameObject geradenUI = Instantiate(prefabGeradenUI);
        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        string nameGerade = "Gerade " + Geraden.Items3D.Count;
        geradenInfo.GetComponent<UpdateInfos>().Name.SetText(nameGerade);
        foreach (GameObject ebene in Ebenen.Items3D)
        {
            AddSchnittpunkt(geraden.transform, ebene.transform);
        }

        foreach (Transform trackedPlane in trackedPlanes)
        {
            if (trackedPlane.GetComponent<AddMarkerplane>().istracked)
            {
                AddSchnittpunkt(geraden.transform, trackedPlane);
            }
        }

        Geraden.Items3D.Add(geraden);
        geraden.transform.SetParent(pivot.transform);
        geradenUI.transform.SetParent(Geraden.transform);
        geradenInfo.transform.SetParent(Geradeninfos.transform);
        geradenUI.GetComponent<SetGerade>().linetransform = geraden.transform;

        geradenInfo.GetComponent<UpdateInfos>().Layertransform = geraden.transform;
        if (!Geradeninfos.isopen)
        {
            geradenInfo.SetActive(false);
        }
        Geradeninfos.MenuItems.Add(geradenInfo);
        Geraden.MenuItems.Add(geradenUI);
        Geraden.rearrange();
        Geradeninfos.rearrange();
        geraden.transform.localPosition = new Vector3(0, 0, 0);
    }

    void AddSchnittkante(Transform t1, Transform t2, string ebene1, string ebene2)
    {
        GameObject kante = Instantiate(prefabSchnittkante);
        kante.GetComponent<SnapSchnittkante>().layer1 = t1;
        kante.GetComponent<SnapSchnittkante>().layer2 = t2;
        kante.transform.SetParent(pivot.transform);

        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        geradenInfo.GetComponent<UpdateInfos>().Name.SetText("Schnitt\r\n" + ebene1 + "\r\n" + ebene2);
        geradenInfo.GetComponent<UpdateInfos>().Layertransform = kante.transform;
        geradenInfo.transform.SetParent(Geradeninfos.transform);
        if (!Geradeninfos.isopen)
        {
            geradenInfo.SetActive(false);
        }
        Geradeninfos.MenuItems.Add(geradenInfo);
        Geradeninfos.rearrange();

        GameObject winkelInfo = Instantiate(prefabSchnittWinkelInfo);
        winkelInfo.GetComponent<UpdateWinkel>().transform1 = t1;
        winkelInfo.GetComponent<UpdateWinkel>().transform2 = t2;
        winkelInfo.GetComponent<UpdateWinkel>().Transform1Description = ebene1;
        winkelInfo.GetComponent<UpdateWinkel>().Transform2Description = ebene2;
        winkelInfo.transform.SetParent(Winkelinfos.transform);
        if (!Winkelinfos.isopen)
        {
            winkelInfo.active = false;
        }
        Winkelinfos.MenuItems.Add(winkelInfo);
        Winkelinfos.rearrange();

    }

    private void AddSchnittpunkt(Transform transformGerade, Transform transformEbene)
    {
        GameObject punkt = Instantiate(prefabSchnittpunkt);
        punkt.GetComponent<Findschnittpunkt>().Gerade = transformGerade;
        punkt.GetComponent<Findschnittpunkt>().Ebene = transformEbene;
        punkt.transform.SetParent(pivot.transform);

        GameObject punktInfo = Instantiate(prefabSchnittpunktInfo);
        punktInfo.GetComponent<UpdateInfos>().Layertransform = punkt.transform;
        punktInfo.transform.SetParent(Punktinfos.transform);
        if (!Punktinfos.isopen)
        {
            punktInfo.active = false;
        }
        Punktinfos.MenuItems.Add(punktInfo);
        Punktinfos.rearrange();
    }

    public void pressButton()
    {
        if (Ebenen.isopen)
        {
            AddPlane();
        }
        else if (Geraden.isopen)
        {
            AddLine();
        }
    }
    // Update is called once per frame
    UpdateInfos getLayerinfo(Transform t)
    {

        for (int i = 0; i < Ebeneninfos.MenuItems.Count; i++)
        {
            GameObject ebene = Ebeneninfos.MenuItems[i];
            UpdateInfos info = ebene.GetComponent<UpdateInfos>();
            if (info.Layertransform == t)
            {
                return info;
            }

        }
        return null;
    }

    List<UpdateInfos> findassociated(Transform tf)
    {
        List<UpdateInfos> infos = new List<UpdateInfos>();
        for (int i = 0; i < Geradeninfos.MenuItems.Count; i++)
        {
            GameObject gerade = Geradeninfos.MenuItems[i];
            UpdateInfos info = gerade.GetComponent<UpdateInfos>();
            if (info.Layertransform == tf)
            {
                SnapSchnittkante schnittkante = info.Layertransform.gameObject.GetComponent<SnapSchnittkante>();
                if (schnittkante != null)
                {
                    infos.Add(getLayerinfo(schnittkante.layer1));
                    infos.Add(getLayerinfo(schnittkante.layer2));
                }
                infos.Add(info);
            }
            else
            {
                SnapSchnittkante schnittkante = info.Layertransform.gameObject.GetComponent<SnapSchnittkante>();
                if (schnittkante != null)
                {
                    if (schnittkante.layer1 == tf || schnittkante.layer2 == tf)
                    {
                        infos.Add(info);
                    }
                }
            }
        }
        for (int i = 0; i < Ebeneninfos.MenuItems.Count; i++)
        {
            GameObject ebene = Ebeneninfos.MenuItems[i];
            UpdateInfos info = ebene.GetComponent<UpdateInfos>();
            if (info.Layertransform == tf)
            {
                infos.Add(info);
            }

        }
        return infos;
    }
    List<UpdateWinkel> findassociatedangles(Transform tf)
    {
        List<UpdateWinkel> angles = new List<UpdateWinkel>();
        UpdateInfos layer = getLayerinfo(tf);
        foreach(GameObject angleinfo in Winkelinfos.MenuItems)
        {
            UpdateWinkel angle = angleinfo.GetComponent<UpdateWinkel>();
            if(angle.transform1 == tf || angle.transform2 == tf)
            {
                angles.Add(angle);
            }
        }
        for (int i = 0; i < Geradeninfos.MenuItems.Count; i++)
        {
            UpdateInfos info = Geradeninfos.MenuItems[i].GetComponent<UpdateInfos>();
            if (info.Layertransform == tf)
            {
                SnapSchnittkante schnittkante = info.Layertransform.gameObject.GetComponent<SnapSchnittkante>();
                if (schnittkante != null)
                {
                    foreach (GameObject angleinfo in Winkelinfos.MenuItems)
                    {
                        UpdateWinkel angle = angleinfo.GetComponent<UpdateWinkel>();
                        if (angle.transform1 == schnittkante.layer1 && angle.transform2 == schnittkante.layer2)
                        {
                            angles.Add(angle);
                        }
                    }
                }
            }
        }
        return angles;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            Transform found = null;
            if (Physics.Raycast(ray,out hit))
            {
                Transform selectedObject = hit.transform;
                for(int i =0; i< Ebeneninfos.MenuItems.Count; i++)
                {
                    GameObject ebene = Ebeneninfos.MenuItems[i];
                    UpdateInfos infos = ebene.GetComponent<UpdateInfos>();
                    if (infos.Layertransform == selectedObject)
                    {
                        found = selectedObject;
                        text.SetText("selected layer: " + infos.Name.text);
                    }
                    if (infos.Layertransform == selectedObject.parent)
                    {
                        found = selectedObject.parent;
                        text.SetText("selected layer: " + infos.Name.text);
                    }
                }
                for(int i = 0; i< Geradeninfos.MenuItems.Count; i++)
                {
                    GameObject gerade = Geradeninfos.MenuItems[i];
                    UpdateInfos infos = gerade.GetComponent<UpdateInfos>();
                    if (infos.Layertransform == selectedObject.parent)
                    {
                        found = selectedObject.parent;
                        text.SetText("selected Gerade: " + infos.Name.text);
                    }
                }
                for(int i = 0; i< Punktinfos.MenuItems.Count; i++)
                {
                    GameObject punkt = Punktinfos.MenuItems[i];
                    UpdateInfos infos = punkt.GetComponent<UpdateInfos>();
                    if (infos.Layertransform == selectedObject.parent)
                    {
                        found = selectedObject.parent;
                        text.SetText("selected Punkt: " + infos.Name.text);
                    }
                }
                /*    public Menu Ebenen;
                    public Menu Ebeneninfos;
                    public Menu Geraden;
                    public Menu Geradeninfos;
                    public Menu Punktinfos;
                    public Menu Winkelinfos;*/
                //text.SetText(selectedObject.name);
            }
            if (found == null)
            {
                foreach (GameObject info in Ebeneninfos.MenuItems)
                {
                    foreach(CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                    {
                        cr.SetAlpha(1.0f);
                    }
                }
                foreach (GameObject info in Geradeninfos.MenuItems)
                {
                    foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                    {
                        cr.SetAlpha(1.0f);
                    }
                }
                foreach (GameObject info in Punktinfos.MenuItems)
                {
                    foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                    {
                        cr.SetAlpha(1.0f);
                    }
                }
                foreach (GameObject info in Winkelinfos.MenuItems)
                {
                    foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                    {
                        cr.SetAlpha(1.0f);
                    }
                }
                text.SetText("selected nothing important");
            }
            else
            {
                List<UpdateInfos> fields=findassociated(found);
                List<UpdateWinkel> angles = findassociatedangles(found);
                foreach (GameObject info in Ebeneninfos.MenuItems)
                {
                    if (fields.Contains(info.GetComponent<UpdateInfos>()))
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(1.0f);
                        }
                    } else
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(0.0f);
                        }
                    }
                }
                foreach (GameObject info in Geradeninfos.MenuItems)
                {
                    if (fields.Contains(info.GetComponent<UpdateInfos>()))
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(1.0f);
                        }
                    }
                    else
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(0.0f);
                        }
                    }
                }
                foreach (GameObject info in Punktinfos.MenuItems)
                {
                    if (fields.Contains(info.GetComponent<UpdateInfos>()))
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(1.0f);
                        }
                    }
                    else
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(0.0f);
                        }
                    }
                }
                foreach (GameObject info in Winkelinfos.MenuItems)
                {
                    if (angles.Contains(info.GetComponent<UpdateWinkel>()))
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(1.0f);
                        }
                    }
                    else
                    {
                        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(0.0f);
                        }
                    }
                }
            }
        }
    }
}
