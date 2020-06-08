using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropAfterDeath : MonoBehaviour
{
    public GameObject itemPrefab;

    private bool _quitting;

    private void Start() 
    {
        Application.quitting += Quitting;
    }

    private void Quitting()
    {
        _quitting = true;
    }

    private void OnDestroy() 
    {
        if (!_quitting)
        {
            try
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity).transform.parent = GetComponent<Patrolling>().patrollPoints[0];
            }
            catch {}
        }
    }
}
