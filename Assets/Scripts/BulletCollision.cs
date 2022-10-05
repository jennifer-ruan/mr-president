using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject target;
    public float speed;

    void Start()
    {
        speed = Random.Range(30, 50) * 1f;
        target = GameObject.Find("President");
    }

    void Update()
    {
        if (target){
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
        else {
            Debug.Log("NO TARGET");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name);
        Destroy(gameObject);
    }
}
