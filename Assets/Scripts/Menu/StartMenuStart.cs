using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuStart : MonoBehaviour
{
    [SerializeField] AudioSource audioStart;
    [SerializeField] List<ParticleSystem> bubbleParticles;
    public int numLevels; // make it global
    public GameObject DatabaseAccess;

    void Awake()
    {
        UpdatePlayerPrefs();
        DontDestroyOnLoad(GameObject.Instantiate(DatabaseAccess));
    }

    bool isStarting = false;
    public float delayAfterStart = 3f;
    float timeAfterStart = 0f;

    public void StartAnimation()
    {
        audioStart.Play();
        foreach (ParticleSystem p in bubbleParticles)
        {
            p.Play();
        }
        isStarting = true;
    }

    void Update()
    {
        if (isStarting)
        {
            timeAfterStart += Time.deltaTime;
            if (timeAfterStart >= delayAfterStart)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    void UpdatePlayerPrefs()
    {
        float lastConnection = Database.GetLastConnection();
        float timeSinceLastConnection = Time.time - lastConnection;

        for (int i = 0; i < numLevels; i++)
        {
            Database.IncrAccessTimeArea($"level{i}", -timeSinceLastConnection);
        }
    }

}
