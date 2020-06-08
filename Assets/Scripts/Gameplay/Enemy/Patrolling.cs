using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    public List<Transform> patrollPoints;
    public float moveSpeed = 5;
    public float idleTime = 2;
    [Space(10)]
    public Animator animator;
    public Rigidbody2D rb;

    private int _movePointIndex;

    private void Start() 
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        if (patrollPoints.Count == 1)
        {
            yield break;
        }

        while(patrollPoints.Count > 1)
        {
            _movePointIndex ++;
            if (_movePointIndex >= patrollPoints.Count)
            {
                _movePointIndex = 0;
            }
            animator.SetBool("Idle", true);
            yield return new WaitForSeconds(idleTime);
            animator.SetBool("Idle", false);
            PlayMoveAnimation(patrollPoints[_movePointIndex].position);
            yield return StartCoroutine(MoveToPointStatic(_movePointIndex));
        }
    }

    private IEnumerator MoveToPointStatic(int index)
    {
        Vector3 direction = (patrollPoints[index].position - transform.position).normalized;
        float couter = 0;
        while (patrollPoints[index] != null)
        {
            yield return null;
            if (Vector3.Distance(transform.position, patrollPoints[index].position) < .1f)
            {
                yield break;
            }
            transform.position = (transform.position + direction * moveSpeed * Time.deltaTime);

            couter += Time.deltaTime;
            if (couter >= 1)
            {
                couter = 0;
                direction = (patrollPoints[index].position - transform.position).normalized;
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
        if (patrollPoints.Count > 1)
        {
            for (int i = 0; i < patrollPoints.Count; i++)
            {
                if (i+1 < patrollPoints.Count)
                {
                    Gizmos.DrawLine(patrollPoints[i].position, patrollPoints[i+1].position);
                }
                else
                {
                    Gizmos.DrawLine(patrollPoints[i].position, patrollPoints[0].position);
                }
            }
        }
    }
}
