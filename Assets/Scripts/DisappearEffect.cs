using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearEffect : MonoBehaviour
{
    [SerializeField] 
    private Material material;

    private float dissolveTime = 2f;

    private float dissolveAmount;
    private bool isDissolving;

/*    void Update()
    {
        if(isDissolving)
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime * dissolveTime);

        material.SetFloat("_DissolveSlider", dissolveAmount);

        FullyDisappeared();
    }*/

    // Begin disappearing 
    public void StartDisappearring(System.Action<bool> onCompletion)
    {
        ApplyShader();

        StartCoroutine(Disappear((result) => {
            onCompletion(result);
        }));

        //if(!isDissolving)
        //{
          //  ApplyShader();
            //isDissolving = true;
        //}
    }

    public IEnumerator Disappear(System.Action<bool> onCompletion)
    {
        while (true)
        {
            Debug.Log("Disappear coroutine re-running...");
            if (!FullyDisappeared())
            {
                dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime * dissolveTime);
                material.SetFloat("_DissolveSlider", dissolveAmount);
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
            return true;
        }
        return false; 
    }


    // Apply shader material to all children of component
    private void ApplyShader()
    {
        Image[] images;
        images = GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            i.material = material;
        }
    }
}
