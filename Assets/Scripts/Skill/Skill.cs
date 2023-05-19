using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Skill", menuName = "Skills | Talent/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public int manaCost;
    public float cooldownTime;
    public List<SkillEffect> skillEffects;
    public List<VFX> vfxList;
}




