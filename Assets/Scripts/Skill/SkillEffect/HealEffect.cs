using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effect/New Heal Effect")]

public class HealEffect : SkillEffect
{
    public float healAmount;


    public override void ApplyEffect()
    {

        foreach (Character character in target)
        {
            character.SetDescription($"Received {healAmount} healing. Duration: {GetEffectDuration()}s");
        }
    }

    public override string GetDetails()
    {
        return $"{name} - {healAmount} healing";
    }
}
