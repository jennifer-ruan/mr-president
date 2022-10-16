using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    float WPradius = 1;
    float rotationSpeed = 45;
    public bool isGettingDown = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGettingDown = true;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
            StartCoroutine(GetDown());
        }
        
        StartCoroutine(MovePresident());
    }

    IEnumerator GetDown()
    {
        float angle = rotationSpeed * 10;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
        yield return new WaitForSeconds(2f);
        isGettingDown = false;
    }

    IEnumerator MovePresident()
    {
        while (isGettingDown)
        {
            yield return null;
        }
        if (Vector3.Distance(transform.position, waypoints[current].transform.position) < 1.5f)
        {
            FindObjectOfType<VictoryManager>().SetGameWin();
        }
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            if (current < waypoints.Length-1)
            {
                current++;
            }
            if (waypoints[current].name.Substring(3) == "waypoint")
            {
                waypoints[current].transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (waypoints[current].name.Substring(3) == "exit waypoint")
            {
                waypoints[current - 1].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Harmful")
        {
            Debug.Log(gameObject.name);
            Debug.Log("president is hit");
            Destroy(gameObject);
        }
    }
}
