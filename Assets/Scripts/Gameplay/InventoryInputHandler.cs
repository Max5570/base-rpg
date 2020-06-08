using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.instance.inventory.transform.parent.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            GameManager.instance.inventory.transform.parent.gameObject.SetActive(false);
            Tooltip.instance.Hide();
        }
    }
}
