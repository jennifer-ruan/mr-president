using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{

    public Vector2 turn;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("President");
    }

    // Update is called once per frame
    void Update()
    {
        if (target){
        turn.x += Input.GetAxis("Mouse X");
        //turn.y += Input.GetAxis("Mouse Y");
        AvoidCrossWall();
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        }
    }

    private void AvoidCrossWall()
    {
        RaycastHit hitInfo;
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(Camera.main.transform.position, fwd, out hitInfo, 0.5f))
        {
            float dis = hitInfo.distance;
            Vector3 correction = Vector3.Normalize(Camera.main.transform.TransformDirection(Vector3.back)) * dis;
            Camera.main.transform.position += correction;
        }
    }
}
