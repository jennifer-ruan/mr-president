using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agent_movement : MonoBehaviour
{

    public float horizontalinput;//水平参数
    public float verticalinput;//垂直参数
    float speed=10.0f;//声明一个参数，没有规定
    public double speedmultiple;
    public float distancefromprez;
    

    public AudioSource randomSound;
    public AudioClip[] audioSources;
    public GameObject target;
    public GameObject president;
    private Vector3 targetpos;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MoveTarget");
        president = GameObject.Find("President");
        targetpos = target.transform.position;
        rb = GetComponent<Rigidbody>();
        speedmultiple = 5;
        distancefromprez = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        /*horizontalinput = Input.GetAxis("Vertical"); //AD
        verticalinput = Input.GetAxis("Horizontal"); // WS
 
        if (horizontalinput!=0&&verticalinput!=0)
        {
            horizontalinput = horizontalinput * 0.6f;
            verticalinput = verticalinput * 0.6f;
        }
        //WS方向控制
        this.transform.Translate(Vector3.right * horizontalinput * Time.deltaTime * speed * -1);
        // 侧方
        this.transform.Translate(Vector3.forward*  verticalinput * Time.deltaTime * speed);
        // 前后*/

        targetpos = target.transform.position;
        speed = (float)(System.Math.Sqrt((double)Vector3.Distance(transform.position, targetpos))*speedmultiple);
        Debug.DrawLine(transform.position, targetpos + new Vector3(0,1,0), Color.white, 100f, false);

        Vector3 movement = Vector3.MoveTowards(transform.position, targetpos, Time.deltaTime * speed);
        Vector3 angle = transform.position - president.transform.position;
        // If the agents are outside the president's 'personal space'
        if (!(Vector3.Distance(movement, president.transform.position) < distancefromprez))
        {
            transform.position = movement;
        }
        else
        {
            // rigidbody.AddRelativeForce(direction.normalized * Time.deltaTime * speed, ForceMode.Force);
            transform.Translate(angle * Time.deltaTime);
        }

        transform.right = -(angle);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Harmful") {

            randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            randomSound.Play ();
            
            Debug.Log(gameObject.name);
            Debug.Log("agent is hit");
            Destroy(gameObject);
        }
    }
}
