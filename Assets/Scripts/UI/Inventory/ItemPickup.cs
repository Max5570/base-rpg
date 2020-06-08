using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private void Start() 
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
            InventoryPanel.instance.AddItem(item);
        }
    }
}
