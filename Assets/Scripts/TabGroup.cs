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
        UpdateTabStates();

        // Increase alpha of already set background sprite
        Color tempColor = button.background.color;
        tempColor.a += .2f;
        button.background.color = tempColor;
    }

    public void OnTabExit(TabButton button)
    {
        UpdateTabStates();
    }

    public void OnTabSelected(TabButton button)
    {
        currentTab = button;

        // Just minimize/show on tab click - should not effect other tabs
        Window window = GetWindow(button);

        if (window.Display == Display.TopMost)
        {
            window.Display = Display.Minimized;
        }
        else 
            window.Display = Display.TopMost;

        SetNextTopMost(window);
        UpdateTabStates();
    }

    // Set next window in stack to topmost
    private void SetNextTopMost(Window window)
    {
        GameObject windowContainer = window.transform.parent.gameObject;
        //int windowCount = windowContainer.transform.childCount;
        int windowCount = windowContainer.transform.childCount; //GetComponentsInChildren<Transform>().GetLength(0);
        List<Window> activeWindows = new List<Window>();

        // Count active game objects in stack
        for (int i = 0; i < windowCount; i++)
        {
            Window nextWindowInStack = windowContainer.transform.GetChild(i).GetComponent<Window>();
            if (nextWindowInStack.isActiveAndEnabled && nextWindowInStack != null)
            {

                nextWindowInStack.Display = Display.Open;
                activeWindows.Add(nextWindowInStack);
            }

            /*if (nextWindowInStack.isActiveAndEnabled && nextWindowInStack != null)
            {

                Debug.Log("Next window in stack : " + nextWindowInStack.Display);
                // !null -> if not the last element, set to Open, else topmost
                if (nextWindowInStack.Display == Display.Open)
                {
                    nextWindowInStack.Display = Display.TopMost;
                    Debug.Log("Setting a window to next topmost... i = " + i);
                }
               // else if (nextWindowInStack.Display == Display.TopMost)
                 //   nextWindowInStack.Display = Display.Open;
            }*/
        }

        // Set the corresponding button to active
        if (activeWindows.Count > 0)
        {
            activeWindows[activeWindows.Count - 1].Display = Display.TopMost;
        }

        Debug.Log("Active windows count: " + activeWindows.Count);
    }

    public void UpdateTabStates()
    {
        foreach (TabButton button in tabButtons)
        {
            //if (currentTab != null && button == currentTab) { continue;  }

            // Check if the other tabs are open and assign state accordingly
            Window window = GetWindow(button);
            switch (window.Display)
            {
                case Display.TopMost:
                    currentTab = button;
                    //window.Display = Display.Open;
                    break;
                case Display.Open:
                    break;
                case Display.Minimized:
                    break;
                case Display.Closed:
                    break;
            }
        }
        UpdateTabBackgrounds();
    }

    public void UpdateTabBackgrounds()
    {
        foreach (TabButton button in tabButtons)
        {
            Window window = GetWindow(button);
            ResetAlpha(button, window.Display);
        }
    }

    private void ResetAlpha(TabButton button, Display display)
    {
        Color tempColor = button.background.color;
        float newAlpha = .0f;

        switch (display)
        {
            case Display.TopMost:
                newAlpha = .45f;
                break;
            case Display.Open:
                newAlpha = .2f;
                break;
            case Display.Minimized:
                newAlpha = .2f;
                break;
            case Display.Closed:
                newAlpha = .0f;
                break;
        }
        tempColor.a = newAlpha;
        button.background.color = tempColor;
    }

    // Get the Window script component from its corresponding tab button
    private Window GetWindow(TabButton button)
    {
        int index = button.transform.GetSiblingIndex();
        return objectsToSwap[index].GetComponent<Window>();
    }
}
