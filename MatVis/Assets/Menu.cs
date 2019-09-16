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
    public RawImage image;
    public bool isopen;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
