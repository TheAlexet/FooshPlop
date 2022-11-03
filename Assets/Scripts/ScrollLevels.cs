using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollLevels : MonoBehaviour
{
    float scrollPosition = 0;
    float[] pos;
    int maxLevel;

    [SerializeField]
    private Database db;

    [SerializeField]
    private GameObject scrollbar;

    [SerializeField]
    private Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        maxLevel = db.getMaxLevel();
        for(int i = 1; i < gameObject.GetComponentsInChildren<Transform>().Length; i++)
        {
            if(i > maxLevel)
            {
                gameObject.GetComponentsInChildren<Transform>()[i].GetComponent<Image>().color = new Color32(100,100,100,150);
                gameObject.GetComponentsInChildren<Transform>()[i].name = "Level " + i.ToString() + ": Locked";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i = 0; i < pos.Length; i++)
        {
            pos [i] = distance * i;
        }
        if(Input.GetMouseButton (0))
        {
            scrollPosition = scrollbar.GetComponent<Scrollbar> ().value;
        } else {
            for(int i = 0; i < pos.Length; i++)
            {
                if(scrollPosition < pos [i] + (distance / 2) && scrollPosition > pos [i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for(int i = 0; i < pos.Length; i++)
        {
            if(scrollPosition < pos[i] + (distance/2) && scrollPosition > pos[i] - (distance/2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp (transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for(int j = 0; j < pos.Length; j++)
                {
                    if(j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp (transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                    else 
                    {
                        levelText.text = gameObject.GetComponentsInChildren<Transform>()[i + 1].name;
                    }
                }
            }
        }
    }

    public void startLevel(int level)
    {
        if(level <= maxLevel)
        {
            SceneManager.LoadScene("Level " + level.ToString());
        }
    }
}
