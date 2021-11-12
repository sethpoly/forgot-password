using UnityEngine;
using UnityEngine.UI;

public enum Display { Closed, TopMost, Open, Minimized  };


public class Window : MonoBehaviour
{
    private Vector2 startPosition;
    public Display Display { get { return _display; } }

    [SerializeField]
    private Display _display;

    private void Start()
    {
        // Save initial start position so we can reset it later
        startPosition = transform.position;
    }

    // Sets current display of window
    // onCompletion callback when window transition is complete and we are ready to re-sort the window stack 
    public void SetDisplay(Display display, System.Action<bool> onCompletion)
    {
        _display = display;

        switch (_display)
        {
            case Display.Closed:
                Closed((closingComplete) => { onCompletion(closingComplete); });
                break;
            case Display.TopMost:
                TopMost();
                // TODO:
                onCompletion(true);
                break;
            case Display.Open:
                Open();
                // TODO:
                onCompletion(true);
                break;
            case Display.Minimized:
                Minimized();
                // TODO:
                onCompletion(true);
                break;
        }
    }

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
        DisableSelf();
    }
    
    // TODO:
    private void Closed(System.Action<bool> onCompletion)
    {
        Debug.Log("Setting window Closed...");

        // Disappear effect
        GetComponent<DisappearEffect>().StartDisappearring((hasDisappeared) =>
        {
            Debug.Log("Disappearing finished -> " + hasDisappeared);

            // Disable gameobject and reset position to origin
            DisableSelf();
            ResetPosition();
            onCompletion(true);
        });
    }

    private void DisableSelf()
    {
        Debug.Log("Disabling window " + this.name);
        gameObject.SetActive(false);
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }
}
