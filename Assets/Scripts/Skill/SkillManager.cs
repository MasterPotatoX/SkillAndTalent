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

    TalentManager _talentManager;

    private void Start()
    {
        _talentManager = FindObjectOfType<TalentManager>();
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
        textSkillDetails.text = $"{skill.skillName} \n{skill.skillDescription}\n" +
            $"\nMana Cost: {skill.manaCost}" +
            $"\nCooldown Time: {skill.cooldownTime}\n";

        string skillEffects = "";

        foreach (var effect in skill.skillEffects)
        {
            switch (effect.effectType)
            {
                case SkillEffectType.None:
                    break;
                case SkillEffectType.Damage:
                    skillEffects += $"\nDamage:{effect.damageAmount}";
                    break;
                case SkillEffectType.Heal:
                    skillEffects += $"\nHeal:{effect.healAmount}";
                    break;
                case SkillEffectType.Buff:
                    skillEffects += $"\nBuff:{effect.buffMultiplier} for {effect.buffDuration}s";
                    break;
                case SkillEffectType.Debuff:
                    skillEffects += $"\nDebuff:{effect.debuffMultiplier} for {effect.debuffDuration}s";
                    break;
                case SkillEffectType.Stun:
                    skillEffects += $"\nStun:{effect.stunDuration}";
                    break;
                case SkillEffectType.Slow:
                    skillEffects += $"\nSlow:{effect.slowPercentage} for {effect.slowDuration}s";
                    break;
                case SkillEffectType.Silence:
                    skillEffects += $"\nSilence:{effect.silenceDuration}s";
                    break;
                case SkillEffectType.Knockback:
                    skillEffects += $"\nKnockback:{effect.knockbackForce}";
                    break;
                case SkillEffectType.Knockup:
                    skillEffects += $"\nKnockup:{effect.knockupForce}";
                    break;
                case SkillEffectType.Root:
                    skillEffects += $"\nRoot:{effect.rootDuration}s";
                    break;
                case SkillEffectType.Disarm:
                    skillEffects += $"\nDisarm:{effect.disarmDuration}s";
                    break;
                case SkillEffectType.Shield:
                    skillEffects += $"\nShield:{effect.shieldAmount} for {effect.shieldDuration}s";
                    break;
                case SkillEffectType.Stealth:
                    skillEffects += $"\nStealth:{effect.stealthDuration}s";
                    break;
                case SkillEffectType.AoE:
                    skillEffects += $"\nAOE Radius:{effect.aoeRadius}";
                    break;
                default:
                    break;
            }

        }
        textSkillDetails.text += skillEffects;

        GetRelatedTalents(skill);
    }

    private void GetRelatedTalents(Skill skill)
    {
        List<Talent> relatedTalents = new List<Talent>();
        relatedTalents = _talentManager.GetRelatedTalents(skill);

        string strTalents = "\nAffected by talents:";

        foreach (var talent in relatedTalents)
        {
            strTalents += $"\n{talent.talentName} - {talent.talentDescription}";
        }

        textSkillDetails.text += strTalents;
    }

    
}
