using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class StatusGlobal : MonoBehaviour
{
    public static StatusGlobal instance;
    public TextMeshProUGUI textStatus;

    private void Awake()
    {
        instance= this;
    }

    public void UpdateStatus(string status)
    {
        textStatus.text = status;
    }


}
