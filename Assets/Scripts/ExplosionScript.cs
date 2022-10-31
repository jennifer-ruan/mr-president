using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    List<Transform> chars = new List<Transform>();
    GameObject prez;
    Transform smallr;
    Transform bigr;
    public float lethalRad;
    public float knockRad;


    // Start is called before the first frame update
    void Start()
    {
        lethalRad = 3f;
        knockRad = 10f;
        var agents = GameObject.Find("Agents").transform;
        prez = GameObject.Find("President");

        foreach(Transform child in agents)
        {
            chars.Add(child);
            // Debug.Log("added agent");
        }
        chars.Add(prez.transform);
        // Debug.Log("added Prez");

        smallr = transform.Find("smallrad");
        bigr = transform.Find("bigrad");

        smallr.localScale = new Vector3(lethalRad, 0.001f, lethalRad);
        bigr.localScale = new Vector3(knockRad, 0.001f, knockRad);
    }

    // Update is called once per frame
    void Update()
    {
        if (CharInRange())
        {
            Explode();
            Destroy(this.gameObject);
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
    void Explode()
    {
        Debug.Log("BOOM!!!");
        foreach (Transform c in chars)
        {
            if (c != null)
            {
                float dist = Vector3.Distance(transform.position, c.position);
                if (dist < lethalRad)
                {
                    Destroy(c.gameObject);
                    Debug.Log("deadge");
                    if (c.gameObject.name == "President")
                    {
                        FindObjectOfType<GameOverManager>().SetGameOver();
                    }
                }
                else if (dist < knockRad)
                {
                    Debug.Log("weeee");
                    if (c.gameObject.name == "President")
                    {
                        Debug.Log(dist);
                        Destroy(c.gameObject);
                        FindObjectOfType<GameOverManager>().SetGameOver();
                    }
                    else
                    {
                        c.gameObject.GetComponent<Rigidbody>().AddForce((c.position - transform.position) * 5f, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
