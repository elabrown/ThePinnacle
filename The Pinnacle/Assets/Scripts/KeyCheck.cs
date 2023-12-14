using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    public GameBehaviour gameBehaviour;
    public GameObject door;
    public GameObject bossDoorL;
    public GameObject bossDoorR;
    public bool isBossDoor;
    public AudioClip audioClip;
    public AudioSource audioSource;
    private new Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Playercharacter"))
        {
            if (gameBehaviour.keys > 0 && !isBossDoor)
            {
                Debug.Log("Player has opened door");
                gameBehaviour.RemoveKey();
                door.SetActive(false);
                audioSource.PlayOneShot(audioClip);
                Destroy(gameObject);
            }

            else if (gameBehaviour.BossKey == true && isBossDoor)
            {
                Debug.Log("Player has opened boss door");
                gameBehaviour.BossKey = false;
                bossDoorL.transform.rotation = Quaternion.Euler(0, 90, 0);
                bossDoorR.transform.rotation = Quaternion.Euler(0, 270, 0);
                audioSource.PlayOneShot(audioClip);
                Destroy(gameObject);
            }
        }
    }
}
