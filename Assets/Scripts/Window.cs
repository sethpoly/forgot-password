using UnityEngine;
using UnityEngine.UI;

public enum Display { Closed, TopMost, Open, Minimized  };


public class Window : MonoBehaviour
{
    // Temporary reference to all shader materials
    // Should mvoe this somewhere else, ideally use a FSM
    [SerializeField]
    private Material dissolveMaterial;
    [SerializeField]
    private Material minimizeMaterial;
    [SerializeField]
    private Material originalMaterial;

    // Tween helpers
    private Vector2 startPosition;
    private Vector3 startScale = Vector3.one;
    private Tweenable tweenable;

    public Display Display { get { return _display; } }

    [SerializeField]
    private Display _display;

    private void Start()
    {
        // Save initial start position so we can reset it later
        startPosition = transform.position;

        // Tweening helper function
        tweenable = GetComponent<Tweenable>();
    }

    // Sets current display of window
    // onCompletion callback when window transition is complete and we are ready to re-sort the window stack 
    public void SetDisplay(Display display, System.Action<bool> onCompletion)
    {
        _display = display;

        switch (_display)
        {
            case Display.Closed:
                Closed((complete) => { onCompletion(complete); });
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
                Minimized((complete) => { onCompletion(complete); });
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

    private void Minimized(System.Action<bool> onCompletion)
    {
        Debug.Log("Setting window Minimized...");

        tweenable.MinimizeTween((completion) =>
        {
            DisableSelf();
            ResetScale();
            onCompletion(true);
        });
    }
    
    private void Closed(System.Action<bool> onCompletion)
    {
        Debug.Log("Setting window Closed...");

        LinearShaderEffect effect = gameObject.AddComponent<LinearShaderEffect>();
        ApplyShader(effect, dissolveMaterial, ShaderConstants.dissolve, (completion) => {
            DisableSelf();
            ResetPosition();
            //onCompletion(true);
        });

        // Test transparentizing text elements
/*        Text[] text;
        text = GetComponentsInChildren<Text>();*/
        TransparentizeText textTransformer = gameObject.AddComponent<TransparentizeText>();
        textTransformer.Transparentize(gameObject, (completion) =>
        {
            Debug.Log("Text has been transparentized...");
            onCompletion(true);
            });
/*        foreach (Text t in text)
        {
            textTransformer.Transparentize(t, (completion) =>
            {
                Debug.Log("Text has been transparentized...");
                Color c = t.color;
*//*                c.a = 1f;
                t.material.color = c;*//*
            });
        }*/
    }

    private void DisableSelf()
    {
        Debug.Log("Disabling window " + this.name);
        gameObject.SetActive(false);
    }

    private void ApplyShader(LinearShaderEffect effect, Material shaderMaterial, string shaderConstant, System.Action<bool> onCompletion)
    {
        Material newShaderMaterial = new Material(shaderMaterial);
        effect.C(newShaderMaterial, originalMaterial, shaderConstant);
        effect.BeginEffect((complete) =>
        {
            Debug.Log("Shader effect complete -> " + shaderConstant);
            onCompletion(true);
        });
    }

    private void ResetScale()
    {
        LeanTween.scale(gameObject, startScale, 0f);
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }
}
