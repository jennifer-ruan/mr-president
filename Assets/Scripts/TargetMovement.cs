using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float horizontalinput;
    public float verticalinput;
    float speed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalinput = Input.GetAxis("Vertical");
        verticalinput = Input.GetAxis("Horizontal");

        if (horizontalinput != 0 && verticalinput != 0)
        {
            horizontalinput = horizontalinput * 0.6f;
            verticalinput = verticalinput * 0.6f;
        }
        
        
        this.transform.Translate(Vector3.right * horizontalinput * Time.deltaTime * speed * -1);
        this.transform.Translate(Vector3.forward * verticalinput * Time.deltaTime * speed);
    }
}
