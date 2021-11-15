using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used on windows so I can animate them closing, opening, and basically whatever I want
 * Set of helper functions to animate
 **/
public class Tweenable : MonoBehaviour
{
    public void MinimizeTween(System.Action<object> onComplete)
    {
        float speed = .1f;
        LeanTween.scaleY(gameObject, .05f, speed).setOnComplete((completion) =>
        {
            LeanTween.scaleX(gameObject, 0, speed).setOnComplete(onComplete);
        });
    }

    public void MinimizeTweenRotate(System.Action<object> onComplete)
    {
        float speed = .1f;
        float rotateSpeed = .3f;
        LeanTween.rotateAround(gameObject, Vector3.forward, 360.0f, rotateSpeed).setOnComplete((completion) => {
            LeanTween.scaleY(gameObject, .05f, speed).setDelay(.1f).setOnComplete((completion) =>
            {
                LeanTween.scaleX(gameObject, 0.0f, speed).setDelay(.05f).setOnComplete(onComplete);
            });
        });
    }
}
