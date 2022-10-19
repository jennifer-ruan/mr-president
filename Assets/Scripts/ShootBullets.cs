using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{

    public GameObject bullet;
    public GameObject shooter;
    public float speed;
    public GameObject target;

    bool isActive = false;

    void Start()
    {
        speed = Random.Range(30, 50) * 1f;
        target = GameObject.Find("President");
    }

    void OnTriggerEnter(Collider collision) 
    {        
        if (collision.name == "President" && !isActive){
            isActive = true;
            
            //Create a new bullet
            GameObject newBullet = Instantiate(bullet, shooter.transform.position, shooter.transform.rotation) as GameObject;

            //Parent it to get a less messy workspace
            newBullet.transform.parent = shooter.transform;
        }
    }
}