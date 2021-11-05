using UnityEngine;

public enum NavAction {
    Minimize, Fullscreen, Close
}

public class NavButton : TabButton
{
    [SerializeField]
    public NavAction clickAction; 
}
