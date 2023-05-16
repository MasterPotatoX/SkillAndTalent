using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effect/New Damage Effect")]
public class DamageEffect : SkillEffect
{
    public float damageAmount;
    public DamageType damageType = DamageType.Normal;

    public override void ApplyEffect()
    {
        Debug.Log("damage to " + target);
        foreach (Character character in target)
        {
            character.SetDescription($"Received {damageAmount} {damageType} damage. Duration: {GetEffectDuration()}");
        }
        
    }
    
}
