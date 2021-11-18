using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentizeText : MonoBehaviour
{
    private float effectTime = 2f;

    public void Transparentize(Text text, System.Action<bool> onCompletion)
    {
        StartCoroutine(EffectRoutine(text, (result) => {
            onCompletion(result);
        }));
    }

    private IEnumerator EffectRoutine(Text text, System.Action<bool> onCompletion)
    {
        while (true)
        {
            onCompletion(true);
            break;
            Color c = text.material.color;

            if (!ActionComplete(text))
            {
                c.a = Mathf.Clamp01(c.a - Time.deltaTime * effectTime);
                text.material.color = c;
            }
            else
            {
                onCompletion(true);
                break;
            }

            yield return null;
        }
    }

    private bool ActionComplete(Text text)
    {
        if (text.material.color.a <= 0)
        {
            // Apply normal materials back & reset variables
/*            Color c = text.material.color;
            c.a = 1f;
            text.material.color = c;*/
            return true;
        }
        return false;
    }
}
