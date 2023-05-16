using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : SkillEffect
{
    public float stunDuration = 3;

    public override void ApplyEffect()
    {

        Debug.Log("Applying Stun effect");
    }
}
