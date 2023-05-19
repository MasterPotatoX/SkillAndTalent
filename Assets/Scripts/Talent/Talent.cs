using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Talent", menuName = "Skills | Talent/Talent")]
public class Talent : ScriptableObject
{
    public string talentName;
    public string talentDescription;
    public ModificationType manaCostModificationType;
    public int manaCostModifier;
    public ModificationType cooldownTimeModificationType;
    public float cooldownTimeModifier;

    [SerializeField] public List<SkillTalent> skill;
}

public enum ModificationType
{
    Multiplier,
    Addition,
    Replacement
}
