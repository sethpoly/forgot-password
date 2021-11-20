using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentizeText : MonoBehaviour
{
    private float effectTime = 2.25f;
    private GameObject obj;
    private int offset = 0;


    private void BeforeRoutineStart(GameObject obj)
    {
        this.obj = obj;
    }

    public void Transparentize(GameObject obj, System.Action<bool> onCompletion)
    {
        BeforeRoutineStart(obj);
        int count = 0;
        if(ToolTipExists()) { offset++; }
        Debug.Log("ToolTipExists? -> " + ToolTipExists());

        Text[] textElements = obj.GetComponentsInChildren<Text>();

        foreach (Text text in textElements)
        {
            StartCoroutine(EffectRoutine(text, (result) =>
            {
                count++;
                Debug.Log("on transparent result -> count: " + count + " length: " + textElements.Length);

                if (count == textElements.Length - offset)
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
            return true;
        }
        return false;
    }

    public void ResetTextAlpha()
    {
        Text[] textElements = obj.GetComponentsInChildren<Text>();
        Debug.Log("Text count : " + textElements.Length);

        foreach (Text text in textElements)
        {
            Color c = text.color;
            c.a = 1f;
            text.color = c;
        }
    }
    public bool ToolTipExists()
    {
        ToolTip toolTip = GetComponentInChildren<ToolTip>();
        if (toolTip != null) 
        {
            return true;
        }
        return false;
    }
}
