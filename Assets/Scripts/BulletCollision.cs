using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject target;
    public float speed;

    GameOverManager gameOverManager;

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
        Debug.Log("DESTROYED" + collision.gameObject.name);
        if (collision.gameObject.name == "President"){
            Debug.Log("game over");
            Destroy(gameObject);
            FindObjectOfType<GameOverManager>().SetGameOver();
        }
        else{
            Destroy(gameObject);
        }
    }
}
