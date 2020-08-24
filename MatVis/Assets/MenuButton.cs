using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    internal ComponentsHandler Handler;
    internal Buttonlist Buttonlist;
    //public GameObject prefab3D;
    //public GameObject prefab2D;
    public List<GameObject> MenuItems;
    //public List<GameObject> Items3D;
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
            if (menuitem != null)
            {
                menuitem.SetActive(false);
            }
            else
            {
                MenuItems.Remove(menuitem);
            }
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
        //Buttonlist.Rearrange();
        int i = 0;
        int xdiff=0;
        if (Buttonlist.Vertical)
        {
            i = (int)-transform.localPosition.y;
        }
        else
        {
            xdiff = (int)-transform.localPosition.x;
        }
        foreach (GameObject MenuItem in MenuItems){
            if (Buttonlist.Vertical)
            {
                MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, i);
                MenuItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                //int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
                //float scalefactor = ((float)actwidth / 1440.0f);
                if (MenuItem.GetComponent<InfoMenuItem>() != null)
                {
                    if (MenuItem.GetComponent<InfoMenuItem>().hidden)
                    {
                        i += (int)(80);// * scalefactor);
                    }
                    else
                    {
                        i += (int)(MenuItem.GetComponent<RectTransform>().rect.height);// * scalefactor);
                    }
                }
                else
                {
                    i += (int)(MenuItem.GetComponent<RectTransform>().rect.height);// * scalefactor);
                }
                i += 10;
            }
            else
            {
                MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(xdiff, i-104);
                MenuItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                //int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
                //float scalefactor = ((float)actwidth / 1440.0f);
                i -= (int)(MenuItem.GetComponent<RectTransform>().rect.height);// *scalefactor);
                i -= 10;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
