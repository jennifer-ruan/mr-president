using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{

    public Vector2 turn;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("President");
    }

    // Update is called once per frame
    void Update()
    {
        if (target){
        turn.x += Input.GetAxis("Mouse X");
        //turn.y += Input.GetAxis("Mouse Y");

        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        }
    }
}
