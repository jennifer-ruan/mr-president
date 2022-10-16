using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float horizontalinput;
    public float verticalinput;
    float speed = 10.0f;
    public Transform maincamera;
    public GameObject target;
    public bool isGettingDown = false;
    public bool isGetDownReady = true;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("President");
        maincamera = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target){
            if (Input.GetKeyDown(KeyCode.G) && isGetDownReady)
            {
                isGettingDown = true;
                StartCoroutine(GetDown());
            }
            StartCoroutine(moveTarget());
        }
    }

    IEnumerator GetDown()
    {
        isGetDownReady = false;
        yield return new WaitForSeconds(2f);

        //resume movement, get down cooldown
        isGettingDown = false;
        yield return new WaitForSeconds(5f);

        //cooldown complete
        isGetDownReady = true;
    }

    IEnumerator moveTarget()
    {
        while (isGettingDown)
        {
            yield return null;
        }
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
