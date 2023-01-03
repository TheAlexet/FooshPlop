using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPlayerPrefs : MonoBehaviour
{
    public int Acorns;

    void Update()
    {
        if (Acorns != Database.getAcorns())
        {
            Database.setAcorns(Acorns);
        }

    }

}
