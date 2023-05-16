using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : ScriptableObject
{
    [Tooltip("To spread effect over time. Leave it as 0 to heal instantinously")]
    [SerializeField]private float _effectDuration = 0;
    [SerializeField]private TargetType _targetType = TargetType.Enemies;
    [SerializeField]private bool _isAOE = false;
    [SerializeField]private float _aoeRange = 0;

    [HideInInspector]
    protected List<Character> target;

    public virtual void ApplyEffect()
    {
        Debug.Log("Applying effect in base");
    }

    public virtual void SetCharacters(List<Character> characters)
    {
        target = characters;
    }

    public float GetEffectDuration()
    {
        return _effectDuration;
    }

    public bool GetAOE()
    {
        return _isAOE;
    }

    public float GetAOERange()
    {
        return _aoeRange;
    }

    public TargetType GetTargetType()
    {
        return _targetType;
    }

    public enum DamageType
    {
        Normal,
        Fire,
        Ice,
        Electric
    }

    public enum TargetType
    {
        Self,
        Friendlies,
        Enemies
    }
}
