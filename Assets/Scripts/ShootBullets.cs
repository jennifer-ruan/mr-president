using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{

    public GameObject bullet;
    public Transform shooter;
    public float speed;

    public AudioSource randomSound;
    public AudioClip[] audioSources;

    void Start()
    {
        speed = Random.Range(30, 50) * 1f;
        randomSound = gameObject.GetComponent<AudioSource>();
        StartCoroutine(Fire());
    }

    public IEnumerator Fire() 
    {        
        while (true)
        {

            randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            randomSound.Play ();

            yield return new WaitForSeconds(0.5f);
            
            //Create a new bullet
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;

            //Parent it to get a less messy workspace
            newBullet.transform.parent = shooter;

            // //Add velocity to the bullet with a rigidbody
            // newBullet.GetComponent<Rigidbody>().velocity = speed * transform.forward;
            // Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);

            yield return new WaitForSeconds(Random.Range(1, 5) * 1f);
        }
    }
}
