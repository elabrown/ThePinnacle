using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectiles : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Rigidbody rb;
    private GameBehaviour gameBehaviour;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after a set time
        rb = GetComponent<Rigidbody>();
        gameBehaviour = GameObject.Find("GameBehaviour").GetComponent<GameBehaviour>();
    }

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

        void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Playercharacter"))
        {
            if (gameBehaviour != null)
            {
                gameBehaviour.TakeDamage(5);
                Debug.Log("hit");
            }
        }
    }
}
