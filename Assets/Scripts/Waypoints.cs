using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    public float getDownActivationTime = 2f;
    public float getDownCooldownTime = 3f;
    float WPradius = 1;
    public bool isGettingDown = false;
    public bool isGetDownReady = true;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGetDownReady)
        {
            isGettingDown = true;
            StartCoroutine(GetDown());
        }

        StartCoroutine(MovePresident());
    }

    IEnumerator GetDown()
    {
        isGetDownReady = false;
        //get down motion
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);

        yield return new WaitForSeconds(getDownActivationTime);

        //get up motion
        transform.rotation *= Quaternion.AngleAxis(-90, Vector3.right);

        //face front
        Vector3 waypointAngle = waypoints[current].transform.position - transform.position;
        transform.forward = waypointAngle;

        //resume movement, get down cooldown
        isGettingDown = false;
        yield return new WaitForSeconds(getDownCooldownTime);

        //cooldown complete
        isGetDownReady = true;

    }

    IEnumerator MovePresident()
    {
        while (isGettingDown)
        {
            yield return null;
        }
        if (Vector3.Distance(transform.position, waypoints[waypoints.Length-1].transform.position) < 1.5f)
        {
            FindObjectOfType<VictoryManager>().SetGameWin();
        }
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            if (current < waypoints.Length - 1)
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
