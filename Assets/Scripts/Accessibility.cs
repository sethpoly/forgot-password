using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


/*
 * Creates a popup tooltip over hovered gameobject
 * Can set offset and tooltip text
 */
public class Accessibility : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public string description;
    public GameObject toolTipPrefab;
    private GameObject toolTip;
    public Vector3 offset;

    private ToolTip ttScript = null;
    private float seconds = .5f;
    private bool hovering = false;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;

        // Show tooltip popup after delay
        StartCoroutine(CreateToolTip(description, offset));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hovering = false;

        DestroyToolTip();
    }

    // Destroy tooltip when pointer exits
    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;

        DestroyToolTip(); 
    }

    IEnumerator CreateToolTip(string description, Vector3 offset)
    {
        yield return new WaitForSeconds(seconds);

        // Only create if cursor is still hovering
        if (hovering && ttScript == null)
        {
            Debug.Log("Created tooltip..");

            toolTip = Instantiate(toolTipPrefab);
            ttScript = toolTip.GetComponent<ToolTip>();

            // Set tooltip text
            ttScript.SetDescription(description);


            // Set tooltip offset if specified
            if (offset != Vector3.zero)
            {
                ttScript.SetOffset(offset);
            }

            // Set transform in correct hierarchy
            toolTip.transform.SetParent(gameObject.transform);
        } 
    }

    public void DestroyToolTip()
    {
        Destroy(toolTip);
        ttScript = null;
    }
}
