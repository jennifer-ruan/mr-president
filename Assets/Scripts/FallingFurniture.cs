using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFurniture : MonoBehaviour
{
    public GameObject furniture;
    public GameObject spawnLocation;
    bool fallen = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision) 
    {        
        if ((collision.name == "President" || collision.tag == "Agent") && !fallen){
            fallen = true;

            // randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            // randomSound.Play ();
            
            Invoke ("Drop", 0.5f);

            // //Add velocity to the bullet with a rigidbody
            // newBullet.GetComponent<Rigidbody>().velocity = speed * transform.forward;
            // Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
    }

    void Drop(){
        //Create a new bullet
        GameObject fallingFurniture = Instantiate(furniture, spawnLocation.transform.position, spawnLocation.transform.rotation) as GameObject;

        //Parent it to get a less messy workspace
        fallingFurniture.transform.parent = spawnLocation.transform;
    }
}
