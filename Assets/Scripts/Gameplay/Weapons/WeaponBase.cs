using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public string weaponName;
    public string description;
    public float damage;
    public float cooldown;
    public Sprite icon;


    public virtual void Attack(){}
}
