using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Gameplay")]
    private int maxhealth = 100;
    public int currenthealth;
    public int keys;

    [Header("Movement")]
    [SerializeField] private float movementSpeedInit = 2.0f; // Movement speed initialise to be reset
    private float movementSpeed;
    [SerializeField] private float rotationSpeed = 100.0f; // Rotation speed
    [SerializeField] private float jumpHeight = 1.0f;
    private GameBehaviour gameBehaviour; 
    private float gravityValue;

    [Header("UI")]
    public Slider healthbar;
    public GameObject Key1;
    //private elements
    private CharacterController characterController;
    private Vector3 move; // Vertical input for moving
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private void Start()
    {
        gameBehaviour = GameObject.Find("GameBehaviour").GetComponent<GameBehaviour>();  
        gravityValue = gameBehaviour.gravityValue;  
        movementSpeed = movementSpeedInit; // Initialise movement speed
        characterController = GetComponent<CharacterController>();   
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        transform.position = new Vector3(0f, 0f, 0f);

        currenthealth = maxhealth;
        healthbar.value = (float)(currenthealth / maxhealth);
        keys = 0;
    }

    private void Update()
    {
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }

        // Use vertical input for forward/backward movement
        float verticalInput = Input.GetAxis("Vertical");
        move = transform.forward * verticalInput;
        characterController.Move(move * Time.deltaTime * movementSpeed);

        // Change the facing direction based on horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            // Rotate the character around the y-axis
            transform.Rotate(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
        }

        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        // }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Key"))
        {
            keys += 1;
            Destroy(hit.gameObject);
            Debug.Log("Player keys: " + keys);
            Key1.SetActive(true);
        }
        // if (hit.gameObject.CompareTag("Projectile"))
        // {
        //     currenthealth -= 10;
        //     Debug.Log("Player health: " + currenthealth);
        //     healthbar.value = (float)(currenthealth / maxhealth);
        // }
    }
}
