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
        // Increase alpha of already set background sprite
        Color tempColor = button.background.color;
        tempColor.a += .2f;
        button.background.color = tempColor;
    }

    public override void OnTabExit(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTabSelected(TabButton button)
    {
        NavAction clickAction = button.GetComponent<NavButton>().clickAction;
        Display nextDisplay = Display.Minimized;

        switch(clickAction)
        {
            case NavAction.Minimize:
                nextDisplay = Display.Minimized;
                break;
            case NavAction.Fullscreen:
                nextDisplay = Display.TopMost; // TODO
                break;
            case NavAction.Close:
                nextDisplay = Display.Closed;
                break;
        }
        taskBarManager.SetWindowStateAndRefresh(GetWindow(), nextDisplay);
    }

    // Get the Window script component from its corresponding tab button
    private Window GetWindow()
    {
        return GetComponentInParent<Window>();
    }
}
