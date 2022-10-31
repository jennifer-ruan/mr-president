using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    GameObject pauseScreen;

    void Start()
    {
        // gameOverScreen = GameObject.Find("Game Over Screen");
        // getDownCoolDown = GameObject.Find("Get down cooldown");
        // gameHasEnded = false;
        // gameOverScreen.SetActive(false);
        pauseScreen = GameObject.Find("Pause Screen");
        pauseScreen.SetActive(false);
    }

    public void SetGamePause()
    {
        pauseScreen.SetActive(true);
        // make the cursor visible
        Cursor.lockState = CursorLockMode.None;
    }

}
