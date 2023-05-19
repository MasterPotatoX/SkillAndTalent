using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public Skill[] skills;
    public GameObject skillButton;
    public Transform skillsHolder;
    public TextMeshProUGUI textSkillDetails;

    private void Start()
    {
        SetupSkills();
    }

    private void SetupSkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            GameObject gSkill = Instantiate(skillButton);
            gSkill.GetComponent<SkillButton>().SetSkill(skills[i], this);
            gSkill.transform.SetParent(skillsHolder, false);
        }
    }

    public void SetSkillDetails(Skill skill)
    {
        textSkillDetails.text = skill.skillName;
    }

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Skill s = skills[0];
            LogMe(s.skillName);
            LogMe(s.skillDescription);
            LogMe(s.skillEffects.Count.ToString());
            
        }
    }

    private void LogMe(string msg)
    {
        Debug.Log(msg);
    }*/
}
