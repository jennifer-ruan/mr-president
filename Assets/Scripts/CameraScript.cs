using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    public GameObject target;
    public float sensitivity;

    void Start()
    {
        target = GameObject.Find("MoveTarget");
    }
    
    void LateUpdate ()
    {
        transform.position = target.transform.position + offset;
    }
    

    void FixedUpdate()
    {
        /*float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        transform.RotateAround(target.transform.position, -Vector3.up, rotateHorizontal * sensitivity);
        transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity);*/
    }
}
