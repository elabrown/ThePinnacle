using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public GameObject explosionPrefab;
    public int health;
    public AudioManager audioManager;
    public int maxhealth;
    public Transform fireballSpawnPoint;
    public GameObject fireballPrefab;
    public float launchInterval = 1.0f; // Time interval between launches
    private bool isSlowed = false; // Flag to check if enemy is currently slowed
    public GameObject restartbutton;
    public GameObject restarttext;
    public GameBehaviour gameBehaviour;

    void Start()
    {
        health = maxhealth;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameBehaviour = GameObject.Find("GameBehaviour").GetComponent<GameBehaviour>();
    }
    public void StartBossFight()
    {
        Debug.Log("Boss fight started");
        audioManager.SetState(2);
        StartCoroutine(LaunchFireballs());
    }
    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Boss health: " + health);
        if (health <= 0)
        {
            restartbutton.SetActive(true);
            restarttext.SetActive(true);
            gameBehaviour.GamePause();
            Explode();
        }
    }

    IEnumerator LaunchFireballs()
    {
        while (true)
        {
            yield return new WaitForSeconds(launchInterval);
            LaunchFireball();
        }
    }
    private void LaunchFireball()
    {
        // Generate a random angle within -30 to 30 degrees
        float randomAngle = Random.Range(-60f, 60f);
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0) * transform.rotation;

        // Instantiate the fireball at the parent object's position and with the calculated rotation
        Instantiate(fireballPrefab, fireballSpawnPoint.transform.position, rotation);
    }
    // Coroutine to slow down the enemy's speed by 50%
    IEnumerator SlowDownEnemy()
    {
        if (!isSlowed)
        {
            isSlowed = true;
            launchInterval *= 2f; // Reduce speed by 50%
            yield return new WaitForSeconds(5f); // Slow down for 5 seconds

            launchInterval *= 0.5f; // Restore original speed
            isSlowed = false;
        }
    }
}
