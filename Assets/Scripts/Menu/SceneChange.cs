using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;

    public void ChangeScene(int sceneID)
    {
        buttonSound.Play();
        SceneManager.LoadScene(sceneID);
    }
}
