using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : Singleton<InventoryPanel>
{
    public Item emptyItem;

    public List<Item> items;
    public List<GridBase> grids;
    private int id;

    private void Start() 
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            if (GetCount(grids[i]) < 1)
            {
                grids[i].SetItem(emptyItem);
                grids[i].count.text = "";
                items[grids.IndexOf(grids[i])] = emptyItem;
                continue;
            }
            if (items[i] != null)
            {
                grids[i].SetItem(items[i]);
            }
        }
    }

    public Item GetItemByName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == name)
            {
                return items[i];
            }
        }
        return null;
    }

    public void AddItem(Item item)
    {
        if (GetItemByName(item.itemName) != null)
        {
            for (int i = 0; i < grids.Count; i++)
            {
                if (grids[i].item.itemName == item.itemName)
                {
                    grids[i].count.text = (GetCount(grids[i]) + 1).ToString();
                    break;
                }
            }
            UpdateInventory();
            return;
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == "")
            {
                items[i] = item;
                break;
            }
        }

        UpdateInventory();
    }

    public void RemoveItem(Item item, int count = 1)
    {
        if (GetItemByName(item.itemName) != null)
        {
            for (int i = 0; i < grids.Count; i++)
            {
                if (grids[i].item.itemName == item.itemName)
                {
                    grids[i].count.text = (GetCount(grids[i]) - count).ToString();
                    break;
                }
            }
        }

        UpdateInventory();
    }

    public void SwapItems(GridBase firstGrid, GridBase secondGrid)
    {
        Item firstItem = firstGrid.item;
        Item secondItem = secondGrid.item;
        
        items[grids.IndexOf(firstGrid)] = secondItem;
        items[grids.IndexOf(secondGrid)] = firstItem;

        string firstGridItemCount = firstGrid.count.text;
        string secondGridItemCount = secondGrid.count.text;
        firstGrid.count.text = secondGridItemCount;
        secondGrid.count.text = firstGridItemCount;
        
        UpdateInventory();
    }

    private int GetCount(GridBase grid)
    {
        try
        {
            return int.Parse(grid.count.text);
        }
        catch
        {
            return 1;
        }
    }
}
