using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public string levelName;
    public Image fade;

    public enum ConditionToLoad
    {
        none,
        needKey
    }

    public ConditionToLoad conditionToLoad;
    public string itemName;
    public bool removeItem;

    [HideInInspector]
    public Animator animator;
    private void Start() 
    {
        StartCoroutine(FadeOut());
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Player>() != null)
        {
            if (conditionToLoad == ConditionToLoad.needKey)
            {
                if (InventoryPanel.instance.GetItemByName(itemName) == null)
                {
                    return;
                }
                else
                {
                    if (removeItem)
                    {
                        InventoryPanel.instance.RemoveItem(InventoryPanel.instance.GetItemByName(itemName));
                    }
                }
            }
            if (animator != null)
            {
                animator.SetTrigger("Open");
            }

            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        GameManager.instance.SetPlayerStatus(Player.Status.stunned);
        fade.gameObject.SetActive(true);

        Color color = Color.black;
#if UNITY_EDITOR
        fade.gameObject.SetActive(false);
        GameManager.instance.SetPlayerStatus(Player.Status.empty);
        yield break;
#endif
        for (float i = 0; i < 1; i += Time.deltaTime/2)
        {
            color.a = 1 - i;
            fade.color = color;
            yield return null;
        }
        fade.gameObject.SetActive(false);
        GameManager.instance.SetPlayerStatus(Player.Status.empty);
    }
    private IEnumerator FadeIn()
    {
        GameManager.instance.SetPlayerStatus(Player.Status.stunned);
        fade.gameObject.SetActive(true);
        Color color = Color.black;
#if UNITY_EDITOR
        GameManager.instance.SetPlayerStatus(Player.Status.empty);
        GameManager.instance.LoadLevel(levelName);
        yield break;
#endif
        for (float i = 0; i < 1; i += Time.deltaTime/2)
        {
            color.a = i;
            fade.color = color;
            yield return null;
        }
        
        GameManager.instance.SetPlayerStatus(Player.Status.empty);
        GameManager.instance.LoadLevel(levelName);
    }
}
