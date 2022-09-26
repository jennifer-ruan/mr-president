using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    void Start()
    {
        //HELLO
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log(gameObject.name);
            Destroy(gameObject);
    }
}
