using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Assign all skills here")]
    public Skill[] skills;
    [Space(5)]
    [Header("Setup")]
    public GameObject skillUIPrefab;
    public Transform skillUIHolder;
    public Character[] friendlies, enemies;

    private void Start()
    {
        CreateSkills();
    }

    private void CreateSkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            GameObject goSkill = Instantiate(skillUIPrefab);
            goSkill.GetComponent<SkillUI>().SetSkill(skills[i], this);
            goSkill.transform.SetParent(skillUIHolder, false);
        }
    }

    public void ExecuteSkill(Skill skill)
    {
        Debug.Log("Executing skill: " + skill.GetSkillName());

        SkillEffect[] effects = skill.GetSkillEffects();
        List<Character> characterList = new List<Character>();

        ClearAllDescriptions();

        for (int i = 0; i < effects.Length; i++)
        {
            

            if (effects[i].GetAOE()) //is area of effect skill, so get all enemies in range
            {
                switch (effects[i].GetTargetType())
                {
                    case SkillEffect.TargetType.Self:
                        characterList.Add(friendlies[0]);
                        break;
                    case SkillEffect.TargetType.Friendlies:
                        for (int j = 1; j < friendlies.Length; j++)
                        {
                            if (friendlies[j].GetDistanceFromPlayer() <= effects[i].GetAOERange())
                                characterList.Add(friendlies[j]);
                        }
                        break;
                    case SkillEffect.TargetType.Enemies:
                        for (int j = 0; j < enemies.Length; j++)
                        {
                            if (enemies[j].GetDistanceFromPlayer() <= effects[i].GetAOERange())
                                characterList.Add(enemies[j]);
                        }
                        break;
                }

            }
            else
            {
                switch(effects[i].GetTargetType())
                {
                    case SkillEffect.TargetType.Self:
                        characterList.Add(friendlies[0]);
                        break;
                    case SkillEffect.TargetType.Enemies:
                        characterList.Add(enemies[0]);
                        break;
                }
            }

            effects[i].SetCharacters(characterList);
            effects[i].ApplyEffect();
        }
        
    }

    private void ClearAllDescriptions()
    {
        for (int i = 0; i < friendlies.Length; i++)
        {
            friendlies[i].ClearDescription();
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ClearDescription();
        }
    }
}
