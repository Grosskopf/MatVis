using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buttonlist : MonoBehaviour
{
    private List<MenuButton> items;
    private TextMeshProUGUI Label;
    public int ActiveItem=-1;
    public bool Vertical = false;
    // Start is called before the first frame update
    public void StartFromCH()
    {
        Label = GetComponentInChildren<TextMeshProUGUI>();
        items = new List<MenuButton>(GetComponentsInChildren<MenuButton>());
        foreach (MenuButton item in items)
        {
            item.StartFromCH();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void CloseMe(MenuButton menu)
    {
        ActiveItem = -1;
        menu.CloseMenu();
        Label.text = "";
    }


    internal MenuButton GetMenu(string v)
    {
        MenuButton result= items.Find((obj) => obj.name == v);
        return result;
    }

    internal void OpenMe(MenuButton menu)
    {
        if(ActiveItem != -1)
        {
            items[ActiveItem].CloseMenu();
        }
        ActiveItem = items.FindIndex(r => r.transform == menu.transform);
        menu.OpenMenu();
        Label.text = menu.name;
    }
}
