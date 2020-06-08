using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    public AnimationCurve easing;
    public float time = .7f;
    public float distance = 1f;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Animator>().SetTrigger("Hit");
            other.GetComponent<Damageable>().ApplyDamageOrHill(12.5f);
            StopAllCoroutines();
            StartCoroutine(Push(other.transform));
        }
    }

    private IEnumerator Push(Transform target)
    {
        Vector3 startPosition = target.transform.position;
        Vector3 targetPosition = startPosition - (transform.position - startPosition).normalized * distance;
        GameManager.instance.SetPlayerStatus(Player.Status.stunned);
        for (float i = 0; i < 1; i += Time.deltaTime / time)
        {
            if (target == null)
            {
                yield break;
            }
            target.transform.position = Vector3.Lerp(startPosition, targetPosition, easing.Evaluate(i));
            yield return null;
        }
        GameManager.instance.SetPlayerStatus(Player.Status.empty);
    }
}
