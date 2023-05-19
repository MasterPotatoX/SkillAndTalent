using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillEffect
{
    [HideInInspector]
    public SkillEffectType effectType;


    // Damage
    [HideInInspector]
    public int damageAmount;

    // Heal
    [HideInInspector] public int healAmount;

    // Buff
    [HideInInspector] public float buffDuration;
    [HideInInspector] public float buffMultiplier;

    // Debuff
    [HideInInspector] public float debuffDuration;
    [HideInInspector] public float debuffMultiplier;

    // Stun
    [HideInInspector] public float stunDuration;

    // Slow
    [HideInInspector] public float slowDuration;
    [HideInInspector] public float slowPercentage;

    // Silence
    [HideInInspector] public float silenceDuration;

    // Knockback
    [HideInInspector] public float knockbackForce;

    // Knockup
    [HideInInspector] public float knockupForce;

    // Root
    [HideInInspector] public float rootDuration;

    // Disarm
    [HideInInspector] public float disarmDuration;

    // Shield
    [HideInInspector] public int shieldAmount;
    [HideInInspector] public float shieldDuration;

    // Stealth
    [HideInInspector] public float stealthDuration;

    // AoE
    [HideInInspector] public float aoeRadius;

}

public enum SkillEffectType
{
    None,
    Damage,
    Heal,
    Buff,
    Debuff,
    Stun,
    Slow,
    Silence,
    Knockback,
    Knockup,
    Root,
    Disarm,
    Shield,
    Stealth,
    AoE
}

