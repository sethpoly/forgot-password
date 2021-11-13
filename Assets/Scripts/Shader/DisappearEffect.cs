using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisappearEffect : MonoBehaviour
{
    [SerializeField] 
    private Material dissolveMaterial;

    [SerializeField]
    private Material originalMaterial;

    private float dissolveTime = 2f;
    private float dissolveAmount;

    // Begin disappearing 
    public void StartDisappearring(System.Action<bool> onCompletion)
    {
        ApplyShader(dissolveMaterial);

        StartCoroutine(Disappear((result) => {
            onCompletion(result);
        }));
    }

    public IEnumerator Disappear(System.Action<bool> onCompletion)
    {
        while (true)
        {
            Debug.Log("Disappear coroutine re-running...");
            if (!FullyDisappeared())
            {
                dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime * dissolveTime);
                dissolveMaterial.SetFloat("_DissolveSlider", dissolveAmount);
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
    private bool FullyDisappeared() 
    { 
        if(dissolveAmount >= .99f)
        {
            Debug.Log("Fully dissolved -> Do something...");

            // Apply normal materials back & reset variables
            ApplyShader(originalMaterial);
            dissolveAmount = 0f;
            return true;
        }
        return false; 
    }


    // Apply shader material to all children of component
    private void ApplyShader(Material material)
    {
        Image[] images;
        images = GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            i.material = material;
        }
    }
}
