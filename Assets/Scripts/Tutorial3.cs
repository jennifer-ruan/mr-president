using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Start next lvl by controller");
            CloseScreen();
        }
    }

    public void CloseScreen()
    {
        GameObject.Find("Tutorial 3").SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
