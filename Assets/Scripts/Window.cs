using UnityEngine;
using UnityEngine.UI;

public enum Display { Closed, TopMost, Open, Minimized  };


public class Window : MonoBehaviour
{

    [SerializeField]
    private Image image;

    [SerializeField]
    private Material testShader;

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


    private void TopMost()
    {
        Debug.Log("Setting window TopMost...");
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        // Testing
       // ApplyShader();
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
        //gameObject.SetActive(false);
    }
    
    // TODO:
    private void Closed()
    {
        Debug.Log("Window: Closed() -> Didn't implement...");
        DisableSelf();
    }

    private void DisableSelf()
    {
        Debug.Log("Disabling self...");
        gameObject.SetActive(false);
    }

    // Testing shaders
    private void ApplyShader()
    {
        image.material = testShader;
    }
}
