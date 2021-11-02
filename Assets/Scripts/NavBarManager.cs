using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Top navigation bar of every window
// Minimize, close, maximize tab buttons
public class NavBarManager : TabGroup
{

    public override void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public override void OnTabEnter(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTabExit(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTabSelected(TabButton button)
    {
        throw new System.NotImplementedException();
    }
}
