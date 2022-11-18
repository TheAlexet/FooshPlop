using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextColorChange : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] Color normalColor;
    [SerializeField] Color pressedColor;
    [SerializeField] float transitionDuration = 2f;
    [SerializeField] float transitionTimeStep = 0.1f;

    bool changeColor = false;
    [SerializeField] float curStep = 0f;


    void Start()
    {
        text.color = normalColor;
    }

    void Update()
    {
        if (changeColor)
        {
            StartCoroutine(ColorTransition());
        }
    }

    IEnumerator ColorTransition()
    {
        for (float i = 0f; i <= transitionDuration; i += transitionTimeStep / transitionDuration)
        {
            text.color = Color.Lerp(normalColor, pressedColor, curStep);
            curStep += transitionTimeStep / transitionDuration;
            yield return new WaitForSeconds(transitionTimeStep);
        }
    }

    public void ChangeColor()
    {
        changeColor = true;
    }
}
