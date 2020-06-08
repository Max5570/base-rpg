using System;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public Transform playerStartPos;
    public Transform playerPosPrevLevel;

    private LoadLevel door;

    private void Start()
    {
        try
        {
            int previousLevelIndex = int.Parse((GameManager.instance.previousLevel[GameManager.instance.previousLevel.Length - 1]).ToString());
            int loadingLevelIndex = int.Parse((GameManager.instance.currentLevel[GameManager.instance.currentLevel.Length - 1]).ToString());
            if (previousLevelIndex > loadingLevelIndex)
            {
                Debug.Log("Previos level loaded");
                GameManager.instance.SetPlayerPosition(playerPosPrevLevel.position);
                try
                {
                    door = GameObject.FindGameObjectWithTag("Door").GetComponent<LoadLevel>();
                    door.animator.SetTrigger("Close");
                }
                catch {}
            }
            else
            {
                Debug.Log("Next level loaded");
                GameManager.instance.SetPlayerPosition(playerStartPos.position);
            }
        }
        catch { }
    }
}