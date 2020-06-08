using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : Singleton<Tooltip>
{
    public Camera uiCamera;
    public Text tooltipText;
    public RectTransform background;

    public enum Status
    {
        empty,
        dragItem
    }
    public Status status;

    private void Start() 
    {
        
        Hide();
    }

    private void FixedUpdate()
    {
        TransformToMousePosition();
    }

    public void TransformToMousePosition()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(string itemName, string description)
    {
        if (status == Status.dragItem)
        {
            return;
        }
        tooltipText.text = itemName;
        tooltipText.text += "\n" + "\n" + description;
        float paddingSize = 15;
        Vector2 bgSize = new Vector2(tooltipText.preferredWidth + paddingSize * 2, tooltipText.preferredHeight + paddingSize);
        background.sizeDelta = bgSize;

        gameObject.SetActive(true);
    }
}
