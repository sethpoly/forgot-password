using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    public GameObject Target;
    private EventTrigger _eventTrigger;
    private Vector2 lastMousePosition;

    public void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
        _eventTrigger.AddEventTrigger(OnMouseDown, EventTriggerType.PointerClick);

    }

    public void OnMouseDown(BaseEventData data)
    {
        StackOnTop();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        lastMousePosition = eventData.position;
        StackOnTop();
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData)data;
        Vector2 currentMousePosition = ped.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {

    }

    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }

    // Stack window at topmost layer
    private void StackOnTop()
    {
        transform.SetAsLastSibling(); 
    }
}