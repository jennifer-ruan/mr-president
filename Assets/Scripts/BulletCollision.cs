using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject target;
    public float speed;
    GameObject line;
    LineRenderer lr;

    GameOverManager gameOverManager;

    void Start()
    {
        speed = Random.Range(30, 50) * 1f;
        target = GameObject.Find("President");

        line = new GameObject("Line");
        line.transform.position = transform.position;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.SetWidth(0.8f, 0.3f);
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target.transform.position);

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.4f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

    void Update()
    {
        if (target){
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            lr.SetPosition(1, target.transform.position);
        }
        else {
            Debug.Log("NO TARGET");
            Destroy(line);
            Destroy(gameObject);
        }
    }
}