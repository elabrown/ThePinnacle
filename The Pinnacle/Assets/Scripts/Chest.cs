using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject replacementPrefab; // Assign the prefab in the inspector
    public GameObject chestLid;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Playercharacter"))
        {
            if (chestLid != null)
            {
                Destroy(chestLid); // Destroy the current game object
                Instantiate(replacementPrefab, transform.position, transform.rotation); // Instantiate the replacement prefab
            }
        }
    }
}
