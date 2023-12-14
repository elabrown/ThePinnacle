using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    private GameBehaviour _gameBehaviour;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public void Awake()
    {
        _gameBehaviour = GameObject.Find("GameBehaviour").GetComponent<GameBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the Player
        if (other.CompareTag("Playercharacter"))
        {
            // Access the Player's mana component and increase the mana by 20
            _gameBehaviour.RegainHealth(20);
            audioSource.PlayOneShot(audioClip);

            // Destroy the potion object
            Destroy(gameObject);
        }
    }
}