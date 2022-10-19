using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class agent_movement : MonoBehaviour
{

    public float horizontalinput;
    public float verticalinput;
    float speed = 10.0f;
    float speedmultiple;
    float dragVar = 15f;

    public AudioSource randomSound;
    public AudioClip[] dyingSounds;
    public AudioClip[] smackSounds;
    public GameObject target;
    public GameObject president;
    private Vector3 targetpos;
    public float jumpAmount = 5;
    public float getDownActivationTime = 2f;
    public float getDownCooldownTime = 3f;
    public bool isGettingDown = false;
    public bool isGetDownReady = true;

    // public bool EndScreenOn = false;

    // GameOverManager gameOverManager;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MoveTarget");
        president = GameObject.Find("President");
        targetpos = target.transform.position;
        rb = GetComponent<Rigidbody>();
        speedmultiple = 2000f;
    }

    // Update is called once per frame
    void Update()
    {

        // check if president is still in the game, if not end game
        // if (!president){
        //     Debug.Log("game over");
        //     gameOverManager.SetGameOver();
        //     // SceneManager.LoadScene("End");
        //     // EndScreenOn = true;
        // }

        // else {
        if (president)
        {
            targetpos = target.transform.position;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke("addJumpForce", Vector3.Distance(transform.position, targetpos) * Vector3.Distance(transform.position, targetpos) * 0.02f);
            }
            if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGetDownReady)
            {
                isGettingDown = true;
                StartCoroutine(GetDown());
            }

            StartCoroutine(moveAgent());
        }
        // }

    }

    void addJumpForce()
    {
        rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
    }

    IEnumerator GetDown()
    {
        isGetDownReady = false;
        //get down motion
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);

         AudioSource.PlayClipAtPoint(smackSounds[Random.Range(0, smackSounds.Length)], transform.position);

        //stay in place on the ground
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ;
        yield return new WaitForSeconds(getDownActivationTime);

        //restore normal constraints
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //get up motion
        transform.rotation *= Quaternion.AngleAxis(-90, Vector3.right);

        //resume movement, get down cooldown
        isGettingDown = false;
        yield return new WaitForSeconds(getDownCooldownTime);

        //cooldown complete
        isGetDownReady = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Harmful")
        {

            // randomSound.clip = dyingSounds[Random.Range(0, dyingSounds.Length)];
            AudioSource.PlayClipAtPoint(dyingSounds[Random.Range(0, dyingSounds.Length)], transform.position);
            // randomSound.Play ();

            Debug.Log(gameObject.name);
            Debug.Log("agent is hit");
            Destroy(gameObject);
        }
    }
    IEnumerator moveAgent()
    {
        while (isGettingDown)
        {
            yield return null;
        }
        float distance = Vector3.Distance(transform.position, targetpos);
        speed = distance * 8 * speedmultiple;
        Debug.DrawLine(transform.position, targetpos + new Vector3(0, 1, 0), Color.white, 100f, false);

        Vector3 angle = transform.position - president.transform.position;

        rb.drag = dragVar / (distance * distance);
        // Debug.Log(transform.position.y);
        if (transform.position.y < 1.3f)
        {
            rb.AddForce(targetpos - transform.position);
        }
        // Debug.Log("force added");

        transform.forward = angle;
    }
    public void Push(Vector3 dir)
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.AddForce(dir);
    }
}
