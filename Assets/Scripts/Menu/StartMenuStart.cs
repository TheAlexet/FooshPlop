using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuStart : MonoBehaviour
{
    [SerializeField] AudioSource audioStart;
    [SerializeField] List<ParticleSystem> bubbleParticles;

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
}
