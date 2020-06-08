using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
    public float speed = 5;

    public ProjectileBase projectilePrefab;
    public Animator animator;


    public override void Attack()
    {
        //play animation
        animator.SetTrigger("Attack");
        
        Invoke("InstantiatePrefab", .1f);
    }

    private void InstantiatePrefab()
    {
        float direction = animator.GetFloat("LookDirection");
        ProjectileBase instance = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0,0, -direction));
        instance.transform.position += instance.transform.up * .7f + Vector3.down * .3f;
        instance.GetComponent<Rigidbody2D>().velocity = instance.transform.up * speed;
        instance.damage = damage;
        Destroy(instance.gameObject, 3);
    }
}
