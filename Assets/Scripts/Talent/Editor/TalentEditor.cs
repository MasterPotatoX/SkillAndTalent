using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Talent))]
public class TalentEditor : Editor
{
    private SerializedProperty talentNameProp;
    private SerializedProperty talentDescriptionProp;
    private SerializedProperty manaCostModificationTypeProp;
    private SerializedProperty manaCostModifierProp;
    private SerializedProperty cooldownTimeModificationTypeProp;
    private SerializedProperty cooldownTimeModifierProp;
    private SerializedProperty skillProp;

    private void OnEnable()
    {
        talentNameProp = serializedObject.FindProperty("talentName");
        talentDescriptionProp = serializedObject.FindProperty("talentDescription");
        manaCostModificationTypeProp = serializedObject.FindProperty("manaCostModificationType");
        manaCostModifierProp = serializedObject.FindProperty("manaCostModifier");
        cooldownTimeModificationTypeProp = serializedObject.FindProperty("cooldownTimeModificationType");
        cooldownTimeModifierProp = serializedObject.FindProperty("cooldownTimeModifier");
        skillProp = serializedObject.FindProperty("skill");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(talentNameProp);
        EditorGUILayout.PropertyField(talentDescriptionProp);
        EditorGUILayout.PropertyField(manaCostModificationTypeProp);
        EditorGUILayout.PropertyField(manaCostModifierProp);
        EditorGUILayout.PropertyField(cooldownTimeModificationTypeProp);
        EditorGUILayout.PropertyField(cooldownTimeModifierProp);

        EditorGUILayout.PropertyField(skillProp, true);

        // Loop through skills
        for (int i = 0; i < skillProp.arraySize; i++)
        {
            SerializedProperty skillElementProp = skillProp.GetArrayElementAtIndex(i);
            SerializedProperty talentEffectProp = skillElementProp.FindPropertyRelative("talentEffect");
            SerializedProperty affectedSkillProp = skillElementProp.FindPropertyRelative("affectedSkill");

            
            string skillName = affectedSkillProp.objectReferenceValue != null ? affectedSkillProp.objectReferenceValue.name : "None";
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Skill: " + skillName, EditorStyles.boldLabel); 
            

            // Loop through talent effects
            for (int j = 0; j < talentEffectProp.arraySize; j++)
            {
                EditorGUILayout.Space();
                SerializedProperty talentEffectElementProp = talentEffectProp.GetArrayElementAtIndex(j);
                SerializedProperty effectTypeProp = talentEffectElementProp.FindPropertyRelative("effectType");

                EditorGUILayout.PropertyField(effectTypeProp);

                SkillEffectType effectType = (SkillEffectType)effectTypeProp.enumValueIndex;

                EditorGUI.indentLevel++;
                switch (effectType)
                {
                    case SkillEffectType.Damage:
                        DrawDamageFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Heal:
                        DrawHealFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Buff:
                        DrawBuffFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Debuff:
                        DrawDebuffFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Stun:
                        DrawStunFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Slow:
                        DrawSlowFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Silence:
                        DrawSilenceFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Knockback:
                        DrawKnockbackFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Knockup:
                        DrawKnockupFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Root:
                        DrawRootFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Disarm:
                        DrawDisarmFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Shield:
                        DrawShieldFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.Stealth:
                        DrawStealthFields(talentEffectElementProp);
                        break;
                    case SkillEffectType.AoE:
                        DrawAoEFields(talentEffectElementProp);
                        break;
                    
                    default:
                        EditorGUILayout.HelpBox("Unsupported effect type.", MessageType.Warning);
                        break;
                }
                EditorGUI.indentLevel--;
            }
            
            if (GUILayout.Button("Add Talent Effect"))
            {
                talentEffectProp.arraySize++;
                SerializedProperty newTalentEffectProp = talentEffectProp.GetArrayElementAtIndex(talentEffectProp.arraySize - 1);
                newTalentEffectProp.FindPropertyRelative("effectType").enumValueIndex = 0; // Set default effect type
                
            }

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawDamageFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty damageModificationProp = talentEffectProp.FindPropertyRelative("damageModificationType");
        SerializedProperty damageAmountProp = talentEffectProp.FindPropertyRelative("damageAmount");

        EditorGUILayout.PropertyField(damageModificationProp);
        EditorGUILayout.PropertyField(damageAmountProp);
    }

    private void DrawHealFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty healAmountProp = talentEffectProp.FindPropertyRelative("healAmount");
        SerializedProperty healAmountPropM = talentEffectProp.FindPropertyRelative("healModificationType");

        EditorGUILayout.PropertyField(healAmountPropM);
        EditorGUILayout.PropertyField(healAmountProp);
    }

    private void DrawBuffFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty buffDurationProp = talentEffectProp.FindPropertyRelative("buffDuration");
        SerializedProperty buffMultiplierProp = talentEffectProp.FindPropertyRelative("buffMultiplier");
        SerializedProperty buffMDurationProp = talentEffectProp.FindPropertyRelative("buffDurationModificationType");
        SerializedProperty buffMMultiplierProp = talentEffectProp.FindPropertyRelative("buffMultiplierModificationType");


        EditorGUILayout.PropertyField(buffMDurationProp);
        EditorGUILayout.PropertyField(buffDurationProp);
        EditorGUILayout.PropertyField(buffMMultiplierProp);
        EditorGUILayout.PropertyField(buffMultiplierProp);
    }

    private void DrawDebuffFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty debuffDurationProp = talentEffectProp.FindPropertyRelative("debuffDuration");
        SerializedProperty debuffMultiplierProp = talentEffectProp.FindPropertyRelative("debuffMultiplier");
        SerializedProperty debuffDurationPropM = talentEffectProp.FindPropertyRelative("debuffModificationType");
        SerializedProperty debuffMultiplierPropM = talentEffectProp.FindPropertyRelative("debuffMultiplierModificationType");

        EditorGUILayout.PropertyField(debuffDurationPropM);
        EditorGUILayout.PropertyField(debuffDurationProp);
        EditorGUILayout.PropertyField(debuffMultiplierPropM);
        EditorGUILayout.PropertyField(debuffMultiplierProp);
    }

    private void DrawStunFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty stunDurationProp = talentEffectProp.FindPropertyRelative("stunDuration");
        SerializedProperty stunDurationPropM = talentEffectProp.FindPropertyRelative("stunDurationModificationType");

        EditorGUILayout.PropertyField(stunDurationPropM);
        EditorGUILayout.PropertyField(stunDurationProp);
    }

    private void DrawSlowFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty slowDurationProp = talentEffectProp.FindPropertyRelative("slowDuration");
        SerializedProperty slowDurationPropM = talentEffectProp.FindPropertyRelative("slowDurationModificationType");
        SerializedProperty slowPercentageProp = talentEffectProp.FindPropertyRelative("slowPercentage");
        SerializedProperty slowPercentagePropM = talentEffectProp.FindPropertyRelative("slowPercentageModificationType");

        EditorGUILayout.PropertyField(slowDurationPropM);
        EditorGUILayout.PropertyField(slowDurationProp);
        EditorGUILayout.PropertyField(slowPercentagePropM);
        EditorGUILayout.PropertyField(slowPercentageProp);
    }

    private void DrawSilenceFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty silenceDurationProp = talentEffectProp.FindPropertyRelative("silenceDuration");
        SerializedProperty silenceDurationPropM = talentEffectProp.FindPropertyRelative("silenceDurationModificationType");

        EditorGUILayout.PropertyField(silenceDurationPropM);
        EditorGUILayout.PropertyField(silenceDurationProp);
    }

    private void DrawKnockbackFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty knockbackForceProp = talentEffectProp.FindPropertyRelative("knockbackForce");
        SerializedProperty knockbackForcePropM = talentEffectProp.FindPropertyRelative("knockbackForceModificationType");

        EditorGUILayout.PropertyField(knockbackForcePropM);
        EditorGUILayout.PropertyField(knockbackForceProp);
    }

    private void DrawKnockupFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty knockupForceProp = talentEffectProp.FindPropertyRelative("knockupForce");
        SerializedProperty knockupForcePropM = talentEffectProp.FindPropertyRelative("knockupForceModificationType");

        EditorGUILayout.PropertyField(knockupForcePropM);
        EditorGUILayout.PropertyField(knockupForceProp);
    }

    private void DrawRootFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty rootDurationProp = talentEffectProp.FindPropertyRelative("rootDuration");
        SerializedProperty rootDurationPropM = talentEffectProp.FindPropertyRelative("rootDurationModificationType");

        EditorGUILayout.PropertyField(rootDurationPropM);
        EditorGUILayout.PropertyField(rootDurationProp);
    }

    private void DrawDisarmFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty disarmDurationProp = talentEffectProp.FindPropertyRelative("disarmDuration");
        SerializedProperty disarmDurationPropM = talentEffectProp.FindPropertyRelative("disarmDurationModificationType");

        EditorGUILayout.PropertyField(disarmDurationPropM);
        EditorGUILayout.PropertyField(disarmDurationProp);
    }

    private void DrawShieldFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty shieldAmountProp = talentEffectProp.FindPropertyRelative("shieldAmount");
        SerializedProperty shieldAmountPropM = talentEffectProp.FindPropertyRelative("shieldAmountModificationType");
        SerializedProperty shieldDurationProp = talentEffectProp.FindPropertyRelative("shieldDuration");
        SerializedProperty shieldDurationPropM = talentEffectProp.FindPropertyRelative("shieldDurationModificationType");

        EditorGUILayout.PropertyField(shieldAmountPropM);
        EditorGUILayout.PropertyField(shieldAmountProp);
        EditorGUILayout.PropertyField(shieldDurationPropM);
        EditorGUILayout.PropertyField(shieldDurationProp);
    }

    private void DrawStealthFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty stealthDurationProp = talentEffectProp.FindPropertyRelative("stealthDuration");
        SerializedProperty stealthDurationPropM = talentEffectProp.FindPropertyRelative("stealthDurationModificationType");

        EditorGUILayout.PropertyField(stealthDurationPropM);
        EditorGUILayout.PropertyField(stealthDurationProp);
    }

    private void DrawAoEFields(SerializedProperty talentEffectProp)
    {
        SerializedProperty aoeRadiusProp = talentEffectProp.FindPropertyRelative("aoeRadius");
        SerializedProperty aoeRadiusPropM = talentEffectProp.FindPropertyRelative("aoeRadiusModificationType");

        EditorGUILayout.PropertyField(aoeRadiusPropM);
        EditorGUILayout.PropertyField(aoeRadiusProp);
    }

}
