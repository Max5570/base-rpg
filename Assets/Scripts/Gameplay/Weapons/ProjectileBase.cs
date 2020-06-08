using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.transform.GetComponent<Damageable>() != null && other.transform.GetComponent<Player>() == null)
        {
            other.transform.GetComponent<Damageable>().ApplyDamageOrHill(damage);
            Destroy(gameObject, .1f);
        }
    }
}
