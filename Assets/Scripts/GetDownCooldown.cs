using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetDownCooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    private bool isCoolingDown = false;
    //the value of 7 is derived from the 2 second activation window plus 5 second cooldown
    private float coolDownLength = 7f;
    private float coolDownTimer = 0f;

    void Start()
    {
        imageCooldown.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetDown();
        }
        if (isCoolingDown)
        {
            UpdateCoolDown();
        }
    }

    void UpdateCoolDown()
    {
        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer <= 0)
        {
            isCoolingDown = false;
            imageCooldown.fillAmount = 0f;
        }
        else
        {
            Debug.Log(coolDownTimer / coolDownLength);
            imageCooldown.fillAmount = coolDownTimer / coolDownLength;
        }
    }

    void GetDown()
    {
        if (!isCoolingDown)
        {
            isCoolingDown = true;
            coolDownTimer = coolDownLength;
        }
    }
}
