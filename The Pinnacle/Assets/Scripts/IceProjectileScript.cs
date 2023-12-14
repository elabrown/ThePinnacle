using UnityEngine;

public class IceProjectileScript : MonoBehaviour
{
    public float speed = 7f;
    public float lifetime = 6f;
    private Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after a set time
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour enemyBehaviour = other.gameObject.GetComponentInParent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.StartCoroutine("SlowDownEnemy");
                Destroy(gameObject);
                
            }
        }
    }
}
