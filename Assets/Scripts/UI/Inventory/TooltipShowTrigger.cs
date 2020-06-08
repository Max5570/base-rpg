using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipShowTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName { get; set; }
    public string description { get; set; }
    public void OnPointerEnter(PointerEventData eventData)
	{
		Tooltip.instance.Show(itemName, description);
        Tooltip.instance.TransformToMousePosition();
	}

    public void OnPointerExit(PointerEventData eventData)
	{
		Tooltip.instance.Hide();
	}
}
