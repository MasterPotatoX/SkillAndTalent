using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TalentEffect 
{

    [HideInInspector] public SkillEffectType effectType;

    // Damage
    [HideInInspector] public ModificationType damageModificationType;
     [HideInInspector] public int damageAmount;

    // Heal
    [HideInInspector] public ModificationType healModificationType;
    [HideInInspector] public int healAmount;

    // Buff
    [HideInInspector] public ModificationType buffDurationModificationType;
    [HideInInspector] public float buffDuration;
    [HideInInspector] public ModificationType buffMultiplierModificationType;
    [HideInInspector] public float buffMultiplier;

    // Debuff
    [HideInInspector] public ModificationType debuffModificationType;
    [HideInInspector] public float debuffDuration;
    [HideInInspector] public ModificationType debuffMultiplierModificationType;
    [HideInInspector] public float debuffMultiplier;

    // Stun
    [HideInInspector] public ModificationType stunDurationModificationType;
    [HideInInspector] public float stunDuration;

    // Slow
    [HideInInspector] public ModificationType slowDurationModificationType;
    [HideInInspector] public float slowDuration;
    [HideInInspector] public ModificationType slowPercentageModificationType;
    [HideInInspector] public float slowPercentage;

    // Silence
    [HideInInspector] public ModificationType silenceDurationModificationType;
    [HideInInspector] public float silenceDuration;

    // Knockback
    [HideInInspector] public ModificationType knockbackForceModificationType;
    [HideInInspector] public float knockbackForce;

    // Knockup
    [HideInInspector] public ModificationType knockupForceModificationType;
    [HideInInspector] public float knockupForce;

    // Root
    [HideInInspector] public ModificationType rootDurationModificationType;
    [HideInInspector] public float rootDuration;

    // Disarm
    [HideInInspector] public ModificationType disarmDurationModificationType;
    [HideInInspector] public float disarmDuration;

    // Shield
    [HideInInspector] public ModificationType shieldAmountModificationType;
    [HideInInspector] public int shieldAmount;
    [HideInInspector] public ModificationType shieldDurationModificationType;
    [HideInInspector] public float shieldDuration;

    // Stealth
    [HideInInspector] public ModificationType stealthDurationModificationType;
    [HideInInspector] public float stealthDuration;

    // AoE
    [HideInInspector] public ModificationType aoeRadiusModificationType;
    [HideInInspector] public float aoeRadius;

    
}

[System.Serializable]
public class SkillTalent
{
    [SerializeField] public Skill affectedSkill;
    public List<TalentEffect> talentEffect;
}
