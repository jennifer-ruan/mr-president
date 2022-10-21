using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    GameObject gameWinScreen;
    GameObject getDownCoolDown;
    GameObject target;
    public bool gameHasWon = false;

    void Start()
    {
        gameWinScreen = GameObject.Find("Victory Screen");
        getDownCoolDown = GameObject.Find("Get down cooldown");
        gameHasWon = false;
        target = GameObject.Find("President");
        gameWinScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!(target) || gameHasWon)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetGameWin()
    {
        if (gameHasWon == false)
        {
            gameHasWon = true;
            Debug.Log("Set Game Over function is called");
            getDownCoolDown.SetActive(false);
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
