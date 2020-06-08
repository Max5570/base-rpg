using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persecution : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5;
    public float pathUpdateRate = .3f;

    [Space(10)]
    public Animator animator;
    public Rigidbody2D rb;


    private float idleTime = 2;

    private void Start() 
    {
        if (target == null)
        {
            target = GameManager.instance.player.transform;
        }
        StartCoroutine(Persecute());
    }

    private IEnumerator Persecute()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float couter = 0;
        while (target != null)
        {
            yield return null;
            transform.position = (transform.position + direction * moveSpeed * Time.deltaTime);

            couter += Time.deltaTime;
            if (couter >= pathUpdateRate)
            {
                couter = 0;
                direction = (target.position - transform.position).normalized;
                PlayMoveAnimation(target.position);
            }
        }
    }

    private void PlayMoveAnimation(Vector3 position)
    {
        if(Vector3.Angle(position - transform.position, -Vector3.up) < 45)
        {
            animator.SetFloat("MoveDirection", 0);
        }
        else if(Vector3.Angle(position - transform.position, Vector3.up) < 45)
        {
            animator.SetFloat("MoveDirection", 1);
        }
        else if(Vector3.Angle(position - transform.position, -Vector3.right) < 45)
        {
            animator.SetFloat("MoveDirection", 2);
        }
        else if(Vector3.Angle(position - transform.position, Vector3.right) < 45)
        {
            animator.SetFloat("MoveDirection", 3);
        }
    }

    private void OnDrawGizmos() 
    {
        if (target != null)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
