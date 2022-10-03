using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agent_movement : MonoBehaviour
{

    public float horizontalinput;//水平参数
    public float verticalinput;//垂直参数
    float speed=10.0f;//声明一个参数，没有规定

    public AudioSource randomSound;
    public AudioClip[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalinput = Input.GetAxis("Vertical"); //AD
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
        // 前后

        
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