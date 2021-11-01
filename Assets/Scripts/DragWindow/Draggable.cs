using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    public GameObject Target;
    private EventTrigger _eventTrigger;
    private Vector2 lastMousePosition;
    private Window window;

    [SerializeField]
    private WindowTabManager windowManager;

    [SerializeField]
    private GameObject windowContainer;

    private Rect containerRect;

    public void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
        _eventTrigger.AddEventTrigger(OnMouseDown, EventTriggerType.PointerClick);
        window = GetComponent<Window>();

        containerRect = windowContainer.GetComponent<RectTransform>().rect;
    }

    public void OnMouseDown(BaseEventData data)
    {
        windowManager.SetWindowStateAndRefresh(window, Display.TopMost);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        lastMousePosition = eventData.position;

        windowManager.SetWindowStateAndRefresh(window, Display.TopMost);
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
        float tabHeight = Screen.height - containerRect.height;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, tabHeight, containerRect.width, containerRect.height);
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
}