using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalentButton : MonoBehaviour
{
    public TextMeshProUGUI textTalentName;
    public Image imageBG;
    [HideInInspector]
    public bool isActive = false;
    Talent _talent;
    TalentManager _manager;

       public void SetTalent(Talent talent, TalentManager manager)
    {
        _talent = talent;
        _manager = manager;
        textTalentName.text = _talent.talentName;
        imageBG.color = Color.gray;
    }

    public void TalentSelected()
    { 
        if(isActive)
        {
            isActive = false;
            imageBG.color = Color.gray;
            StatusGlobal.instance.UpdateStatus($"Talent: {_talent.talentName} de-activated.");
            _manager.TalentDeActivated(_talent);
            
        }
        else
        {
            isActive= true;
            imageBG.color = Color.green;
            StatusGlobal.instance.UpdateStatus($"Talent: {_talent.talentName} activated.");
            _manager.TalentActivated(_talent);
        }
    }

}
