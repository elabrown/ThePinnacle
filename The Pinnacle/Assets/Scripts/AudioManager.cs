using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource source1;
    public AudioSource source2;
    private AudioSource currentSource;
    public AudioClip[] state1;
    public AudioClip[] state2;
    public AudioClip[] state3;
    public AudioClip[] musicPlaylist;
    public int currentPhrase;
    public float reverbTail;
    public int currentState;
    public bool endMusic;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Start()
    {
        currentSource = source1;
        musicPlaylist = state1;

        currentSource.clip = musicPlaylist[0];
        currentSource.Play();
    }

    private void FixedUpdate()
    {
        if (currentSource.time > currentSource.clip.length - reverbTail && !endMusic)
        {
            currentPhrase++;

            if (currentPhrase >= musicPlaylist.Length) { currentPhrase = 0; }

            if (currentSource == source1)
            {
                source2.clip = musicPlaylist[currentPhrase];
                currentSource = source2;
            }
            else
            {
                source1.clip = musicPlaylist[currentPhrase];
                currentSource = source1;
            }

            currentSource.Play();

            if (currentState == 5) { endMusic = true; }
        }
    }
    public void SetState(int newState)
    {
        currentState = newState;

        if (newState == 0) { musicPlaylist = state1; }
        else if (newState == 1) { musicPlaylist = state2; }
        else if (newState == 2) { musicPlaylist = state3; }

        currentPhrase = musicPlaylist.Length + 1;
    }
}
