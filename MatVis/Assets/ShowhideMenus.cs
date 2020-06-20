using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowhideMenus : MonoBehaviour
{
    public RawImage open;
    public RawImage close;
    public bool isopen = true;
    public List<MenuButton> menus;
    public List<bool> wasopen;

    public void closemenus()
    {
        foreach(MenuButton menu in menus)
        {
            wasopen[menus.IndexOf(menu)] = menu.isopen;
            menu.CloseMenu();
        }
    }
    public void openmenus()
    {
        foreach (MenuButton menu in menus)
        {
            if (wasopen[menus.IndexOf(menu)])
            {
                menu.OpenMenu();
            }
        }
    }
    public void Clickbutton()
    {
        if (isopen)
        {
            closemenus();
            open.enabled = false;
            close.enabled = true;
        }
        else
        {
            openmenus();
            open.enabled = true;
            close.enabled = false;
        }
        isopen = !isopen;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
