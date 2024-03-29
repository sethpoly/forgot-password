using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Name of shader constants that we need to manipulate 
public static class ShaderConstants {
    public static readonly string dissolve = "_DissolveSlider";
    public static readonly string minimize = "_ScaleSlider";
}

public class LinearShaderEffect : MonoBehaviour
{
    [SerializeField]
    protected Material shaderMaterial;

    [SerializeField]
    protected Material originalMaterial;

    protected string shaderConstant;
    protected float effectAmount;

    // Non-param attributes
    protected float effectTime = 2f;

    // Custom constructor because unity is stupid and won't let us program like normal coders 
    public void C(Material shaderMaterial, Material originalMaterial, string shaderConstant)
    {
        this.shaderMaterial = shaderMaterial;
        this.originalMaterial = originalMaterial;
        this.shaderConstant = shaderConstant;
    }

    // Begin disappearing 
    public void BeginEffect(System.Action<bool> onCompletion)
    {
        ApplyShader(shaderMaterial);

        StartCoroutine(EffectAction((result) => {
            onCompletion(result);
        }));
    }

    protected virtual IEnumerator EffectAction(System.Action<bool> onCompletion)
    {
        while (true)
        {
            if (!ActionComplete())
            {
                effectAmount = Mathf.Clamp01(effectAmount + Time.deltaTime * effectTime);
                shaderMaterial.SetFloat(shaderConstant, effectAmount);
            }
            else
            {
                onCompletion(true);
                break;
            }

            yield return null;
        }
    }

    // If full dissolved, 
    protected virtual bool ActionComplete()
    {
       if (effectAmount >= .99f)
        {
            Debug.Log("Shader action: " + shaderConstant +" complete -> Do something...");

            effectAmount = 0f;
            return true;
        }
        return false;
    }


    // Apply shader material to all children of component
    public void ApplyShader(Material material)
    {
        Image[] images;
        images = GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            i.material = material;
        }
    }
}
