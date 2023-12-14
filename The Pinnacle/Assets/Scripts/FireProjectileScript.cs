using UnityEngine;

public class FireProjectileScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
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
                enemyBehaviour.TakeDamage(10);
                Debug.Log("hit");
            }
        }

        else if (other.gameObject.CompareTag("Boss"))
        {
            BossBehaviour bossBehaviour = other.gameObject.GetComponentInParent<BossBehaviour>();
            if (bossBehaviour != null)
            {
                bossBehaviour.TakeDamage(10);
                Debug.Log("hit");
            }
        }
    }
}
