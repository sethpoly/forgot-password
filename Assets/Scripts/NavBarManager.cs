using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Top navigation bar of every window
// Minimize, close, maximize tab buttons
public class NavBarManager : TabGroup
{
    [SerializeField]
    private TaskBarManager taskBarManager;

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
        taskBarManager.SetWindowStateAndRefresh(GetWindow(button), Display.Minimized);
    }

    // Get the Window script component from its corresponding tab button
    private Window GetWindow(TabButton button)
    {
        return GetComponentInParent<Window>();
    }
}
