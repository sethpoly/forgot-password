using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Top navigation bar of every window
// Minimize, close, maximize tab buttons
public class NavigationBar : MonoBehaviour, ITabGroup
{

    public List<TabButton> tabButtons;

    public void OnTabEnter(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    public void OnTabExit(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    public void OnTabSelected(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    public void Subscribe(TabButton button)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
   
}
