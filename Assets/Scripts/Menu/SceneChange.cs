using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] Database db;
    [SerializeField] TMPro.TextMeshProUGUI acornsText;

    public void ChangeScene(int sceneID)
    {
        int level = sceneID - 1;
        int acorns = (level - 1) * 100;

        if(db.getMaxLevel() >= level)
        {
            SceneManager.LoadScene(sceneID);
        }
        else if(db.getAcorns() >= acorns && level < 3)
        {
            db.setMaxLevel(db.getMaxLevel() + 1);
            db.setAcorns(db.getAcorns() - acorns);
            acornsText.text = db.getAcorns().ToString();
        }
    }
}
