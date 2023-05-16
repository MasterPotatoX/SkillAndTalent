using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effect/New Heal Effect")]

public class HealEffect : SkillEffect
{
    public float healAmount;


    public override void ApplyEffect()
    {
    
        Debug.Log("Applying Heal effect");
    }
}
