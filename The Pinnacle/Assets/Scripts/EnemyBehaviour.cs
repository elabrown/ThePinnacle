using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public Transform PatrolRoute;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public List<Transform> locations;
    public GameObject explosionPrefab;
    public int health;
    public int maxhealth;
    public int locationIndex = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private GameBehaviour _gameBehaviour;
    private PlayerController _playerController;
    private bool isSlowed = false; // Flag to check if enemy is currently slowed

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _gameBehaviour = GameObject.Find("GameBehaviour").GetComponent<GameBehaviour>();
        _playerController = GameObject.Find("Character").GetComponent<PlayerController>();
        health = maxhealth;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in PatrolRoute)
        {
            locations.Add(child);
        }
    }

    public void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    public void Explode()
    {
        audioSource.PlayOneShot(audioClip);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        _playerController.ResetHealthTick();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);
        if (health <= 0)
        {
            Explode();
        }
    }

    // Coroutine to slow down the enemy's speed by 50%
    IEnumerator SlowDownEnemy()
    {
        if (!isSlowed)
        {
            isSlowed = true;
            float originalSpeed = agent.speed;
            agent.speed = 0;

            yield return new WaitForSeconds(5f);

            agent.speed = originalSpeed; 
            isSlowed = false;
        }
    }
}
