using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollision : MonoBehaviour
{
    List<Transform> chars = new List<Transform>();
    GameObject prez;
    float lethalRad;
    float knockRad;

    [Range(0,50)]
    public int segments = 50;
    [Range(0,5)]
    public float xradius = 5;
    [Range(0,5)]
    public float yradius = 5;
    GameObject circle;
    LineRenderer line;

    public float ShrinkDuration = 1f;
    public Vector3 TargetScale = Vector3.one * 0.5f;
    Vector3 startScale;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        lethalRad = 2f;
        knockRad = 5f;
        var agents = GameObject.Find("Agents").transform;
        prez = GameObject.Find("President");

        foreach(Transform child in agents)
        {
            chars.Add(child);
            // Debug.Log("added agent");
        }
        chars.Add(prez.transform);
        // Debug.Log("added Prez");


        circle = new GameObject("Circle");
        circle.transform.position = new Vector3(transform.position.x,0,transform.position.z);
        circle.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        circle.AddComponent<LineRenderer>();
        line = circle.GetComponent<LineRenderer>();
        line.SetVertexCount (segments + 1);
        line.useWorldSpace = false;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f), new GradientColorKey(Color.black, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.6f, 0.0f), new GradientAlphaKey(0.8f, 1.0f) }
        );
        line.colorGradient = gradient;
        line.material = new Material(Shader.Find("Sprites/Default"));
        startScale = circle.transform.localScale;
        t = 0;
        DrawShadow();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharInRange())
        {
            Flatten();
        }
        else {
            t += Time.deltaTime / ShrinkDuration;
            Vector3 newScale = Vector3.Lerp(startScale, TargetScale, t);
            circle.transform.localScale = newScale;
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
                    Destroy(c.gameObject);
                    if (c.gameObject.name == "President")
                    {
                        FindObjectOfType<GameOverManager>().SetGameOver();
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

        Destroy(circle);
        Destroy(gameObject);
    }

    void DrawShadow(){
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition (i,new Vector3(x,y,0) );

            angle += (360f / segments);
        }
    }
}
