using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{


    public void SetGameOver()
    {
        Debug.Log("Set Game Over functio is called");
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Tutorial");
    }

    public void ReturnMenu()
    {
        Debug.Log("return to menu");
        SceneManager.LoadScene("NewMenu");
    }

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
