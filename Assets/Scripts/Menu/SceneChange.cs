using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int MenuScenesOffset = 3; // number of scenes before first level
    [SerializeField] private AudioSource buttonSound;
    private DatabaseAccess db;

    private void Start()
    {
        db = GameObject.FindGameObjectWithTag("Database")?.GetComponent<DatabaseAccess>();
    }

    public void ChangeScene(int sceneID)
    {
        buttonSound.Play();
        SceneManager.LoadScene(sceneID);
    }

    public void LoadArea(int areaInt)
    {
        if (db.LevelsTime[areaInt] != 0) { ChangeScene(areaInt + MenuScenesOffset); }
    }
}
