using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Image))]
public class TTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        Debug.Log(name + ": Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(name + ": Enter");
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(name + ": Exit");
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }
}
