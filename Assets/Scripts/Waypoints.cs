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
    public bool pause = false;

    public AudioClip[] dyingSounds;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // pause is pressed 
        if (!pause && (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Escape))){
            // Debug.Log("1 pause pressed");
            FindObjectOfType<PauseManager>().SetGamePause();
            // Debug.Log("2 pause pressed");
            FindObjectOfType<PauseMenu>().PauseGame();
            // Debug.Log("3 pause pressed");
            pause = true;

        }
        if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGetDownReady && !pause)
        {
            isGettingDown = true;
            StartCoroutine(GetDown());
        }
        if (!isGettingDown && !pause)
        {
            MovePresident();
        }
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

    void MovePresident()
    {
        if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].transform.position) < 1.5f)
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
        Vector3 angle = waypoints[current].transform.position - transform.position;
        //transform.forward = angle;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Harmful")
        {
            Debug.Log(gameObject.name);
            Debug.Log("president is hit");
            AudioSource.PlayClipAtPoint(dyingSounds[Random.Range(0, dyingSounds.Length)], transform.position);
            Destroy(gameObject);
        }
    }

    public void resume()
    {
        pause = false;
    }
}
