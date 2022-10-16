using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class agent_movement : MonoBehaviour
{

    public float horizontalinput;
    public float verticalinput;
    float speed = 10.0f;
    public float speedmultiple;
    public float dragVar = 5f;
    float rotationSpeed = 45;

    public AudioSource randomSound;
    public AudioClip[] audioSources;
    public GameObject target;
    public GameObject president;
    private Vector3 targetpos;
    public Vector3 currentEulerAngles;
    public float jumpAmount = 5;
    public bool isGettingDown = false;
    
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
            if (Input.GetKeyDown(KeyCode.G))
            {
                isGettingDown = true;
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
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
        float angle = rotationSpeed * 10;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
        yield return new WaitForSeconds(2f);
        isGettingDown = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Harmful")
        {

            // randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            AudioSource.PlayClipAtPoint(audioSources[Random.Range(0, audioSources.Length)], transform.position);
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
        speed = distance * distance * 8 * speedmultiple;
        Debug.DrawLine(transform.position, targetpos + new Vector3(0, 1, 0), Color.white, 100f, false);

        Vector3 angle = transform.position - president.transform.position;

        rb.drag = dragVar / distance;
        rb.AddForce(targetpos - transform.position);
        // Debug.Log("force added");

        Debug.Log("NONONONONN");
        transform.forward = angle;
    }
    public void Push(Vector3 dir)
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.AddForce(dir);
    }
}
