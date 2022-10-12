using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    GameObject gameWinScreen;
    bool gameHasWon = false;

    void Start()
    {
        gameWinScreen = GameObject.Find("Victory Screen");
        gameHasWon = false;
        gameWinScreen.SetActive(false);
    }

    public void SetGameWin()
    {
        if (gameHasWon == false)
        {
            gameHasWon = true;
            Debug.Log("Set Game Over function is called");
            gameWinScreen.SetActive(true);
        }
    }

    /*public void RestartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/

    public void ReturnMenu()
    {
        Debug.Log("return to menu");
        SceneManager.LoadScene("NewMenu");
    }
}
