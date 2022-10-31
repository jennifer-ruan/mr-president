using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject target;
    public float speed;
    public Vector3 prevPosition;

    GameObject line;
    LineRenderer lr;

    public AudioSource randomSound;
    public AudioClip[] audioSources;

    float timer = 0;
    bool timerReached = false;

    List<Transform> chars = new List<Transform>();

    GameOverManager gameOverManager;

    void Start()
    {
        speed = Random.Range(30, 50) * 1f;
        target = GameObject.Find("President");

        var agents = GameObject.Find("Agents").transform;
        foreach(Transform child in agents)
        {
            chars.Add(child);
            // Debug.Log("added agent");
        }

        randomSound = gameObject.GetComponent<AudioSource>();
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play ();

        line = new GameObject("Line");
        line.transform.position = transform.position;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.SetWidth(0.8f, 0.3f);
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, targetVector());

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.4f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

    void Update()
    {
        if (!timerReached){
            timer += Time.deltaTime;
        }
        if (!timerReached && timer > 0.5)
        {
            timerReached = true;
        }
        if (timerReached){
            if (target){
                prevPosition = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, targetVector(), Time.deltaTime * speed);

                //if president dodged then the projectile will be directly above his head and stay there until the next update.
                //This is a temp solution, as there is an edge case where president gets down as early as possible,
                //causing bullet to stop above his head just before he starts moving again, thus causing the bullet 
                //to stick to his head until the next time he gets down since the states never matched between consecutive updates
                if (prevPosition == transform.position)
                {
                    Destroy(line);
                    Destroy(gameObject);
                }
                lr.SetPosition(1, targetVector());
            }
            else {
                Debug.Log("NO TARGET");
                Destroy(line);
                Destroy(gameObject);
            }
        }
    }

    public Vector3 targetVector()
    {
        //Don't target the y value, stay only on the y value of shooter. This is so that get-down works to avoid bullets
        return new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DESTROYED" + collision.gameObject.name);
        if (collision.gameObject.name == "President"){
            Debug.Log("game over");
            Destroy(gameObject);
            FindObjectOfType<GameOverManager>().SetGameOver();
        }
        else{
            Destroy(gameObject);
            float closestDistance = 1000;
            GameObject closestAgent = null;
            foreach(Transform c in chars)
            {
                if (c != null)
                {
                    float dist = Vector3.Distance(c.position, transform.position);
                    if (dist < closestDistance){
                        closestDistance = dist;
                        closestAgent = c.gameObject;
                    }
                }
            }
            closestAgent.GetComponent<agent_movement>().Unalive();
        }
    }
}