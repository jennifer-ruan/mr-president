using UnityEngine;
using UnityEngine.UI;
public class LevelProgress : MonoBehaviour
{
    [SerializeField]
    private Image stripe1;
    [SerializeField]
    private Image stripe2;
    [SerializeField]
    private Image stripe3;
    [SerializeField]
    private GameObject president;
    [SerializeField]
    private GameObject finalWaypoint;
    private float totalDistance;

    void Start()
    {
        totalDistance = Vector3.Distance(finalWaypoint.transform.position, president.transform.position);
        stripe1.fillAmount = 0f;
        stripe2.fillAmount = 0f;
        stripe3.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float ratioCompleteness = 1 - (Vector3.Distance(finalWaypoint.transform.position, president.transform.position) / totalDistance);
        stripe1.fillAmount = stripe2.fillAmount = stripe3.fillAmount = ratioCompleteness;
    }
}
