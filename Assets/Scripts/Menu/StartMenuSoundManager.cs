using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<float> audioDelays;
    [SerializeField] int startedAudio = 0;
    float timeSinceStart = 0f;

    void Update()
    {
        if (startedAudio < audioDelays.Count)
        {
            timeSinceStart += Time.deltaTime;
            for (int i = 0; i < audioDelays.Count; i++)
            {
                if (audioDelays[i] <= timeSinceStart && !audioSources[i].isPlaying)
                {
                    audioSources[i].Play();
                    startedAudio++;
                }
            }
        }

    }
}
