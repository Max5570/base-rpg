using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBase : MonoBehaviour
{
    public Image image;
    public Text count;
    public Item item { get; set; }
    public TooltipShowTrigger tooltipShow { get; private set; }

    private void Awake()
    {
        tooltipShow = GetComponent<TooltipShowTrigger>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        image.sprite = item.sprite;
        tooltipShow.itemName = item.itemName;
        tooltipShow.description = item.description;
    }
}
