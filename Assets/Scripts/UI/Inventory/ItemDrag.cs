using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public DraggableImage draggableImage;
    public GridBase dragGrid;

    private void Awake() 
    {
        if (draggableImage == null)
        {
            draggableImage = GameObject.FindObjectOfType<DraggableImage>();
            dragGrid = transform.parent.GetComponent<GridBase>();
        }
    }

    private void Start()
    {
        draggableImage.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (dragGrid.item == null || string.IsNullOrEmpty(dragGrid.item.itemName))
        {
            return;
        }
        if(eventData.pointerId == -1)
        {
            draggableImage.gameObject.SetActive(true);
            Tooltip.instance.Hide();
            Tooltip.instance.status = Tooltip.Status.dragItem;
            draggableImage.transform.position = transform.position;
            draggableImage.offset = GetScreenToLocalPoint(transform.position) - GetScreenToLocalPoint(Input.mousePosition);
            draggableImage.dragging = true;
            draggableImage.gridToSwap = transform.parent.GetComponent<GridBase>();
            draggableImage.GetComponent<Image>().sprite = transform.parent.GetComponent<GridBase>().image.sprite;
        }
    }

    private Vector2 GetScreenToLocalPoint(Vector2 point)
    {
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), point, null, out result);
        return result;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (draggableImage.dragging)
        {
            draggableImage.gridPointerUp = transform.parent.GetComponent<GridBase>();
        }
    }
}
