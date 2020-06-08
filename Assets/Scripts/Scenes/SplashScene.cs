using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScene : MonoBehaviour
{
    public string levelName = "1_1";
    private void Awake() 
    {
        GameManager.instance.LoadLevel(levelName);
        Destroy(gameObject);
    }
}
