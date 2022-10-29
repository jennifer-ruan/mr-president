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
        // AvoidCrossWall();
        detect(Vector3.forward, Vector3.back);
        detect(Vector3.back, Vector3.forward);
        detect(Vector3.left, Vector3.right);
        detect(Vector3.right, Vector3.left);
        // rotate around y axis
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        }
    }
    
    void detect(Vector3 dir, Vector3 inverse_dir)
    {
        RaycastHit hitInfo;
        Vector3 _dir = Camera.main.transform.TransformDirection(dir);
        if (Physics.SphereCast(Camera.main.transform.position, 0.5f, _dir, out hitInfo, 0.5f))
        {
            float dis = hitInfo.distance;
            Vector3 correction = Vector3.Normalize(Camera.main.transform.TransformDirection(inverse_dir)) * dis;
            correction.z = 0f;
            correction.y = 0f;
            Camera.main.transform.position += correction;
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
