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
    public int damageAmount = 0;

    // Heal
    [HideInInspector] public int healAmount = 0;

    // Buff
    [HideInInspector] public float buffDuration = 0;
    [HideInInspector] public float buffMultiplier = 0;

    // Debuff
    [HideInInspector] public float debuffDuration = 0;
    [HideInInspector] public float debuffMultiplier = 0;

    // Stun
    [HideInInspector] public float stunDuration = 0;

    // Slow
    [HideInInspector] public float slowDuration = 0;
    [HideInInspector] public float slowPercentage = 0;

    // Silence
    [HideInInspector] public float silenceDuration = 0;

    // Knockback
    [HideInInspector] public float knockbackForce = 0;

    // Knockup
    [HideInInspector] public float knockupForce = 0;

    // Root
    [HideInInspector] public float rootDuration = 0;

    // Disarm
    [HideInInspector] public float disarmDuration = 0;

    // Shield
    [HideInInspector] public int shieldAmount = 0;
    [HideInInspector] public float shieldDuration = 0;

    // Stealth
    [HideInInspector] public float stealthDuration = 0;

    // AoE
    [HideInInspector] public float aoeRadius = 0;

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

