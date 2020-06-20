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

    internal void Rearrange()
    {
        int i = 0;
        foreach (MenuButton MenuItem in items)
        {
            if (Vertical)
            {
                MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, i);
                int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
                float scalefactor = ((float)actwidth / 1440.0f);
                i += (int)(MenuItem.GetComponent<RectTransform>().rect.height * scalefactor);
                i += 10;
            }
            else
            {
                MenuItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i - 104);
                int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
                float scalefactor = ((float)actwidth / 1440.0f);
                i -= (int)(MenuItem.GetComponent<RectTransform>().rect.height * scalefactor);
                i -= 10;
            }
        }
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
