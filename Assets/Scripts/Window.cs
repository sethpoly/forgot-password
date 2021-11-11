using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Display _display;

    // Determine if in mid-state change transition
    // Don't include in SetNextTopmost while transitioning
    // TODO: Make private set
    public bool transitioning = false;

    private void TopMost()
    {
        Debug.Log("Setting window TopMost...");
        transitioning = true;
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        transitioning = false;
    }

    // TODO: 
    private void Open()
    {
        Debug.Log("Window: Open() -> Didn't implement...");
        transitioning = true;
        transitioning = false;
    }

    private void Minimized()
    {
        Debug.Log("Setting window Minimized...");
        transitioning = true;
        DisableSelf();
    }
    
    // TODO:
    private void Closed()
    {
        Debug.Log("Setting window Closed...");
        transitioning = true;

        // Disappear effect
        GetComponent<DisappearEffect>().StartDisappearring((onCompletion) =>
        {
            Debug.Log("Disappearing finished -> " + onCompletion);
            DisableSelf();
        });
    }

    private void DisableSelf()
    {
        Debug.Log("Disabling window " + this.name);
        gameObject.SetActive(false);
        transitioning = false;
    }
}
