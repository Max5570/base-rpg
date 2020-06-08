using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public WeaponBase[] weapons;
    public WeaponBase currentWeapon;
    public KeyCode[] weaponKeyCode;

    private Player _player;
    private bool _cooldowning = false;

    private void Start() 
    {
        _player = GetComponent<Player>();
        SwitchWeapon(0);
    }

    private void Update() 
    {
        //while weapon cooldowning can't change or use weapon
        if (Input.GetKeyDown(KeyCode.F) && !_cooldowning)
        {
            //can't attack if stunned
            if (_player.status == Player.Status.stunned)
            {
                return;
            }
            _cooldowning = true;
            currentWeapon.Attack();
            StopAllCoroutines();
            StartCoroutine(Cooldown());
        }

        for (int i = 0; i < weaponKeyCode.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeyCode[i]))
            {
                SwitchWeapon(i);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(currentWeapon.cooldown);
        _cooldowning = false;
    }

    public void SwitchWeapon(int index)
    {
        if (_cooldowning)
            return;
        
        currentWeapon = weapons[index];
        GameManager.instance.currentWeapon.sprite = currentWeapon.icon;
    }

}
