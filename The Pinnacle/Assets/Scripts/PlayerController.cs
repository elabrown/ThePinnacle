using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeedInit = 2.0f; // Movement speed initialise to be reset
    private float movementSpeed;
    private GameBehaviour gameBehaviour; 
    private float gravityValue;

    [Header("Actions")]
    public GameObject fireprojectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject iceblastPrefab;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip fireballClip;
    public AudioClip iceblastClip;
    public AudioClip rayshootClip;
    public AudioClip hitClip;
    public AudioClip keyClip;
    public AudioClip bosskeyClip;  

    //private elements
    private CharacterController characterController;
    private Vector3 move; // Vertical input for moving
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Coroutine healthCoroutine;

    private void Start()
    {
        gameBehaviour = GameObject.Find("GameBehaviour").GetComponent<GameBehaviour>();  
        gravityValue = gameBehaviour.gravityValue;  
        movementSpeed = movementSpeedInit; // Initialise movement speed
        characterController = GetComponent<CharacterController>();   
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        transform.position = new Vector3(0f, 0f, 0f);
        gameBehaviour.StartManaRegen();
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Forward movement
        move = transform.forward * verticalInput;

        // Strafing movement
        Vector3 strafe = transform.right * horizontalInput;

        // Combine forward and strafe movements
        Vector3 combinedMovement = move + strafe;

        // Apply movement
        characterController.Move(combinedMovement * Time.deltaTime * movementSpeed);

        // Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Key"))
        {
            gameBehaviour.AddKey();
            Destroy(hit.gameObject);
            Debug.Log("Player keys: " + gameBehaviour.keys);
            audioSource.PlayOneShot(keyClip);
        }

        else if (hit.gameObject.CompareTag("BossKey"))
        {
            gameBehaviour.ObtainBossKey();
            Destroy(hit.gameObject);
            Debug.Log("Player has boss key");
            audioSource.PlayOneShot(bosskeyClip);
        }

        else if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit");
            // Start the coroutine and store the reference
            if (healthCoroutine == null)
            {
                healthCoroutine = StartCoroutine(DecreaseHealthOverTime());
            }
        }

        else if (hit.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Player hit");
            audioSource.PlayOneShot(hitClip);
            // Start the coroutine and store the reference
            gameBehaviour.TakeDamage(30);
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.CompareTag("Enemy"))
        {
            // Stop the coroutine using the stored reference
            ResetHealthTick();
        }
    }

    public void Fireball()
    {
        if (gameBehaviour.currentmana < 10)
        {
            Debug.Log("Not enough mana!");
            return;
        }
        else
        {
            gameBehaviour.currentmana -= 10;
            Debug.Log("Fireball!");
            audioSource.PlayOneShot(fireballClip);
            GameObject projectile = Instantiate(fireprojectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            return;
        }
    }

    public void RayShoot()
    {
        if (gameBehaviour.currentmana < 5)
        {
            Debug.Log("Not enough mana!");
            return;
        }
        else
        {
            gameBehaviour.currentmana -= 5;
            audioSource.PlayOneShot(rayshootClip);
            Debug.Log("Ray Shoot!");

            // Perform raycast
            RaycastHit hit;
            if (Physics.Raycast(projectileSpawnPoint.position, projectileSpawnPoint.forward, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.collider.gameObject.GetComponent<EnemyBehaviour>() != null)
                    {
                        hit.collider.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(5);
                        Debug.Log("Hit enemy!");
                    }
                    
                }
                else if (hit.collider.CompareTag("Boss"))
                {
                    if (hit.collider.gameObject.GetComponent<EnemyBehaviour>() != null)
                    {
                        hit.collider.gameObject.GetComponent<BossBehaviour>().TakeDamage(5);
                        Debug.Log("Hit boss!");
                    }
                }
            }

            return;
        }
    }

    public void Iceblast()
    {
        if (gameBehaviour.currentmana < 20)
        {
            Debug.Log("Not enough mana!");
            return;
        }
        else
        {
            gameBehaviour.currentmana -= 20;
            Debug.Log("Ice Blast!");
            audioSource.PlayOneShot(iceblastClip);
            GameObject projectile = Instantiate(iceblastPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            return;
        }
    }

    public void ResetHealthTick()
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
            healthCoroutine = null; // Reset the reference
        }
    }

    IEnumerator DecreaseHealthOverTime()
    {
        while (gameBehaviour.currenthealth > 0)
        {
            gameBehaviour.TakeDamage(20);
            audioSource.PlayOneShot(hitClip);
            yield return new WaitForSeconds(1); // Wait for one second
        }
    }
}
