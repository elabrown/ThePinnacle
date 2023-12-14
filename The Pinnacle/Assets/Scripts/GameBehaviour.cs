using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    public float gravityValue = -9.81f;

    [Header("Gameplay")]
    private int maxhealth = 100;
    private int _currenthealth;
    public int currenthealth
    {
        get { return _currenthealth; }
        set
        {
            _currenthealth = value;
            healthbar.value = (float)(_currenthealth / (float)maxhealth);
        }
    }

    private int maxmana = 100;
    private int _currentmana;
    public int currentmana
    {
        get { return _currentmana; }
        set
        {
            _currentmana = value;
            manabar.value = (float)(_currentmana / (float)maxmana);
        }
    }
    public SceneBehaviour sceneBehaviour;

    public int keys;
    public bool BossKey;

    [Header("UI")]
    public Slider healthbar;
    public Slider manabar;
    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;
    public GameObject BossKeyUI;
    public GameObject QuitGameButton;
    public GameObject QuitGameText;

    private bool isPaused = false;
    void Start()
    {
        currenthealth = maxhealth;
        currentmana = maxmana;
        keys = 0;
        sceneBehaviour = GameObject.Find("SceneBehaviour").GetComponent<SceneBehaviour>();
    }

    public void GamePause()
    {
        isPaused = true;
        Time.timeScale = 0;
        QuitGameButton.SetActive(true);
        QuitGameText.SetActive(true);
    }

    void GameResume()
    {
        isPaused = false;
        Time.timeScale = 1;
        QuitGameButton.SetActive(false);
        QuitGameText.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                GamePause();
            else
                GameResume();
        }
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        Debug.Log("Player health: " + currenthealth);
        if (currenthealth <= 0)
        {
            Debug.Log("Player died");
            sceneBehaviour.LoadSceneByIndex(2);
        }
    }
    public void AddKey()
    {
        keys += 1;
        Debug.Log("Player keys: " + keys);
        if (Key1.activeSelf == false)
            Key1.SetActive(true);
        else if (Key2.activeSelf == false)
            Key2.SetActive(true);
        else if (Key3.activeSelf == false)
            Key3.SetActive(true);
    }
    public void RemoveKey()
    {
        keys -= 1;
        Debug.Log("Player keys: " + keys);
        if (Key3.activeSelf == true)
            Key3.SetActive(false);
        else if (Key2.activeSelf == true)
            Key2.SetActive(false);
        else if (Key1.activeSelf == true)
            Key1.SetActive(false);
    }

    public void ObtainBossKey()
    {
        BossKey = true;
        Debug.Log("Player has boss key");
        BossKeyUI.SetActive(true);
    }

    public void RegainMana(int amount)
    {
        if (currentmana < maxmana - amount)
            currentmana += amount;
        else
            currentmana = maxmana;
        Debug.Log("Player mana: " + currentmana);
    }
    public void StartManaRegen()
    {
        StartCoroutine(ManaRegen());
    }
        public void RegainHealth(int amount)
    {
        if (currenthealth < maxhealth - amount)
            currenthealth += amount;
        else
            currenthealth = maxhealth;
        Debug.Log("Player health: " + currenthealth);
    }
    IEnumerator ManaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            RegainMana(5);
        }
    }
}