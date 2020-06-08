using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Status
    {
        empty,
        stunned,
        invulnerable
    }

    public Status status;

    private void Awake() 
    {
        status = Status.empty;
        GameManager.instance.player = this;
    }
}
