using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{

    public GameObject[] bullets;
    public GameObject shooter;
    public float speed;
    public GameObject target;

    public AudioClip[] shootingSounds;

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

            AudioSource.PlayClipAtPoint(shootingSounds[Random.Range(0, shootingSounds.Length)], transform.position);
            
            Invoke("Shoot", 0.5f);
        }
    }

    void Shoot(){
        //Create a new bullet
        GameObject newBullet = Instantiate(bullets[Random.Range(0, bullets.Length)], shooter.GetComponent<Collider>().transform.position, shooter.GetComponent<Collider>().transform.rotation) as GameObject;

        //Parent it to get a less messy workspace
        newBullet.transform.parent = shooter.transform;
    }
}