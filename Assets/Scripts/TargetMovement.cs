using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float horizontalinput;
    public float verticalinput;
    float speed = 10.0f;
    public Transform maincamera;


    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveTarget();
    }

    void moveTarget()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        verticalinput = Input.GetAxis("Vertical");
        var right = maincamera.right;
        var forward = maincamera.forward;



        if (horizontalinput != 0 && verticalinput != 0)
        {
            horizontalinput = horizontalinput * 0.6f;
            verticalinput = verticalinput * 0.6f;
        }

        right.y = 0f;
        forward.y = 0f;
        right.Normalize();
        forward.Normalize();

        transform.Translate(right * horizontalinput * Time.deltaTime * speed);
        transform.Translate(forward * verticalinput * Time.deltaTime * speed);
    }
}
