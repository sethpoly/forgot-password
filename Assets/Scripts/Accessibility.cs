using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Accessibility : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(description);

        // TODO: Show accessibility popup with description
        // use parent gameobject as reference to where to display popup
    }

    public void OnPointerClick(PointerEventData eventData){}


    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();

        // TODO: Hide popup after short delay
    }
}
