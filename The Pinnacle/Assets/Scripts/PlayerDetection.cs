using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform Player;
    private GameObject parent;
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        parent = transform.parent.gameObject;
        agent = parent.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Playercharacter"))
        {
            Debug.Log("Player detected");
            agent.destination = Player.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Playercharacter"))
        {
            Debug.Log("Player lost");
            agent.destination = parent.GetComponent<EnemyBehaviour>().locations[parent.GetComponent<EnemyBehaviour>().locationIndex].position;
        }
    }
}
