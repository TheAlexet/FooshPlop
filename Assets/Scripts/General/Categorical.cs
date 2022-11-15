using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categorical : MonoBehaviour
{
    public int Choice(List<float> probs)
    {
        // Randomly returns one indice of probs
        probs = RescaleProbs(probs);

        float P = Random.Range(0f, 1f);

        float cumulativeProbs = 0f;
        for (int i = 0; i < probs.Count; i++)
        {
            cumulativeProbs += probs[i];
            if (cumulativeProbs >= P) { return i; }
        }
        return probs.Count - 1;
    }

    private List<float> RescaleProbs(List<float> probs)
    {
        // Ensures that probs sums to 1

        float sumProbs = 0f;
        foreach (float p in probs) { sumProbs += p; }
        for (int i = 0; i < probs.Count; i++) { probs[i] /= sumProbs; }
        return probs;
    }
}
