using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Accessibility : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public string description;
    public GameObject toolTipPrefab;
    private GameObject toolTip;
    public Vector3 offset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(description);

        // Show accessibility popup with description
        toolTip = Instantiate(toolTipPrefab);
        ToolTip tt = toolTip.GetComponent<ToolTip>();
        tt.SetDescription(description);
        

        // Set tooltip offset if specified
        if(offset != Vector3.zero)
        {
            tt.SetOffset(offset);
        }

        toolTip.transform.SetParent(gameObject.transform);
    }

    public void OnPointerClick(PointerEventData eventData){}


    public void OnPointerExit(PointerEventData eventData)
    {
        // TODO: Hide popup after short delay
        Destroy(toolTip);
        Debug.Log("Exiting...");
    }
}
