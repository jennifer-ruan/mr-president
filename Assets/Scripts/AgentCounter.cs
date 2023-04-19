using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AgentCounter : MonoBehaviour
{



    Transform agents;
    public GameObject placehold;



    // Start is called before the first frame update
    void Start()
    {
        placehold = GameObject.Find("AgentsLeft");
        agents = GameObject.Find("Agents").transform;
        placehold.GetComponent<TMPro.TextMeshProUGUI>().text = "Agents: " + agents.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        placehold.GetComponent<TMPro.TextMeshProUGUI>().text = "Agents: " + agents.childCount;
    }
}
