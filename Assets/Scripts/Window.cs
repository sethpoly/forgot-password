using UnityEngine;

public enum Display { Closed, TopMost, Open, Minimized  };


public class Window : MonoBehaviour
{
    
    public Display Display
    {
        get { return _display; }
        set
        {
            _display = value;

            switch (_display)
            {
                case Display.Closed:
                    Closed();
                    break;
                case Display.TopMost:
                    TopMost();
                    break;
                case Display.Open:
                    Open();
                    break;
                case Display.Minimized:
                    Minimized();
                    break;
            }
        }
    }
    private Display _display;

    private void TopMost()
    {
        Debug.Log("Setting window TopMost...");
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    // TODO: 
    private void Open()
    {
        Debug.Log("Window: Open() -> Didn't implement...");
    }

    private void Minimized()
    {
        Debug.Log("Setting window Minimized...");
        gameObject.SetActive(false);
    }
    
    // TODO:
    private void Closed()
    {
        Debug.Log("Window: Closed() -> Didn't implement...");
        gameObject.SetActive(false);
    }
}
