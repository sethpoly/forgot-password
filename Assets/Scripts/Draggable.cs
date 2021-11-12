using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    private EventTrigger _eventTrigger;
    private Vector2 lastMousePosition;

    private void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
        _eventTrigger.AddEventTrigger(OnMouseDown, EventTriggerType.PointerClick);
    }


    public void OnMouseDown(BaseEventData data)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData)data;
        Vector2 currentMousePosition = ped.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
        rect.position = newPosition;

        lastMousePosition = currentMousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
