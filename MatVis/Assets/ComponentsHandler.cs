using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEditorInternal;

public class VirtualCompnonent
{
    public String Name;
    public GameObject InfoUI;
    public MenuButton InfoParentMenu;
    public Color color;
    public virtual Transform GetTransform() { return null; }
    public virtual void Dissolve() {
    InfoParentMenu.MenuItems.Remove(InfoUI);
    GameObject.Destroy(InfoUI);
    }
    public virtual List<VirtualCompnonent> GetChildren() { return null; }
}
public class VisibleComponent : VirtualCompnonent
{
    public GameObject Model3D;
    public override Transform GetTransform() { return Model3D.transform; }
    public override void Dissolve()
    {
        GameObject.Destroy(Model3D);
        base.Dissolve();
    }
}
public class EditableComponent : VisibleComponent
{
    public GameObject ConfigUI;
    public MenuButton ConfigParentMenu;
    public List<Distanzen> Distanzen= new List<Distanzen>();

    public override List<VirtualCompnonent> GetChildren()
    {
        List<VirtualCompnonent> result = new List<VirtualCompnonent>();
        result.AddRange(Distanzen);
        return result;
    }
    public override void Dissolve()
    {
        Resources.FindObjectsOfTypeAll<ComponentsHandler>()[0].EditableComponents.Remove(this);
        ConfigParentMenu.MenuItems.Remove(ConfigUI);
        GameObject.Destroy(InfoUI);
        foreach(Distanzen dist in Distanzen)
        {
            dist.Dissolve();
        }
        base.Dissolve();
    }
}
public class Ebene : EditableComponent
{
    public List<Schnittkante> Schnittkanten;
    public List<Schnittpunkt> Schnittpunkte;
    public override void Dissolve()
    {
        foreach (Schnittpunkt punkt in Schnittpunkte)
        {
            punkt.Dissolve();
        }
        foreach (Schnittkante kante in Schnittkanten)
        {
            kante.Dissolve();
        }
        base.Dissolve();
    }
    public override List<VirtualCompnonent> GetChildren() {
        List<VirtualCompnonent> result = new List<VirtualCompnonent>();
        result.AddRange(Schnittkanten);
        result.AddRange(Schnittpunkte);
        result.AddRange(Distanzen);
        return result;
    }
    //public UnityEvent SomethingChanged =new UnityEvent();
}
public class TrackedEbene: VirtualCompnonent
{
    public List<Schnittkante> Schnittkanten;
    public List<Schnittpunkt> Schnittpunkte;
    public List<Distanzen> Distanzen = new List<Distanzen>();
    public override List<VirtualCompnonent> GetChildren()
    {
        List<VirtualCompnonent> result = new List<VirtualCompnonent>();
        result.AddRange(Schnittkanten);
        result.AddRange(Schnittpunkte);
        result.AddRange(Distanzen);
        return result;
    }
    public Transform Transform;
    public override Transform GetTransform() { return Transform; }

    //public UnityEvent SomethingChanged;
}
public class Gerade : EditableComponent
{
    public List<Schnittpunkt> Schnittpunkte;
    public override List<VirtualCompnonent> GetChildren()
    {
        List<VirtualCompnonent> result = new List<VirtualCompnonent>();
        result.AddRange(Schnittpunkte);
        result.AddRange(Distanzen);
        return result;
    }
    public override void Dissolve()
    {
        foreach(Schnittpunkt punkt in Schnittpunkte)
        {
            punkt.Dissolve();
        }
        base.Dissolve();
    }
}
public class Punkt : EditableComponent
{
}
public class Winkel : VirtualCompnonent
{
    public Schnittkante Schnittkante;
    public override void Dissolve()
    {
        base.Dissolve();
    }
}
public class Schnittkante : VisibleComponent
{
    public VirtualCompnonent Ebene1;
    public VirtualCompnonent Ebene2;
    public Winkel Winkel;
    public List<Schnittpunkt> Schnittpunkte;
    public override List<VirtualCompnonent> GetChildren()
    {
        List<VirtualCompnonent> result = new List<VirtualCompnonent>();
        result.AddRange(Schnittpunkte);
        return result;
    }
    public override void Dissolve()
    {
        Winkel.Dissolve();
        base.Dissolve();
    }
}
public class Schnittpunkt : VisibleComponent
{
    public VirtualCompnonent Ebene;
    public Gerade Gerade;
}
public class Distanzen : VirtualCompnonent
{
    public VirtualCompnonent Element1; //vorrangig punkte, zweitrangig geraden
    public VirtualCompnonent Element2; //vorrangig ebenen, zweitrangig geraden
}
public class ComponentsHandler : MonoBehaviour
{
    public GameObject pivot;
    public GameObject prefabEbenen;
    public GameObject prefabEbenenUI;
    public GameObject prefabEbenenInfo;
    public GameObject prefabGeraden;
    public GameObject prefabGeradenUI;
    public GameObject prefabGeradenInfo;
    public GameObject prefabPunkte;
    public GameObject prefabPunkteUI;
    public GameObject prefabPunkteInfo;
    public GameObject prefabSchnittkante;
    public GameObject prefabSchnittpunkt;
    public GameObject prefabSchnittpunktInfo;
    public GameObject prefabSchnittWinkelInfo;
    public GameObject prefabDistanzInfo;

    public Buttonlist InfoMenu;
    public Buttonlist AddMenu;

    private MenuButton Ebenen;
    private MenuButton EbenenInfos;
    private MenuButton Geraden;
    private MenuButton GeradenInfos;
    private MenuButton Punkte;
    private MenuButton PunkteInfos;
    private MenuButton WinkelInfos;
    private MenuButton DistanzInfos;

    public List<TrackedEbene> TrackedPlanes;
    public List<EditableComponent> EditableComponents;


    public Addbutton addbutton;

    public TextMeshProUGUI text;

    void Start()
    {
        AddMenu.StartFromCH();
        InfoMenu.StartFromCH();
        Ebenen = AddMenu.GetMenu("Ebenen");
        Geraden = AddMenu.GetMenu("Geraden");
        Punkte = AddMenu.GetMenu("Punkte");
        EbenenInfos = InfoMenu.GetMenu("EbenenInfos");
        GeradenInfos = InfoMenu.GetMenu("GeradenInfos");
        PunkteInfos = InfoMenu.GetMenu("PunkteInfos");
        WinkelInfos = InfoMenu.GetMenu("WinkelInfos");
        DistanzInfos = InfoMenu.GetMenu("DistanzInfos");
        EditableComponents = new List<EditableComponent>();
        TrackedPlanes = new List<TrackedEbene>();

    }

    public void AddPlaneTracked(Transform tracker)
    {
        TrackedEbene NewTrackedPlane = new TrackedEbene { Transform = tracker,Name= tracker.name, color = UnityEngine.Random.ColorHSV(0f, 1f, 0.2f, 0.2f, 1f, 1f, 0.8f, 0.8f),InfoParentMenu=EbenenInfos };

        NewTrackedPlane.Schnittkanten = new List<Schnittkante>();
        NewTrackedPlane.Schnittpunkte = new List<Schnittpunkt>();
        if (TrackedPlanes.Exists((TrackedEbene obj) => obj.Name == tracker.name))
        {
            Debug.Log("found old tracked");
            NewTrackedPlane = TrackedPlanes.Find((TrackedEbene obj) => obj.Transform == tracker);
        }
        else
        {
            Debug.Log("making new tracked: "+tracker.name);
            TrackedPlanes.Add(NewTrackedPlane);
            foreach (MeshRenderer mr in tracker.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material.color = NewTrackedPlane.color;
            }
            GameObject ebeneInfo = Instantiate(prefabEbenenInfo);
            NewTrackedPlane.InfoUI = ebeneInfo;

            ebeneInfo.transform.SetParent(EbenenInfos.transform);
            tracker.GetComponent<AddMarkerplane>().plane.GetComponent<Splitplane>().ebeneninfos = NewTrackedPlane;
            UpdateInfos updateInfos;
            if (ebeneInfo.TryGetComponent<UpdateInfos>(out updateInfos))
            {
                updateInfos.compnonent = NewTrackedPlane;
                updateInfos.Layertransform = tracker;
                updateInfos.Name.color = NewTrackedPlane.color;
                updateInfos.Name.text = tracker.name;
                updateInfos.menu = EbenenInfos;
            }
            EbenenInfos.MenuItems.Add(ebeneInfo);
            if (!EbenenInfos.isopen)
            {
                ebeneInfo.SetActive(false);
            }
            EbenenInfos.Rearrange();

        }

        //for (int i = 0; i < EditableComponents.Count; i++)
        //{
        //    if (EditableComponents[i] is Ebene)
        //    {
        //        AddSchnittkante(EditableComponents[i], NewTrackedPlane);
        //GameObject ebenef1 = Ebenen.Items3D[i];
        //string nameEbene2 = "Ebene R" + TrackedPlanes.Count;
        //AddSchnittkante(ebenef1.transform, tracker, nameEbene2, tracker.name);
        //    }
        //}
        foreach (EditableComponent e in EditableComponents)
        {
            if(e is Ebene)
            {
                AddSchnittkante(e, NewTrackedPlane);
            }
            else if (e is Gerade)
            {
                AddSchnittpunkt((Gerade)e, NewTrackedPlane);
                AddDistanz(e, NewTrackedPlane);
            }
        }/*
        foreach (Ebene ebene in EditableComponents)
        {
            AddSchnittkante(ebene, NewTrackedPlane);
        }
        foreach (Gerade gerade in EditableComponents)
        {
            AddSchnittpunkt(gerade, NewTrackedPlane);
            AddDistanz(gerade, NewTrackedPlane);
        }*/
        foreach (TrackedEbene OtherTrackedPlane in TrackedPlanes)
        {
            if (OtherTrackedPlane.Transform.GetComponent<AddMarkerplane>().istracked && OtherTrackedPlane.Transform != tracker)
            {
                AddSchnittkante(OtherTrackedPlane, NewTrackedPlane);
            }
        }
        /*if (TrackedPlanes.Exists((TrackedEbene obj) => obj.Transform == tracker))
        {
            TrackedPlanes.Find((TrackedEbene obj) => obj.Transform == tracker).Transform.gameObject.SetActive(true);
        }
        else
        {
            TrackedPlanes.Add(NewTrackedPlane);
        }*/
    }
    public void RemovePlaneTracked(Transform tracker)
    {
        //text.text = "1";
        TrackedEbene TrackedPlaneToRemove = TrackedPlanes.Find((TrackedEbene obj) => obj.Name == tracker.name);
        if (TrackedPlaneToRemove == null)
        {
            Debug.Log("doesn't exist");
            return;
        }
        foreach (Schnittkante schnittkante in TrackedPlaneToRemove.Schnittkanten)
        {
            schnittkante.Dissolve();
        }
        foreach (Schnittpunkt schnittkpunkt in TrackedPlaneToRemove.Schnittpunkte)
        {
            schnittkpunkt.Dissolve();
        }
        foreach (Distanzen Distanz in TrackedPlaneToRemove.Distanzen)
        {
            Distanz.Dissolve();
        }
        TrackedPlanes.Remove(TrackedPlaneToRemove);
        TrackedPlaneToRemove.Dissolve();
        //List<GameObject> geradenToRemove = new List<GameObject>();
        //foreach (GameObject geradeninfo in GeradenInfos.MenuItems)
        //{
        //    SnapSchnittkante kante = geradeninfo.GetComponent<UpdateInfos>().Layertransform.GetComponent<SnapSchnittkante>();
        //    if (kante != null)
        //    {
        //        if (kante.layer1 == tracker || kante.layer2 == tracker)
        //        {
        //            geradenToRemove.Add(geradeninfo);
        //        }
        //    }
        //}
        //text.text = "2";
        //List<GameObject> winkelToRemove = new List<GameObject>();
        //foreach (GameObject winkel in WinkelInfos.MenuItems)
        //{
        //    if (winkel.GetComponent<UpdateWinkel>().transform1 == tracker || winkel.GetComponent<UpdateWinkel>().transform2 == tracker)
        //    {
        //        winkelToRemove.Add(winkel);
        //    }
        //}

        //text.text = "3";
        //foreach (GameObject winkel in winkelToRemove)
        //{
        //    WinkelInfos.MenuItems.Remove(winkel);
        //    GameObject.Destroy(winkel);
        //}

        //text.text = "4";
        //foreach (GameObject gerade in geradenToRemove)
        //{
        //    GeradenInfos.MenuItems.Remove(gerade);
        //    text.text = "5";
        //    GameObject.Destroy(
        //    gerade.GetComponent<UpdateInfos>().Layertransform.gameObject);
        //    text.text = "6";
        //    GameObject.Destroy(gerade);
        //    text.text = "7";
        //}

        //TrackedPlanes.Find((TrackedEbene obj) => obj.Transform == tracker).Transform.gameObject.SetActive(false);
    }
    public void AddObject(VirtualCompnonent virtc, GameObject Object3D, GameObject ObjectUI, GameObject ObjectInfo, MenuButton ParentUI, MenuButton ParentInfo, Color color, string name)
    {
        ObjectUI.transform.SetParent(ParentUI.transform);
        if (!(ObjectInfo is GameObject))
        {
            Debug.LogError("objectinfo is no gameobject");
        }
        if (!(ParentInfo is MenuButton))
        {
            Debug.LogError("parentinfo is no gameobject");
        }
        ObjectInfo.transform.SetParent(ParentInfo.transform);
        UpdateInfos updateInfos;
        if(ObjectInfo.TryGetComponent<UpdateInfos>(out updateInfos))
        {
            updateInfos.Layertransform = Object3D.transform;
            updateInfos.Name.color = color;
            updateInfos.Name.text = name;
        }
        if (ObjectInfo.GetComponent<InfoMenuItem>() != null)
        {
            ObjectInfo.GetComponent<InfoMenuItem>().compnonent = virtc;
            ObjectInfo.GetComponent<InfoMenuItem>().menu = ParentInfo;
        }
        Object3D.transform.SetParent(pivot.transform);
        Object3D.transform.localPosition = new Vector3(0, 0, 0);
        Object3D.transform.rotation = new Quaternion(0,0,0,0);
        //ParentUI.Items3D.Add(Object3D);
        ParentInfo.MenuItems.Add(ObjectInfo);
        ParentUI.MenuItems.Add(ObjectUI);
        foreach (MeshRenderer mr in Object3D.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = color;
        }
        if (ObjectUI.GetComponent<EditCompUI>() != null)
        {
            ObjectUI.GetComponent<EditCompUI>().Transform = Object3D.transform;
            ObjectUI.GetComponent<EditCompUI>().component=(EditableComponent)virtc;
        }
        if (!ParentInfo.isopen)
        {
            ObjectInfo.SetActive(false);
        }
        ParentUI.Rearrange();
        ParentInfo.Rearrange();
    }
    public void AddPlane()
    {
        GameObject ebene = Instantiate(prefabEbenen);
        GameObject ebeneUI = Instantiate(prefabEbenenUI);
        GameObject ebeneInfo = Instantiate(prefabEbenenInfo);
        Ebene newEbene = new Ebene { ConfigUI = ebeneUI, ConfigParentMenu = Ebenen, Name= "Ebene E" + EditableComponents.FindAll((EditableComponent obj) => obj is Ebene).Count, InfoUI = ebeneInfo, InfoParentMenu = EbenenInfos, Model3D = ebene , color= UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 0.8f, 0.8f) };
        AddObject(newEbene, ebene, ebeneUI, ebeneInfo, Ebenen, EbenenInfos, newEbene.color, newEbene.Name);
        newEbene.Schnittkanten = new List<Schnittkante>();
        newEbene.Schnittpunkte = new List<Schnittpunkt>();
        ebene.GetComponent<Splitplane>().ebeneninfos = newEbene;
        foreach (EditableComponent e in EditableComponents)
        {
            if(e is Gerade)
            {

                AddSchnittpunkt((Gerade)e, newEbene);
                AddDistanz(e, newEbene);
            }else if (e is Punkt)
            {

                AddDistanz(e, newEbene);
            }else if (e is Ebene)
            {
                AddSchnittkante(e, newEbene);
            }
        }
        /*
        foreach (Gerade gerade in EditableComponents)
        {
            AddSchnittpunkt(gerade, newEbene);
            AddDistanz(gerade, newEbene);
        }
        foreach (Punkt p in EditableComponents)
        {
            AddDistanz(p, newEbene);
        }*/
        //ebeneUI.transform.SetParent(Ebenen.transform);
        //ebeneInfo.transform.SetParent(EbenenInfos.gameObject.transform);
        //ebene.transform.SetParent(pivot.transform);
        //ebene.transform.localPosition = new Vector3(0, 0, 0);
        //string nameEbene = "Ebene E" + EditableComponents.FindAll((EditableComponent obj) => obj is Ebene).Count;//Ebenen.Items3D.Count;
        //ebeneInfo.GetComponent<UpdateInfos>().Name.SetText(nameEbene);
        //foreach (GameObject ebenef1 in Ebenen.Items3D)
        //{
        //    string nameEbene2 = "Ebene " + Ebenen.Items3D.IndexOf(ebenef1);
        //    AddSchnittkante(ebenef1.transform, ebene.transform, nameEbene2, nameEbene);
        //}
        /*foreach (Ebene ebenef1 in EditableComponents)
        {
            AddSchnittkante(ebenef1, newEbene);
        }*/
        foreach (TrackedEbene trackedPlane in TrackedPlanes)
        {
            if (trackedPlane.Transform.gameObject.activeSelf)
            {
                AddSchnittkante(newEbene, trackedPlane);
            }
            //if (trackedPlane.GetComponent<AddMarkerplane>().istracked)
            //{
            //    AddSchnittkante(trackedPlane, ebene.transform, trackedPlane.name, nameEbene);
            //}
        }
        EditableComponents.Add(newEbene);
        //Ebenen.Items3D.Add(ebene);
        //ebeneInfo.GetComponent<UpdateInfos>().Layertransform = ebene.transform;
        //if (!EbenenInfos.isopen)
        //{
        //    ebeneInfo.SetActive(false);
        //}
        //ebeneUI.GetComponent<SetPlane>().planetransform = ebene.transform;
        //EbenenInfos.MenuItems.Add(ebeneInfo);
        ////Ebenen.MenuItems.Add(ebeneUI);
        //Ebenen.Rearrange();
        //EbenenInfos.Rearrange();
        UpdateAllLayers();
    }
    public void AddLine()
    {
        GameObject geraden = Instantiate(prefabGeraden);
        GameObject geradenUI = Instantiate(prefabGeradenUI);
        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        string nameGerade = "Gerade G" + EditableComponents.FindAll((EditableComponent obj) => obj is Gerade).Count;
        Gerade newGerade = new Gerade { ConfigUI = geradenUI, ConfigParentMenu = Geraden, InfoUI = geradenInfo, InfoParentMenu = GeradenInfos, Model3D = geraden, color = UnityEngine.Random.ColorHSV(0f, 1f, 0.7f, 0.7f, 1f, 1f, 0.8f, 0.8f) };
        AddObject(newGerade,geraden, geradenUI, geradenInfo, Geraden, GeradenInfos, newGerade.color, newGerade.Name);
        newGerade.Schnittpunkte = new List<Schnittpunkt>();
        //geradenInfo.GetComponent<UpdateInfos>().Name.SetText(nameGerade);
        foreach (EditableComponent e in EditableComponents)
        {
            if(e is Ebene)
            {
                Ebene ebene = (Ebene)e;
                AddSchnittpunkt(newGerade, ebene);
                AddDistanz(newGerade, ebene);
            }else if (e is Punkt)
            {
                AddDistanz(e, newGerade);
            }
        }
        /*foreach (Ebene ebene in EditableComponents)
        {
            AddSchnittpunkt(newGerade, ebene);
            AddDistanz(newGerade, ebene);
        }
        foreach (Punkt p in EditableComponents)
        {
            AddDistanz(p, newGerade);
        }*/

        foreach (TrackedEbene trackedPlane in TrackedPlanes)
        {
            if (trackedPlane.Transform.gameObject.activeSelf)
            {
                AddSchnittpunkt(newGerade, trackedPlane);
            }
            //if (trackedPlane.GetComponent<AddMarkerplane>().istracked)
            //{
            //    AddSchnittpunkt(geraden.transform, trackedPlane);
            //}
        }
        EditableComponents.Add(newGerade);
        /*
        Geraden.Items3D.Add(geraden);
        geraden.transform.SetParent(pivot.transform);
        geradenUI.transform.SetParent(Geraden.transform);
        geradenInfo.transform.SetParent(GeradenInfos.gameObject.transform);
        geradenUI.GetComponent<SetGerade>().linetransform = geraden.transform;

        geradenInfo.GetComponent<UpdateInfos>().Layertransform = geraden.transform;
        if (!GeradenInfos.isopen)
        {
            geradenInfo.SetActive(false);
        }
        GeradenInfos.MenuItems.Add(geradenInfo);
        Geraden.MenuItems.Add(geradenUI);
        Geraden.Rearrange();
        GeradenInfos.Rearrange();
        geraden.transform.localPosition = new Vector3(0, 0, 0);
        */
    }
    internal void AddPunkt()
    {
        GameObject punkt = Instantiate(prefabPunkte);
        GameObject punktUI = Instantiate(prefabPunkteUI);
        GameObject punktInfo = Instantiate(prefabPunkteInfo);
        string namePunkt = "Punkt P" + EditableComponents.FindAll((EditableComponent obj) => obj is Punkt).Count;
        punktInfo.GetComponent<UpdateInfos>().Name.text=namePunkt;
        Punkt p = new Punkt { Name=namePunkt, ConfigUI = punktUI, ConfigParentMenu = Punkte, InfoUI = punktInfo, InfoParentMenu = PunkteInfos, Model3D = punkt, color = UnityEngine.Random.ColorHSV(0f, 1f, 0.4f, 0.4f, 1f, 1f, 0.8f, 0.8f) };
        AddObject(p,punkt, punktUI, punktInfo, Punkte, PunkteInfos, p.color, p.Name);
        EditableComponents.Add(p);
        foreach (EditableComponent e in EditableComponents)
        {
            if(e is Ebene || e is Gerade)
            {
                AddDistanz(p, e);
            }
        }
        foreach (TrackedEbene ebene in TrackedPlanes)
        {
            AddDistanz(p, ebene);
        }
        //foreach (GameObject ebene in Punkte.Items3D)
        //{
        //    AddSchnittpunkt(punkt.transform, ebene.transform);
        //}

        //foreach (Transform trackedPlane in trackedPlanes)
        //{
        //    if (trackedPlane.GetComponent<AddMarkerplane>().istracked)
        //    {
        //        AddSchnittpunkt(punkt.transform, trackedPlane);
        //    }
        //}
        /*
        Punkte.Items3D.Add(punkt);
        punktUI.transform.SetParent(Punkte.transform);
        punktInfo.transform.SetParent(PunkteInfos.gameObject.transform);
        punkt.transform.SetParent(pivot.transform);
        punktUI.GetComponent<SetPunkt>().punkttransform = punkt.transform;

        punktInfo.GetComponent<UpdateInfos>().Layertransform = punkt.transform;
        if (!PunkteInfos.isopen)
        {
            punktInfo.SetActive(false);
        }
        PunkteInfos.MenuItems.Add(punktInfo);
        Punkte.MenuItems.Add(punktUI);
        Punkte.Rearrange();
        PunkteInfos.Rearrange();
        punkt.transform.localPosition = new Vector3(0, 0, 0);
        */
    }
    //void AddSchnittkante(Transform t1, Transform t2, string ebene1, string ebene2)
    void AddSchnittkante(VirtualCompnonent e1, VirtualCompnonent e2)
    {
        Debug.Log("adding schnittkante");
        GameObject kantenobjekt = Instantiate(prefabSchnittkante);
        GameObject geradenInfo = Instantiate(prefabGeradenInfo);
        Schnittkante kante = new Schnittkante { Model3D = kantenobjekt, Name= "Schnitt\r\n" + e1.Name + "\r\n" + e2.Name,Ebene1=e1,Ebene2=e2, InfoUI=geradenInfo, InfoParentMenu= GeradenInfos , color = UnityEngine.Random.ColorHSV(0f, 1f, 0.2f, 0.2f, 1f, 1f, 0.8f, 0.8f) };

        kante.Winkel = new Winkel { Schnittkante = kante, Name = "Winkel\r\n" + e1.Name + "\r\n" + e2.Name, color = kante.color, InfoParentMenu = WinkelInfos };
        foreach (MeshRenderer mr in kantenobjekt.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = kante.color;
        }
        kante.Schnittpunkte = new List<Schnittpunkt>();
        if (e1 is Ebene)
        {
            kante.Model3D.GetComponent<SnapSchnittkante>().layer1 = ((Ebene)e1).Model3D.transform;
            ((Ebene)e1).Schnittkanten.Add(kante);
        }
        else if (e1 is TrackedEbene)
        {
            kante.Model3D.GetComponent<SnapSchnittkante>().layer1 = ((TrackedEbene)e1).Transform;
            ((TrackedEbene)e1).Schnittkanten.Add(kante);
        }
        if (e2 is Ebene)
        {
            kante.Model3D.GetComponent<SnapSchnittkante>().layer2 = ((Ebene)e2).Model3D.transform;
            ((Ebene)e2).Schnittkanten.Add(kante);
        }
        else if (e2 is TrackedEbene)
        {
            kante.Model3D.GetComponent<SnapSchnittkante>().layer2 = ((TrackedEbene)e2).Transform;
            ((TrackedEbene)e2).Schnittkanten.Add(kante);
        }
        kante.Model3D.transform.SetParent(pivot.transform);
        kante.Model3D.GetComponent<SnapSchnittkante>().updateposrot();

        Debug.Log("added schnittkante");
        geradenInfo.GetComponent<UpdateInfos>().Name.SetText(kante.Name);
        geradenInfo.GetComponent<UpdateInfos>().Layertransform = kante.Model3D.transform;
        geradenInfo.transform.SetParent(GeradenInfos.transform);
        geradenInfo.GetComponent<UpdateInfos>().compnonent = kante;
        geradenInfo.GetComponent<UpdateInfos>().menu = GeradenInfos;
        if (!GeradenInfos.isopen)
        {
            geradenInfo.SetActive(false);
        }
        GeradenInfos.MenuItems.Add(geradenInfo);
        GeradenInfos.Rearrange();


        Debug.Log("added info");

        GameObject winkelInfo = Instantiate(prefabSchnittWinkelInfo);
        winkelInfo.GetComponent<UpdateWinkel>().transform1 = kante.Ebene1.GetTransform();
        winkelInfo.GetComponent<UpdateWinkel>().transform2 = kante.Ebene2.GetTransform();
        winkelInfo.GetComponent<UpdateWinkel>().Transform1Description = kante.Ebene1.Name;
        winkelInfo.GetComponent<UpdateWinkel>().Transform2Description = kante.Ebene2.Name;
        winkelInfo.GetComponent<UpdateWinkel>().compnonent = kante.Winkel;
        winkelInfo.GetComponent<UpdateWinkel>().menu = WinkelInfos;
        winkelInfo.transform.SetParent(WinkelInfos.transform);
        if (!WinkelInfos.isopen)
        {
            winkelInfo.SetActive(false);
        }
        WinkelInfos.MenuItems.Add(winkelInfo);
        WinkelInfos.Rearrange();
        kante.Winkel.InfoUI = winkelInfo;

        Debug.Log("added winkelinfo");
        UpdateAllLayers();
    }
    public void UpdateAllLayers()
    {

        foreach (EditableComponent e in EditableComponents)
        {
            if (e is Ebene) {
                //print("Ebene: " + e.Name);
                e.Model3D.GetComponent<Splitplane>().SomethingChanged();
            }
        }
        foreach(TrackedEbene e in TrackedPlanes)
        {
            e.Transform.GetComponent<AddMarkerplane>().plane.GetComponent<Splitplane>().SomethingChanged();
            //e.SomethingChanged.Invoke();
        }
    }
    private void AddDistanz(EditableComponent Element1, VirtualCompnonent Element2)
    {
        GameObject DistanzInfo = Instantiate(prefabDistanzInfo);
        Distanzen distanzen = new Distanzen() { Element1 = Element1, Element2 = Element2, InfoParentMenu=DistanzInfos, InfoUI=DistanzInfo };
        DistanzInfo.GetComponent<UpdateDistance>().Element1 = Element1;
        DistanzInfo.GetComponent<UpdateDistance>().Element2 = Element2;
        DistanzInfo.GetComponent<UpdateDistance>().compnonent = distanzen;
        DistanzInfo.GetComponent<UpdateDistance>().menu = DistanzInfos;
        DistanzInfo.transform.SetParent(DistanzInfos.transform);
        Element1.Distanzen.Add(distanzen);
        if(Element2 is EditableComponent)
        {
            ((EditableComponent)Element2).Distanzen.Add(distanzen);
        }
        else
        {
            ((TrackedEbene)Element2).Distanzen.Add(distanzen);
        }
        if (!DistanzInfos.isopen)
        {
            DistanzInfo.SetActive(false);
        }
        DistanzInfos.MenuItems.Add(DistanzInfo);
        DistanzInfos.Rearrange();
        UpdateAllLayers();
    }
    private void AddSchnittpunkt(Gerade SchnittGerade, VirtualCompnonent SchnittEbene)
    {
        GameObject punktInfo = Instantiate(prefabSchnittpunktInfo);
        GameObject punkt = Instantiate(prefabSchnittpunkt);
        Schnittpunkt schnittpunkt = new Schnittpunkt {Gerade=SchnittGerade, Ebene=SchnittEbene, InfoParentMenu=Punkte, InfoUI=punktInfo, Model3D= punkt, color = UnityEngine.Random.ColorHSV(0f, 1f, 0.2f, 0.2f, 1f, 1f, 0.8f, 0.8f) };

        foreach (MeshRenderer mr in punkt.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = schnittpunkt.color;
        }
        SchnittGerade.Schnittpunkte.Add(schnittpunkt);
        punkt.GetComponent<Findschnittpunkt>().Gerade = SchnittGerade.GetTransform();
        punkt.GetComponent<Findschnittpunkt>().Ebene = SchnittEbene.GetTransform();
        punkt.transform.SetParent(pivot.transform);

        punktInfo.GetComponent<UpdateInfos>().Layertransform = punkt.transform;
        punktInfo.GetComponent<UpdateInfos>().compnonent = schnittpunkt;
        punktInfo.GetComponent<UpdateInfos>().menu = PunkteInfos;
        punktInfo.transform.SetParent(PunkteInfos.transform);
        if (!PunkteInfos.isopen)
        {
            punktInfo.SetActive(false);
        }
        PunkteInfos.MenuItems.Add(punktInfo);
        PunkteInfos.Rearrange();
    }

    //// Update is called once per frame
    //UpdateInfos GetLayerinfo(Transform t)
    //{

    //    for (int i = 0; i < EbenenInfos.MenuItems.Count; i++)
    //    {
    //        GameObject ebene = EbenenInfos.MenuItems[i];
    //        UpdateInfos info = ebene.GetComponent<UpdateInfos>();
    //        if (info.Layertransform == t)
    //        {
    //            return info;
    //        }

    //    }
    //    return null;
    //}

    List<VirtualCompnonent> Findassociated(VirtualCompnonent tf)
    {
        List<VirtualCompnonent> infos = new List<VirtualCompnonent>();
        foreach (VirtualCompnonent Editable in EditableComponents)
        {
            if (tf == Editable || Editable.GetChildren().Contains(tf))
            {
                infos.Add(tf);
                infos.AddRange(tf.GetChildren());
            }
        }
        //for (int i = 0; i < GeradenInfos.MenuItems.Count; i++)
        //{
        //    GameObject gerade = GeradenInfos.MenuItems[i];
        //    UpdateInfos info = gerade.GetComponent<UpdateInfos>();
        //    if (info.Layertransform == tf)
        //    {
        //        SnapSchnittkante schnittkante = info.Layertransform.gameObject.GetComponent<SnapSchnittkante>();
        //        if (schnittkante != null)
        //        {
        //            infos.Add(GetLayerinfo(schnittkante.layer1));
        //            infos.Add(GetLayerinfo(schnittkante.layer2));
        //        }
        //        infos.Add(info);
        //    }
        //    else
        //    {
        //        SnapSchnittkante schnittkante = info.Layertransform.gameObject.GetComponent<SnapSchnittkante>();
        //        if (schnittkante != null)
        //        {
        //            if (schnittkante.layer1 == tf || schnittkante.layer2 == tf)
        //            {
        //                infos.Add(info);
        //            }
        //        }
        //    }
        //}
        //for (int i = 0; i < EbenenInfos.MenuItems.Count; i++)
        //{
        //    GameObject ebene = EbenenInfos.MenuItems[i];
        //    UpdateInfos info = ebene.GetComponent<UpdateInfos>();
        //    if (info.Layertransform == tf)
        //    {
        //        infos.Add(info);
        //    }

        //}
        return infos;
    }
    //List<UpdateWinkel> Findassociatedangles(Transform tf)
    //{
    //    List<UpdateWinkel> angles = new List<UpdateWinkel>();
    //    UpdateInfos layer = GetLayerinfo(tf);
    //    foreach(GameObject angleinfo in WinkelInfos.MenuItems)
    //    {
    //        UpdateWinkel angle = angleinfo.GetComponent<UpdateWinkel>();
    //        if(angle.transform1 == tf || angle.transform2 == tf)
    //        {
    //            angles.Add(angle);
    //        }
    //    }
    //    for (int i = 0; i < GeradenInfos.MenuItems.Count; i++)
    //    {
    //        UpdateInfos info = GeradenInfos.MenuItems[i].GetComponent<UpdateInfos>();
    //        if (info.Layertransform == tf)
    //        {
    //            SnapSchnittkante schnittkante = info.Layertransform.gameObject.GetComponent<SnapSchnittkante>();
    //            if (schnittkante != null)
    //            {
    //                foreach (GameObject angleinfo in WinkelInfos.MenuItems)
    //                {
    //                    UpdateWinkel angle = angleinfo.GetComponent<UpdateWinkel>();
    //                    if (angle.transform1 == schnittkante.layer1 && angle.transform2 == schnittkante.layer2)
    //                    {
    //                        angles.Add(angle);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return angles;
    //}

    void Update()
    {
        if (Geraden.isopen)
        {
            addbutton.SetStatus("Gerade");
        }
        else if (Ebenen.isopen)
        {
            addbutton.SetStatus("Ebene");
        }
        else if (Punkte.isopen)
        {
            addbutton.SetStatus("Punkt");
        }
        else
        {
            addbutton.SetStatus("");
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            VisibleComponent found = null;
            if (Physics.Raycast(ray,out hit))
            {
                Transform selectedObject = hit.transform;
                foreach (VisibleComponent visibleComponent in EditableComponents)
                {
                    if (visibleComponent.GetTransform() == selectedObject)
                    {
                        found = visibleComponent;
                        text.SetText("selected " + visibleComponent.Name);
                    }
                    if (visibleComponent.GetChildren().Exists((VirtualCompnonent obj) => (obj is VisibleComponent)&& ((VisibleComponent)obj).Model3D.transform == selectedObject))
                    {
                        found = (VisibleComponent)visibleComponent.GetChildren().Find((VirtualCompnonent obj) => (obj is VisibleComponent) && ((VisibleComponent)obj).Model3D.transform == selectedObject);
                        text.SetText("selected " + visibleComponent.Name);
                    }
                }
                //for (int i =0; i< EbenenInfos.MenuItems.Count; i++)
                //{
                //    GameObject ebene = EbenenInfos.MenuItems[i];
                //    UpdateInfos infos = ebene.GetComponent<UpdateInfos>();
                //    if (infos.Layertransform == selectedObject)
                //    {
                //        found = selectedObject;
                //        text.SetText("selected layer: " + infos.Name.text);
                //    }
                //    if (infos.Layertransform == selectedObject.parent)
                //    {
                //        found = selectedObject.parent;
                //        text.SetText("selected layer: " + infos.Name.text);
                //    }
                //}
                //for(int i = 0; i< GeradenInfos.MenuItems.Count; i++)
                //{
                //    GameObject gerade = GeradenInfos.MenuItems[i];
                //    UpdateInfos infos = gerade.GetComponent<UpdateInfos>();
                //    if (infos.Layertransform == selectedObject.parent)
                //    {
                //        found = selectedObject.parent;
                //        text.SetText("selected Gerade: " + infos.Name.text);
                //    }
                //}
                //for(int i = 0; i< PunkteInfos.MenuItems.Count; i++)
                //{
                //    GameObject punkt = PunkteInfos.MenuItems[i];
                //    UpdateInfos infos = punkt.GetComponent<UpdateInfos>();
                //    if (infos.Layertransform == selectedObject.parent)
                //    {
                //        found = selectedObject.parent;
                //        text.SetText("selected Punkt: " + infos.Name.text);
                //    }
                //}
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
                foreach (VisibleComponent compnonent in EditableComponents)
                {
                    foreach (VisibleComponent child in compnonent.GetChildren())
                    {
                        foreach (CanvasRenderer cr in child.InfoUI.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(1.0f);
                        }
                    }
                    foreach (CanvasRenderer cr in compnonent.InfoUI.GetComponentsInChildren<CanvasRenderer>())
                    {
                        cr.SetAlpha(1.0f);
                    }
                }
                //foreach (GameObject info in EbenenInfos.MenuItems)
                //{
                //    foreach(CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //    {
                //        cr.SetAlpha(1.0f);
                //    }
                //}
                //foreach (GameObject info in GeradenInfos.MenuItems)
                //{
                //    foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //    {
                //        cr.SetAlpha(1.0f);
                //    }
                //}
                //foreach (GameObject info in PunkteInfos.MenuItems)
                //{
                //    foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //    {
                //        cr.SetAlpha(1.0f);
                //    }
                //}
                //foreach (GameObject info in WinkelInfos.MenuItems)
                //{
                //    foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //    {
                //        cr.SetAlpha(1.0f);
                //    }
                //}
                text.SetText("selected nothing important");
            }
            else
            {
                List<VirtualCompnonent> fields=Findassociated(found);

                foreach (VirtualCompnonent compnonent in EditableComponents)
                {
                    foreach (VirtualCompnonent child in compnonent.GetChildren())
                    {
                        if (fields.Contains(child))
                        {
                            foreach (CanvasRenderer cr in child.InfoUI.GetComponentsInChildren<CanvasRenderer>())
                            {
                                cr.SetAlpha(1.0f);
                            }
                        }
                        else
                        {
                            foreach (CanvasRenderer cr in compnonent.InfoUI.GetComponentsInChildren<CanvasRenderer>())
                            {
                                cr.SetAlpha(0.0f);
                            }
                        }
                    }
                    if (fields.Contains(compnonent))
                    {
                        foreach (CanvasRenderer cr in compnonent.InfoUI.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(1.0f);
                        }
                    } else
                    {
                        foreach (CanvasRenderer cr in compnonent.InfoUI.GetComponentsInChildren<CanvasRenderer>())
                        {
                            cr.SetAlpha(0.0f);
                        }
                    }
                }
                //List<UpdateWinkel> angles = Findassociatedangles(found);
                //foreach (GameObject info in EbenenInfos.MenuItems)
                //{
                //    if (fields.Contains(info.GetComponent<UpdateInfos>()))
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(1.0f);
                //        }
                //    } else
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(0.0f);
                //        }
                //    }
                //}
                //foreach (GameObject info in GeradenInfos.MenuItems)
                //{
                //    if (fields.Contains(info.GetComponent<UpdateInfos>()))
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(1.0f);
                //        }
                //    }
                //    else
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(0.0f);
                //        }
                //    }
                //}
                //foreach (GameObject info in PunkteInfos.MenuItems)
                //{
                //    if (fields.Contains(info.GetComponent<UpdateInfos>()))
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(1.0f);
                //        }
                //    }
                //    else
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(0.0f);
                //        }
                //    }
                //}
                //foreach (GameObject info in WinkelInfos.MenuItems)
                //{
                //    if (angles.Contains(info.GetComponent<UpdateWinkel>()))
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(1.0f);
                //        }
                //    }
                //    else
                //    {
                //        foreach (CanvasRenderer cr in info.GetComponentsInChildren<CanvasRenderer>())
                //        {
                //            cr.SetAlpha(0.0f);
                //        }
                //    }
                //}
            }
        }
    }
}
