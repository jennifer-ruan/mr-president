using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    GameObject gameOverScreen;
    bool gameHasEnded = false;

    void Start() {
        gameOverScreen = GameObject.Find("Game Over Screen");
        gameHasEnded = false;
        gameOverScreen.SetActive(false);
    }

    public void SetGameOver()
    {
        if (gameHasEnded == false){
            gameHasEnded = true;
            Debug.Log("Set Game Over function is called"); 
            gameOverScreen.SetActive(true);
        }
    }

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
}
