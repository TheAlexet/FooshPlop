using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornsManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI acornsText;
    [SerializeField] Database db;

    // Start is called before the first frame update
    void Start()
    {
        acornsText.text = db.getAcorns().ToString();
    }
}
