using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    public GameObject slashPrefab;
    public Animator animator;

    [Header("Push Back")]
    public float distance;
    public float time;
    public AnimationCurve easing;

    public override void Attack()
    {
        //play animation
        animator.SetTrigger("Attack");
        
        Vector3 lookDirection = -Vector3.up;
       
        float offset = .35f;
        switch (animator.GetFloat("LookDirection"))
        {
            case 0:
                lookDirection = Vector3.up;
                break;
            case 90:
                lookDirection = Vector3.right;
                break;
            case 180:
                lookDirection = Vector3.down;
                offset = .5f;
                break;
            case 270:
                lookDirection = Vector3.left;
                break;
        }

        GameObject _slashPrefab = Instantiate(slashPrefab, transform.position + lookDirection * offset, Quaternion.identity);
        _slashPrefab.transform.up = lookDirection;
        _slashPrefab.transform.SetParent(transform);
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position + lookDirection * .2f, .7f, lookDirection, .001f);
        foreach (var item in hit)
        {
            if (item.transform.GetComponent<Damageable>() != null && item.transform.GetComponent<Player>() == null)
            {
                item.transform.GetComponent<Damageable>().ApplyDamageOrHill(damage);
                StopAllCoroutines();
                StartCoroutine(Push(item.transform));
            }
        }
        Destroy(_slashPrefab, .35f);
    }

    private IEnumerator Push(Transform target)
    {
        Vector3 startPosition = target.transform.position;
        Vector3 targetPosition = startPosition - (transform.position - startPosition).normalized * distance;

        for (float i = 0; i < 1; i += Time.deltaTime / time)
        {
            if (target == null)
            {
                yield break;
            }
            target.transform.position = Vector3.Lerp(startPosition, targetPosition, easing.Evaluate(i));
            yield return null;
        }
    }
}
