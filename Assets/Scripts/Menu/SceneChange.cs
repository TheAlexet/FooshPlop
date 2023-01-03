using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int MenuScenesOffset = 3; // number of scenes before first level
    [SerializeField] private AudioSource buttonSound;

    public void ChangeScene(int sceneID)
    {
        buttonSound.Play();
        SceneManager.LoadScene(sceneID);
    }

    public void LoadArea(int areaInt)
    {
        if (Database.GetAccessTimeArea($"level{areaInt}") != 0) { ChangeScene(areaInt + MenuScenesOffset); }
    }
}
