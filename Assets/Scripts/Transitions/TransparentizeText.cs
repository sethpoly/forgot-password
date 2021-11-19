using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentizeText : MonoBehaviour
{
    private float effectTime = 2f;

    public void Transparentize(GameObject obj, System.Action<bool> onCompletion)
    {
        int count = 0;
        Text[] textElements = obj.GetComponentsInChildren<Text>();

        foreach (Text text in textElements)
        {
            StartCoroutine(EffectRoutine(text, (result) =>
            {
                count++;
                Debug.Log("on transparent result -> count: " + count + " length: " + textElements.Length);
                
                if (count == textElements.Length)
                {
                    Debug.Log("Finished transparentizing all text...");
                    onCompletion(result);     
                }
            }));
        }
    }

    private IEnumerator EffectRoutine(Text text, System.Action<bool> onCompletion)
    {
        while (true)
        {
            Color c = text.color;
            Debug.Log("Current text alpha: " + c.a);

            if (!ActionComplete(text))
            {
                c.a = Mathf.Clamp01(c.a - Time.deltaTime * effectTime);
                text.color = c;
            }
            else
            {
                Debug.Log("Transparentize routine finished...");
                onCompletion(true);
                break;
            }

            yield return null;
        }
    }

    private bool ActionComplete(Text text)
    {
        if (text.color.a <= 0)
        {
            // Set color alpha to 0f
            Color c = text.color;
            //c.a = 1f;
            //text.color = c;
            
            // Apply normal materials back & reset variables
/*            Color c = text.material.color;
            c.a = 1f;
            text.material.color = c;*/
            return true;
        }
        return false;
    }
}
