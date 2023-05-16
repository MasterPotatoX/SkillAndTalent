using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effect/New Area Of Effect")]
public class AOE : SkillEffect
{
    [SerializeField] private SkillEffect[] _effects;
    [SerializeField] private float effectRadius = 0;
    

    public override void ApplyEffect()
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            _effects[i].ApplyEffect();
        }
    }

    public SkillEffect[] GetSkillEffects()
    {
        return _effects;
    }

    
}
