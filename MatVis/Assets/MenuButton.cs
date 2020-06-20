using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    internal ComponentsHandler Handler;
    internal Buttonlist Buttonlist;
    //public GameObject prefab3D;
    //public GameObject prefab2D;
    public List<GameObject> MenuItems;
    public List<GameObject> Items3D;
    //public Transform pivot;
    //public List<Menu> othermenus;
    //public Vector2 Listoffset;
    public RawImage image;
    public bool isopen;
    //public bool upwards;
    //public TextMeshProUGUI MenuName;
    // Start is called before the first frame update
    public void StartFromCH()
    {
        Handler = GetComponentInParent<ComponentsHandler>();
        Buttonlist = GetComponentInParent<Buttonlist>();
    }

    public void CloseMenu()
    {
        foreach (GameObject menuitem in MenuItems)
        {
            menuitem.SetActive(false);
        }
        //gameObject.SetActive(false);
        image.color = Color.white;
        isopen = false;
        //MenuName.text = "";
    }


    public void OpenMenu()
    {
        foreach (GameObject menuitem in MenuItems)
        {
            menuitem.SetActive(true);
        }
        //gameObject.SetActive(false);
        image.color = Color.green;
        isopen = true;
        //MenuName.text = name;
    }
    public void Clickbutton()
    {
        if (isopen)
        {
            Buttonlist.CloseMe(this);
        }
        else
        {
            Buttonlist.OpenMe(this);
        }
    }
    public void Rearrange()
    {
        Buttonlist.Rearrange();
        //int i = 0;
        //foreach (GameObject MenuItem in MenuItems){
        //    if (upwards)
        //    {
        //        MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, i);
        //        int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        //        float scalefactor = ((float)actwidth / 1440.0f);
        //        i += (int)(MenuItem.GetComponent<RectTransform>().rect.height*scalefactor);
        //        i += 10;
        //    }
        //    else
        //    {
        //        MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i-104);
        //        int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        //        float scalefactor = ((float)actwidth / 1440.0f);
        //        i -= (int)(MenuItem.GetComponent<RectTransform>().rect.height*scalefactor);
        //        i -= 10;
        //    }
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
