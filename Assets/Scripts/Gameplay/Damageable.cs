using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    [Space(10)]
    [Header("VFX")]
    public UnityEvent onHit;
    public Image[] hearts;

    private void Start() 
    {
        health = maxHealth;
    }

    public void ApplyDamageOrHill(float damage)
    {
        health -= damage;

        onHit.Invoke();

        if (health <= 0)
        {
            Invoke("OnDeath", .2f);
        }
        else if(health > maxHealth)
        {
            health = maxHealth;
        }
        float healthPerHeart = maxHealth/hearts.Length;
        for (int i = 1; i < hearts.Length+1; i++)
        {
            if (health >= i * healthPerHeart)
            {
                continue;
            }
            //Debug.Log((health - healthPerHeart * (i-1))/(healthPerHeart));
            hearts[i-1].fillAmount = (health - healthPerHeart * (i-1))/(healthPerHeart);
        }
        
        //hearts[hearts.Length -1].fillAmount = maxHealth / (hearts.Length - 1) - health / (maxHealth / hearts.Length);
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
