using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    List<Transform> chars = new List<Transform>();
    GameObject prez;
    float lethalRad;
    float knockRad;


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
            Debug.Log("added agent");
        }
        chars.Add(prez.transform);
        Debug.Log("added Prez");
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
            //Debug.Log(c);
            //Debug.Log(Vector3.Distance(transform.position, c.position));
            if (Vector3.Distance(transform.position, c.position) < lethalRad)
            {
                
                return true;
            }
        }
        return false;
    }
    void Explode()
    {
        Debug.Log("BOOM!!!");
        foreach (Transform c in chars)
        {
            float dist = Vector3.Distance(transform.position, c.position);
            if (dist < lethalRad)
            {
                Destroy(c.gameObject);
                Debug.Log("deadge");
            }else if (dist < knockRad)
            {
                Debug.Log("weeee");
                if (c.gameObject.name == "President")
                {
                    Debug.Log(dist);
                    Destroy(c.gameObject);
                }
                else
                {
                    c.gameObject.GetComponent<Rigidbody>().AddForce((c.position - transform.position) * 10, ForceMode.Impulse);
                }
            }
        }
    }
}
