using System.Collections.Generic;
using UnityEngine;

public abstract class TabGroup: MonoBehaviour
{
    public List<TTabButton> tabButtons;

    public abstract void Subscribe(TTabButton button);
    public abstract void OnTabEnter(TTabButton button);
    public abstract void OnTabExit(TTabButton button);
    public abstract void OnTabSelected(TTabButton button);
}
