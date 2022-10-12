using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{

    public GameObject bullet;
    public GameObject shooter;
    public float speed;
    public GameObject target;

    public AudioSource randomSound;
    public AudioClip[] audioSources;

    bool isActive = false;

    void Start()
    {
        speed = Random.Range(30, 50) * 1f;
        target = GameObject.Find("President");
        randomSound = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision) 
    {        
        if (collision.name == "President" && !isActive){
            isActive = true;

            randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            randomSound.Play ();
            
            Invoke ("Shoot", 0.5f);

            // //Add velocity to the bullet with a rigidbody
            // newBullet.GetComponent<Rigidbody>().velocity = speed * transform.forward;
            // Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
    }

    void Shoot(){
        //Create a new bullet
        GameObject newBullet = Instantiate(bullet, shooter.transform.position, shooter.transform.rotation) as GameObject;

        //Parent it to get a less messy workspace
        newBullet.transform.parent = shooter.transform;
    }
}
