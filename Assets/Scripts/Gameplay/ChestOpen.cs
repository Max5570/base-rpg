using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Animator animator;
    public enum ConditionToOpen
    {
        none,
        needKey
    }

    public ConditionToOpen conditionToOpen;
    public string itemName;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.GetComponent<Player>() != null)
        {
            if (conditionToOpen == ConditionToOpen.needKey)
            {
                if (InventoryPanel.instance.GetItemByName(itemName) != null)
                {
                    Debug.Log(itemName);
                    InventoryPanel.instance.RemoveItem(InventoryPanel.instance.GetItemByName(itemName));
                }
                else
                {
                    return;
                }
            }
            if (animator != null)
            {
                animator.SetTrigger("Open");
            }
        }
        //player cant move while chest is opening
        GameManager.instance.SetPlayerStatus(Player.Status.stunned);
        Invoke("AllowPlayerMove", 2.5f);
    }

    //now player can move
    private void AllowPlayerMove()
    {
        GameManager.instance.SetPlayerStatus(Player.Status.empty);
    }
}
