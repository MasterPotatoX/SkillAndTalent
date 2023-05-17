using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Talent : ScriptableObject
{
    public string talentName;
    public string talentDescription;
    public Skill[] skillsAffected;
    public int manaCostDifference;
    public float cooldownTimeDifference;
    public SkillEffect[] skillEffects;

    List<SkillEffect> effects;

    public Talent()
    {
        effects = new List<SkillEffect>();
        for (int i = 0; i < skillEffects.Length; i++)
        {
            effects.Add(skillEffects[i]);
        }
    }

    public void ActivateTalent()
    {
        for (int i = 0; i < skillsAffected.Length; i++)
        {
            skillsAffected[i].ChangeCooldownTime(cooldownTimeDifference);
            skillsAffected[i].ChangeManaCost(manaCostDifference);
        }
    }


}
