using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agent_movement : MonoBehaviour
{

    public float horizontalinput;//水平参数
    public float verticalinput;//垂直参数
    float speed = 10.0f;//声明一个参数，没有规定
    public double speedmultiple;
    public float personalspace;

    public AudioSource randomSound;
    public AudioClip[] audioSources;
    public GameObject target;
    public GameObject president;
    private Vector3 targetpos;
    public float jumpAmount = 5;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MoveTarget");
        president = GameObject.Find("President");
        targetpos = target.transform.position;
        rb = GetComponent<Rigidbody>();
        speedmultiple = 20;
        personalspace = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        targetpos = target.transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("addJumpForce", Vector3.Distance(transform.position, targetpos) * Vector3.Distance(transform.position, targetpos) * 0.02f);
        }

        speed = (float)(System.Math.Sqrt((double)Vector3.Distance(transform.position, targetpos)) * speedmultiple);
        Debug.DrawLine(transform.position, targetpos + new Vector3(0, 1, 0), Color.white, 100f, false);

        Vector3 movement = Vector3.MoveTowards(transform.position, targetpos, Time.deltaTime * speed);
        Vector3 angle = transform.position - president.transform.position;
        // If the agents are outside the president's 'personal space'
        if (!(Vector3.Distance(movement, president.transform.position) < personalspace))
        {
            rb.MovePosition(movement);
        }
        else
        {
            rb.MovePosition(transform.position + (angle * Time.deltaTime * speed));
            // transform.Translate(angle * Time.deltaTime * speed);
        }

        transform.forward = angle;

    }

    void addJumpForce()
    {
        rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
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
}
