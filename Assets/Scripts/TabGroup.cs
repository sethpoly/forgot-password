using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{

    public List<TabButton> tabButtons;
    public Sprite tabClosed;    // Tab was never opened
    public Sprite tabHover;
    public Sprite tabCurrent;   // Topmost tab
    public Sprite tabOpen;  // Tab can be minimized or not
    
    public TabButton currentTab;  // Topmost tab
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        //ResetTabs();
        //SetTabBackgrounds();

        // Increase alpha of already set background sprite
        Color tempColor = button.background.color;
        tempColor.a = .2f;
        button.background.color = tempColor;
            
            
       /* if (currentTab == null || button != currentTab)  
        {
            button.background.sprite = tabHover;
        }*/
    }

    public void OnTabExit(TabButton button)
    {
        //ResetTabs();
        //SetTabBackgrounds();
        Color tempColor = button.background.color;
        tempColor.a = .0f;
        button.background.color = tempColor;
    }

    public void OnTabSelected(TabButton button)
    {
        currentTab = button;
        ResetOtherTabs();
        //button.background.sprite = tabCurrent;


        // Just minimize/show on tab click - should not effect other tabs
        Window window = GetWindow(button);

        if (window.Display == Display.TopMost)
        {
            window.Display = Display.Minimized;
            SetNextTopMost(window);
        }
        else 
            window.Display = Display.TopMost;
    }

    // Set next window in stack to topmost
    private void SetNextTopMost(Window window)
    {
        GameObject windowContainer = window.transform.parent.gameObject;
        Window nextActiveWindow = null;
        int windowCount = windowContainer.transform.childCount;

        // Count active game objects in stack
        for (int i = 0; i < windowCount; i++)
        {
            Window nextWindowInStack = windowContainer.transform.GetChild(i).GetComponent<Window>();
            if (nextWindowInStack.isActiveAndEnabled)
            {
                nextActiveWindow = nextWindowInStack;
            }
        }

        if (nextActiveWindow != null)
        {
            if (nextActiveWindow.Display == Display.Open)
                nextActiveWindow.Display = Display.TopMost;
        }
    }

    public void ResetOtherTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (currentTab != null && button == currentTab) { continue;  }

            // Check if the other tabs are open and assign state accordingly
            Window window = GetWindow(button);
            switch (window.Display)
            {
                case Display.TopMost:
                    window.Display = Display.Open;
                    break;
                case Display.Open:
                    break;
                case Display.Minimized:
                    break;
                case Display.Closed:
                    break;
            }
        }
    }

    private void SetTabBackgrounds()
    {
        foreach (TabButton button in tabButtons)
        {
            Window window = GetWindow(button);

            // Set sprite of each button based on current button display
            switch (window.Display)
            {
                case Display.TopMost:
                    window.Display = Display.Open;
                    break;
                case Display.Open:
                    break;
                case Display.Minimized:
                    break;
                case Display.Closed:
                    //button.background.sprite = tabClosed;
                    ResetAlpha(button);
                    break;

            }
        }
    }

    private void ResetAlpha(TabButton button)
    {
        Color tempColor = button.background.color;
        tempColor.a = .0f;
        button.background.color = tempColor;
    }

    // Get the Window script component from its corresponding tab button
    private Window GetWindow(TabButton button)
    {
        int index = button.transform.GetSiblingIndex();
        return objectsToSwap[index].GetComponent<Window>();
    }
}
