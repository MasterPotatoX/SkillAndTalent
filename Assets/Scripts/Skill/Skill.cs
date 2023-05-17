using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField] private string _skillName;
    [SerializeField] private string _description;
    [SerializeField] private int _manaCost;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private SkillEffect[] _effects;
    
    public string GetSkillName()
    {
        return _skillName;
    }
    
    public string GetDescription() 
    {
        return _description; 
    }

    public void ChangeManaCost(int manaCost)
    {
        _manaCost += manaCost;
    }

    public int GetManaCost()
    {
        return _manaCost;
    }

    public void ChangeCooldownTime(float cooldownTime)
    {
        _cooldownTime += cooldownTime;
    }

    public float GetCooldownTime()
    {
        return _cooldownTime;
    }

    public SkillEffect[] GetSkillEffects()
    {
        return _effects;
    }

    

    
}
