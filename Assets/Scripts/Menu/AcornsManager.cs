using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornsManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI acornsText;
    private DatabaseAccess db;

    private void Awake() { db = new DatabaseAccess(); }

    // Start is called before the first frame update
    void Start()
    {
        acornsText.text = db.getAcorns().ToString();
    }
}
