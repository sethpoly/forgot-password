using System.Collections.Generic;
using UnityEngine;

public abstract class TabGroup: MonoBehaviour
{
    public List<TabButton> tabButtons;

    public abstract void Subscribe(TabButton button);
    public abstract void OnTabEnter(TabButton button);
    public abstract void OnTabExit(TabButton button);
    public abstract void OnTabSelected(TabButton button);
}
