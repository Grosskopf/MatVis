using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponents : MonoBehaviour
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

    public Transform trackedPlane;

    void AddPlane()
    {
        GameObject ebene = Instantiate(prefabEbenen);
        GameObject ebeneUI = Instantiate(prefabEbenenUI);
        GameObject ebeneInfo = Instantiate(prefabEbenenInfo);
        string nameEbene = "Ebene " + Ebenen.Items3D.Count;
        foreach (GameObject ebenef1 in Ebenen.Items3D)
        {
            string nameEbene2 = "Ebene " + Ebenen.Items3D.IndexOf(ebenef1);
            AddSchnittkante(ebenef1.transform, ebene.transform, nameEbene2,nameEbene);
        }
        AddSchnittkante(trackedPlane, ebene.transform,"Tracker 3",nameEbene);
        Ebenen.Items3D.Add(ebene);
        ebene.transform.SetParent(pivot.transform);
        ebeneUI.transform.SetParent(Ebenen.transform);
        ebeneInfo.transform.SetParent(Ebeneninfos.transform);
        ebeneUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -((Ebenen.MenuItems.Count * 510) + 140));
        ebeneInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, Ebeneninfos.MenuItems.Count * 280);
        ebeneInfo.GetComponent<UpdateInfos>().Layertransform = ebene.transform;
        ebeneUI.GetComponent<SetPlane>().planetransform = ebene.transform;
        Ebeneninfos.MenuItems.Add(ebeneInfo);
        Ebenen.MenuItems.Add(ebeneUI);
    }
    void AddLine()
    {
        GameObject geraden = Instantiate(prefabGeraden);
        GameObject geradenUI = Instantiate(prefabGeradenUI);
        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        foreach (GameObject ebene in Ebenen.Items3D)
        {
            AddSchnittpunkt(geraden.transform, ebene.transform);
        }
        AddSchnittpunkt(geraden.transform, trackedPlane);

        Geraden.Items3D.Add(geraden);
        geraden.transform.SetParent(pivot.transform);
        geradenUI.transform.SetParent(Geraden.transform);
        geradenInfo.transform.SetParent(Geradeninfos.transform);
        geradenUI.GetComponent<SetGerade>().linetransform = geraden.transform;
        geradenUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -((Geraden.MenuItems.Count * 510) + 140));

        geradenInfo.GetComponent<UpdateInfos>().Layertransform = geraden.transform;
        geradenInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, Geradeninfos.MenuItems.Count * 280 - 133);

        Geradeninfos.MenuItems.Add(geradenInfo);
        Geraden.MenuItems.Add(geradenUI);
    }

    void AddSchnittkante(Transform t1, Transform t2, string ebene1, string ebene2)
    {
        GameObject kante = Instantiate(prefabSchnittkante);
        kante.GetComponent<SnapSchnittkante>().layer1 = t1;
        kante.GetComponent<SnapSchnittkante>().layer2 = t2;
        kante.transform.SetParent(pivot.transform);

        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        geradenInfo.GetComponent<UpdateInfos>().Layertransform = kante.transform;
        geradenInfo.transform.SetParent(Geradeninfos.transform);
        geradenInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, Geradeninfos.MenuItems.Count * 280 - 133);
        Geradeninfos.MenuItems.Add(geradenInfo);

        GameObject winkelInfo = Instantiate(prefabSchnittWinkelInfo);
        winkelInfo.GetComponent<UpdateWinkel>().transform1 = t1;
        winkelInfo.GetComponent<UpdateWinkel>().transform2 = t2;
        winkelInfo.GetComponent<UpdateWinkel>().Transform1Description = ebene1;
        winkelInfo.GetComponent<UpdateWinkel>().Transform1Description = ebene2;
        winkelInfo.transform.SetParent(Winkelinfos.transform);
        winkelInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, Winkelinfos.MenuItems.Count * 280 - 133 * 4);
        Winkelinfos.MenuItems.Add(winkelInfo);

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
        punktInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, Punktinfos.MenuItems.Count * 280 - 133 * 2);
        Punktinfos.MenuItems.Add(punktInfo);
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
    void Update()
    {
        
    }
}
