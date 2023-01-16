using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StartMenuStart : MonoBehaviour
{
    [SerializeField] AudioSource audioStart;
    [SerializeField] List<ParticleSystem> bubbleParticles;
    public GameObject DatabaseAccess;

    void Awake()
    {
        DontDestroyOnLoad(GameObject.Instantiate(DatabaseAccess));
        UpdatePlayerPrefs();
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
        int lastConnection = Database.GetLastConnection();
        int timeSinceLastConnection = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds - lastConnection;

        for (int i = 1; i < DatabaseAccess.GetComponent<DatabaseAccess>().LevelsCount; i++)
        {
            Database.IncrAccessTimeArea($"level{i}", -timeSinceLastConnection);
            Debug.Log(Database.GetAccessTimeArea($"level{i}"));
        }
    }

}
