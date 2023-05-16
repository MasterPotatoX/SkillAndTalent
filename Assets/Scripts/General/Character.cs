using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    [SerializeField]private float _distanceFromPlayer = 0;
    [SerializeField] private TextMeshProUGUI _textInfo;

    private string _description;

    public void SetDescription(string description)
    {
        _description += "\n" + description;
        _description = _description.Trim();
        _textInfo.text = _description;
    }

    public void ClearDescription()
    {
        _description= string.Empty;
        _textInfo.text = string.Empty;
    }

    public float GetDistanceFromPlayer()
    {
        return _distanceFromPlayer;
    }
}
