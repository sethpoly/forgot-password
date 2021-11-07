using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, -25f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    public void SetDescription(string description)
    {
        GetComponentInChildren<Text>().text = description;
    }

    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }
}
