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
    public Menu Ebenen;
    public Menu Ebeneninfos;
    public Menu Geraden;
    public Menu Geradeninfos;
    public Transform trackedPlane;

    void AddSchnittkante(Transform t1,Transform t2)
    {
        GameObject kante = Instantiate(prefabSchnittkante);
        kante.GetComponent<SnapSchnittkante>().layer1 = t1;
        kante.GetComponent<SnapSchnittkante>().layer2 = t2;
        kante.transform.SetParent(pivot.transform);
        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        geradenInfo.GetComponent<UpdateInfos>().Layertransform=kante.transform;
        geradenInfo.transform.SetParent(Geradeninfos.transform);
        geradenInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, Geradeninfos.MenuItems.Count * 280);
        Geradeninfos.MenuItems.Add(geradenInfo);
    }

    void AddPlane()
    {
        GameObject ebene = Instantiate(prefabEbenen);
        GameObject ebeneUI = Instantiate(prefabEbenenUI);
        GameObject ebeneInfo = Instantiate(prefabEbenenInfo);
        foreach (GameObject ebenef1 in Ebenen.Items3D)
        {
            AddSchnittkante(ebenef1.transform, ebene.transform);
        }
        AddSchnittkante(trackedPlane, ebene.transform);
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
        Geraden.Items3D.Add(geraden);
        geraden.transform.SetParent(pivot.transform);
        geradenUI.transform.SetParent(Geraden.transform);
        geradenUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -((Geraden.MenuItems.Count * 510) + 140));
        Geraden.MenuItems.Add(geradenUI);
    }

    public void pressButton()
    {
        if (Ebenen.isopen)
        {
            AddPlane();
        }
        else if (Geraden.isopen)
        {
            //AddLine();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
