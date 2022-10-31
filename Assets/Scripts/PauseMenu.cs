using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMenu()
    {
        Debug.Log("return to menu");
        SceneManager.LoadScene("NewMenu");
    }

    public void QuitGame()
    {   
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PauseGame()
    {   
        Debug.Log("the game shoud be paused");
        // Time.timeScale = 0f;
    }

    public void ResumeGame()
    {

    }
}
