using System.Collections.Generic;
using UnityEngine;

public class TaskBarManager : TabGroup
{

    private static TaskBarManager _instance;
    public static TaskBarManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public TTabButton currentTab;  // Topmost tab
    public List<GameObject> windows;
    public GameObject windowContainer;
    
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
        UpdateTabBackgrounds();

        // Increase alpha of already set background sprite
        Color tempColor = button.background.color;
        tempColor.a += .2f;
        button.background.color = tempColor;
    }

    public override void OnTabExit(TTabButton button)
    {
        UpdateTabBackgrounds();
    }

    public override void OnTabSelected(TTabButton button)
    {
        currentTab = button;

        // Just minimize/show on tab click
        Window window = GetWindow(button);
        Display newWindowState;

        if (window.Display == Display.TopMost)
            newWindowState = Display.Minimized;
        else
            newWindowState = Display.TopMost;

        SetWindowStateAndRefresh(window, newWindowState);
    }

    // Set window to requested display, set the next topmost, and update backgrounds
    public void SetWindowStateAndRefresh(Window window, Display display)
    {
        window.SetDisplay(display, (onCompletion) => {
            SetNextTopMost();
            UpdateTabBackgrounds();
        });
    }

    // Set next window in stack to topmost
    private void SetNextTopMost()
    {
        List<Window> activeWindows = new List<Window>();
        int windowCount = windowContainer.transform.childCount;

        // Set all active windows to Display.Open
        for (int i = 0; i < windowCount; i++)
        {
            Window nextWindowInStack = windowContainer.transform.GetChild(i).GetComponent<Window>();
            if (nextWindowInStack != null && nextWindowInStack.isActiveAndEnabled)
            {
                nextWindowInStack.SetDisplay(Display.Open, (onCompletion => { }));
                activeWindows.Add(nextWindowInStack);
            }
        }

        // Set most topmost window to Display.TopMost 
        if (activeWindows.Count > 0 && activeWindows[activeWindows.Count - 1].Display != Display.TopMost)
        {
            activeWindows[activeWindows.Count - 1].SetDisplay(Display.TopMost, (onCompletion => { }));
        }
    }

    private void UpdateTabBackgrounds()
    {
        foreach (TTabButton button in tabButtons)
        {
            Window window = GetWindow(button);
            ResetAlpha(button, window.Display);
        }
    }

    private void ResetAlpha(TTabButton button, Display display)
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
    private Window GetWindow(TTabButton button)
    {
        int index = button.transform.GetSiblingIndex();
        return windows[index].GetComponent<Window>();
    }
}
