using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class SkillButton : MonoBehaviour
{
    public TextMeshProUGUI textSkillName;

    float _currentCooldown = 0;

    Skill _skill;

    SkillManager _manager;

    public void SetSkill(Skill skill, SkillManager manager)
    {
        _skill= skill;
        _manager = manager;
        textSkillName.text = _skill.skillName;
    }

    private void UpdateCooldownTime()
    {
        if (_currentCooldown > 0)
        {
            textSkillName.text = _currentCooldown.ToString();
            _currentCooldown -= 1;
            Invoke("UpdateCooldownTime", 1);
        }
        else
        {
            textSkillName.text = _skill.skillName;
        }
            
        
    }

    public void SkillSelected()
    {
        if(_currentCooldown > 0)
        {
            StatusGlobal.instance.UpdateStatus($"Skill {_skill.skillName} can't be selected, is in cooldown state.");
            return;
        }
        StatusGlobal.instance.UpdateStatus($"Skill: {_skill.skillName} selected.");
        _manager.SetSkillDetails(_skill);
        _currentCooldown = _skill.cooldownTime;
        CancelInvoke("UpdateCooldownTime");
        UpdateCooldownTime();
    }
}
