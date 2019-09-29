using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject prefab3D;
    public GameObject prefab2D;
    public List<GameObject> MenuItems;
    public List<GameObject> Items3D;
    public Transform pivot;
    public List<Menu> othermenus;
    public Vector2 Listoffset;
    public RawImage image;
    public bool isopen;
    public bool upwards;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void closeMenu()
    {
        foreach (GameObject menuitem in MenuItems)
        {
            menuitem.SetActive(false);
        }
        image.color = Color.white;
        isopen = false;
    }


    public void openMenu()
    {
        foreach (Menu menu in othermenus)
        {
            menu.closeMenu();
        }
        foreach (GameObject menuitem in MenuItems)
        {
            menuitem.SetActive(true);
        }
        image.color = Color.green;
        isopen = true;
    }
    public void clickbutton()
    {
        if (isopen)
        {
            closeMenu();
        }
        else
        {
            foreach (Menu menu in othermenus)
            {
                menu.closeMenu();
            }
            openMenu();
        }
    }
    public void rearrange()
    {
        int i = 0;
        foreach (GameObject MenuItem in MenuItems){
            if (upwards)
            {
                MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, i);
                i += (int)MenuItem.GetComponent<RectTransform>().rect.height;
                i += 10;
            }
            else
            {
                MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i-140);
                i -= (int)MenuItem.GetComponent<RectTransform>().rect.height;
                i -= 10;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
