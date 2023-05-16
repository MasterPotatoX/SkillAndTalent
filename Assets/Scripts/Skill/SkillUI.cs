using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;

    private Skill _skill;
    private SkillManager _manager;

    public void SetSkill(Skill skill, SkillManager manager)
    {
        _skill= skill;
        _manager= manager;
        textName.text = skill.GetSkillName();
    }

    public void ExecuteSkill()
    {
        _manager.ExecuteSkill(_skill);
    }
}
