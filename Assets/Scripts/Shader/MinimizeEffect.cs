using System;
using System.Collections;
using UnityEngine;

public class MinimizeEffect : LinearShaderEffect
{

    protected new float effectTime = 7.5f;
    protected float startAmount = 1f;
    protected float endAmount = 0f;

    private void Awake()
    {
        effectAmount = startAmount;
    }

    protected override IEnumerator EffectAction(Action<bool> onCompletion)
    {
            while (true)
            {
                Debug.Log("Effect coroutine re-running...");
                if (!ActionComplete())
                {
                    effectAmount = Mathf.Clamp(effectAmount - Time.deltaTime * effectTime, endAmount, startAmount);
                    shaderMaterial.SetFloat(shaderConstant, effectAmount);
                }
                else
                {
                    onCompletion(true);
                    break;
                }

                Debug.Log("Effect amount: " + effectAmount);
                yield return null;
            }
        
    }

    protected override bool ActionComplete()
    {
        if (effectAmount <= endAmount)
        {
            Debug.Log("Shader action: " + shaderConstant + " complete -> Do something...");

            // Apply normal materials back & reset variables
            ApplyShader(originalMaterial);
            effectAmount = startAmount;
            return true;
        }
        return false;
    }


}

