using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class TalentManager : MonoBehaviour
{
    public Talent[] talents;
    public GameObject talentButton;
    public Transform talentsHolder;
    public TextMeshProUGUI textTalentDetail;

    List<Talent> activeTalents = new List<Talent>();

    private void Start()
    {
        SetupTalents();
    }

    private void SetupTalents()
    {
        for (int i = 0; i < talents.Length; i++)
        {
            GameObject gSkill = Instantiate(talentButton);
            gSkill.GetComponent<TalentButton>().SetTalent(talents[i], this);
            gSkill.transform.SetParent(talentsHolder, false);
        }
    }

    public void TalentActivated(Talent talent)
    {
        activeTalents.Add(talent);
        SetTalentDetails(talent);
        
    }

    public void TalentDeActivated(Talent talent)
    {
        activeTalents.Remove(talent);
        SetTalentDetails(talent);
    }

    public List<Talent> GetRelatedTalents(Skill skill)
    {
        Debug.Log("t" + activeTalents.Count);
        List<Talent> relatedTalents = new List<Talent>();

        foreach (var talent in activeTalents)
        {
            List<SkillTalent> activeSkill = new List<SkillTalent>();
            activeSkill = talent.skill;
            foreach (var aSkill in activeSkill)
            {
                Debug.Log(skill + " - " + aSkill.affectedSkill);
                if(aSkill.affectedSkill == skill)
                {
                    relatedTalents.Add(talent);
                }
            }

        }

        return relatedTalents;
    }

    private void SetTalentDetails(Talent talent)
    {
        textTalentDetail.text = $"Talent Details\n" +
            $"Talent Name: {talent.talentName}\n" +
            $"Desc: {talent.talentDescription}";

        string sb = "";
        if (talent.manaCostModifier != 0)
            sb += $"\nMana Modifier: {talent.manaCostModificationType} : {talent.manaCostModifier}";
        if (talent.cooldownTimeModifier != 0)
            sb += $"\nCooldown Time Modifier: {talent.cooldownTimeModificationType} : {talent.cooldownTimeModifier}";

        if(talent.skill.Count > 0)
        {
            for (int i = 0; i < talent.skill.Count; i++)
            {
                
                sb += $"\n\nSkill Affected: {talent.skill[i].affectedSkill.skillName} {talent.skill[i].talentEffect.Count}";

                for (int j = 0; j < talent.skill[i].talentEffect.Count; j++)
                {
                    switch (talent.skill[i].talentEffect[j].effectType.ToString())
                    {
                        case "Damage":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].damageModificationType} : {talent.skill[i].talentEffect[j].damageAmount})";
                            break;
                        case "Heal":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].healModificationType} : {talent.skill[i].talentEffect[j].healAmount})";
                            break;
                        case "Buff":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].buffDurationModificationType} : {talent.skill[i].talentEffect[j].buffDuration} , {talent.skill[i].talentEffect[j].buffMultiplierModificationType} :  {talent.skill[i].talentEffect[j].buffMultiplier})";
                            break;
                        case "Debuff":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].debuffModificationType} : {talent.skill[i].talentEffect[j].debuffDuration} , {talent.skill[i].talentEffect[j].debuffMultiplierModificationType} : {talent.skill[i].talentEffect[j].debuffMultiplier})";
                            break;
                        case "Stun":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].stunDurationModificationType} : {talent.skill[i].talentEffect[j].stunDuration})";
                            break;
                        case "Slow":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].slowDurationModificationType} : {talent.skill[i].talentEffect[j].slowDuration} , {talent.skill[i].talentEffect[j].slowPercentageModificationType} : {talent.skill[i].talentEffect[j].slowPercentage})";
                            break;
                        case "Silence":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].silenceDurationModificationType} : {talent.skill[i].talentEffect[j].silenceDuration})";
                            break;
                        case "Knockback":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].knockbackForceModificationType} :{talent.skill[i].talentEffect[j].knockbackForce})";
                            break;
                        case "Knockup":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].knockupForce} : {talent.skill[i].talentEffect[j].knockupForce})";
                            break;
                        case "Root":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].rootDurationModificationType} : {talent.skill[i].talentEffect[j].rootDuration})";
                            break;
                        case "Disarm":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].disarmDurationModificationType} : {talent.skill[i].talentEffect[j].disarmDuration})";
                            break;
                        case "Shield":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].shieldAmountModificationType} : {talent.skill[i].talentEffect[j].shieldAmount} , {talent.skill[i].talentEffect[j].shieldDurationModificationType} : {talent.skill[i].talentEffect[j].shieldDuration})";
                            break;
                        case "Stealth":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].stealthDurationModificationType} : {talent.skill[i].talentEffect[j].stealthDuration})";
                            break;
                        case "AoE":
                            sb += $"\n{talent.skill[i].talentEffect[j].effectType} ({talent.skill[i].talentEffect[j].aoeRadiusModificationType} : {talent.skill[i].talentEffect[j].aoeRadius})";
                            break;
                        default:
                            break;
                    }

                   
                }

            }
        }

        textTalentDetail.text += sb ;
    }
}
