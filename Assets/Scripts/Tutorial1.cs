using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Start next lvl by controller");
            CloseScreen();
        }
    }

    // Update is called once per frame
    public void CloseScreen()
    {
        GameObject.Find("Tutorial 1").SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
