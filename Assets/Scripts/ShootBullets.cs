using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{

    public GameObject bullet;
    public Transform shooter;
    public float speed = 30f;
    void Start()
    {
        StartCoroutine(Fire());
    }

    public IEnumerator Fire() 
    {        
        while (true)
        {
            //Create a new bullet
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;

            //Parent it to get a less messy workspace
            newBullet.transform.parent = shooter;

            // //Add velocity to the bullet with a rigidbody
            newBullet.GetComponent<Rigidbody>().velocity = speed * transform.forward;

            yield return new WaitForSeconds(5f);
        }
    }
}
