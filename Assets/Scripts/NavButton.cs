using UnityEngine;

public enum NavAction {
    Minimize, Fullscreen, Close
}

public class NavButton : TTabButton
{
    [SerializeField]
    public NavAction clickAction; 
}
