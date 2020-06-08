using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public InventoryPanel inventory;
    public Image currentWeapon;

    [HideInInspector]
    public string currentLevel;
    [HideInInspector]
    public string previousLevel;


    [HideInInspector]
    public PlayerMovement playerMovement;

    private void Start() 
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void SetPlayerStatus(Player.Status status)
    {
        player.status = status;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        player.transform.position = position;
    }
    public void LoadLevel(string levelName)
    {
        if (!String.IsNullOrEmpty(currentLevel))
        {
            SceneManager.UnloadSceneAsync(currentLevel);
        }
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        previousLevel = currentLevel;
        currentLevel = levelName;
    }
}
