using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollision : MonoBehaviour
{
    List<Transform> chars = new List<Transform>();
    GameObject prez;
    float lethalRad;
    float knockRad;

    public AudioSource audio;
    public AudioClip[] clangSounds;

    Transform dieCircle;
    Transform triggerCircle;

    // Start is called before the first frame update
    void Start()
    {
        var agents = GameObject.Find("Agents").transform;
        prez = GameObject.Find("President");
        dieCircle = gameObject.transform.parent.Find("CircleOfDying");
        triggerCircle = gameObject.transform.parent.Find("Trigger");

        lethalRad = dieCircle.localScale.x;
        knockRad = triggerCircle.localScale.x;

        Debug.Log("LETHAL RADIUS: " + lethalRad);
        Debug.Log("KNOCK RAD: " + knockRad);

        foreach(Transform child in agents)
        {
            chars.Add(child);
            // Debug.Log("added agent");
        }
        chars.Add(prez.transform);
        // Debug.Log("added Prez");
    }

    // Update is called once per frame
    void Update()
    {
        if (CharInRange())
        {
            Flatten();
        }
    }

    public bool CharInRange()
    {
        foreach(Transform c in chars)
        {
            if (c != null)
            {
                //Debug.Log(c);
                //Debug.Log(Vector3.Distance(transform.position, c.position));
                if (Vector3.Distance(transform.position, c.position) < lethalRad)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Flatten(){
        foreach (Transform c in chars)
        {
            if (c != null)
            {
                // GET 2D DISTANCE
                float x_dist = Mathf.Abs(transform.transform.position.x - c.transform.position.x);
                float z_dist = Mathf.Abs(transform.transform.position.z - c.transform.position.z);

                float horizontal_dist = Mathf.Sqrt((x_dist * x_dist) + (z_dist * z_dist));

                // float dist = Vector3.Distance(transform.position, c.position);
                if (horizontal_dist < lethalRad)
                {
                    if (c.gameObject.name == "President")
                    {
                        Destroy(c.gameObject);
                        FindObjectOfType<GameOverManager>().SetGameOver();
                    }
                    else
                    {
                        c.gameObject.GetComponent<agent_movement>().Unalive();
                    }
                }
                else if (horizontal_dist < knockRad)
                {
                    if (c.gameObject.name == "President")
                    {
                        Debug.Log(horizontal_dist);
                        Destroy(c.gameObject);
                        FindObjectOfType<GameOverManager>().SetGameOver();
                    }
                    else
                    {
                        c.gameObject.GetComponent<Rigidbody>().AddForce((c.position - transform.position) * 5, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Ground"){
            AudioSource.PlayClipAtPoint(clangSounds[Random.Range(0, clangSounds.Length)], transform.position);
            Destroy(triggerCircle.gameObject);
            Destroy(dieCircle.gameObject);
            Destroy(gameObject);
        }
    }
}
