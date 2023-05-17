using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : SkillEffect
{
    public float stunDuration = 3;

    public override void ApplyEffect()
    {

        foreach (Character character in target)
        {
            character.SetDescription($"Received {stunDuration} seconds stun.");
        }
    }

    public override string GetDetails()
    {
        return $"{name} - {stunDuration} seconds stun";
    }
}
