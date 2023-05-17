using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effect/New Shield Buff Effect")]
public class ShieldBuff : SkillEffect
{
    public float shieldBuff;
    

    public override void ApplyEffect()
    {

        foreach (Character character in target)
        {
            character.SetDescription($"Received {shieldBuff} shield buff. Duration: {GetEffectDuration()}s");
        }
    }

    public override string GetDetails()
    {
        return $"{name} - {shieldBuff} shield buff";
    }
}
