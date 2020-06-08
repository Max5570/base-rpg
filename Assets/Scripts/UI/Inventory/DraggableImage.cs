using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour
{
    public GridBase gridToSwap;
    public GridBase gridPointerUp;

    public bool dragging { get; set; }
    public Vector2 offset { get; set; }

    private void Update() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            gameObject.SetActive(false);
            try
            {
                InventoryPanel.instance.SwapItems(gridToSwap, gridPointerUp);
            } catch {Debug.Log("err");}

            Tooltip.instance.status = Tooltip.Status.empty;
        }
    }
    private void FixedUpdate() 
    {   
        if (dragging)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            transform.localPosition = localPoint + offset;
        }
    }
}
