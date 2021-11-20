using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Top navigation bar of every window
// Minimize, close, maximize tab buttons
public class NavBarManager : TabGroup
{
    public override void Subscribe(TTabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TTabButton>();
        }

        tabButtons.Add(button);
    }

    public override void OnTabEnter(TTabButton button)
    {
        // Increase alpha of already set background sprite
        Color tempColor = button.background.color;
        tempColor.a += .2f;
        button.background.color = tempColor;
    }

    public override void OnTabExit(TTabButton button)
    {
        //qthrow new System.NotImplementedException();
    }

    public override void OnTabSelected(TTabButton button)
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
        TaskBarManager.Instance.SetWindowStateAndRefresh(GetWindow(), nextDisplay);
    }

    // Get the Window script component from its corresponding tab button
    private Window GetWindow()
    {
        return GetComponentInParent<Window>();
    }
}
