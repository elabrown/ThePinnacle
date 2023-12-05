using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public float gravityValue = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GamePause()
    {
        Time.timeScale = 0;
    }
    void GameResume()
    {
        Time.timeScale = 1;
    }
}
