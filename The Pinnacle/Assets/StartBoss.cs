using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public BossBehaviour bossBehaviour;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Playercharacter"))
        {
            bossBehaviour.StartBossFight();
        }
    }
}
