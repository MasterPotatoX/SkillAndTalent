using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR


[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor
{
    private SerializedProperty skillNameProperty;
    private SerializedProperty skillDescriptionProperty;
    private SerializedProperty skillManaCostProperty;
    private SerializedProperty skillCooldownTimeProperty;
    private SerializedProperty skillEffectsProperty;
    private SerializedProperty vfxListProperty;

    private static readonly string[] skillEffectTypeLabels = System.Enum.GetNames(typeof(SkillEffectType));

    private void OnEnable()
    {
        skillNameProperty = serializedObject.FindProperty("skillName");
        skillDescriptionProperty = serializedObject.FindProperty("skillDescription");
        skillManaCostProperty = serializedObject.FindProperty("manaCost");
        skillCooldownTimeProperty = serializedObject.FindProperty("cooldownTime");
        skillEffectsProperty = serializedObject.FindProperty("skillEffects");
        vfxListProperty = serializedObject.FindProperty("vfxList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(skillNameProperty);
        EditorGUILayout.PropertyField(skillDescriptionProperty);
        EditorGUILayout.PropertyField(skillManaCostProperty);
        EditorGUILayout.PropertyField(skillCooldownTimeProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Skill Effects", EditorStyles.boldLabel);
        DrawSkillEffects();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("VFX List", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(vfxListProperty, true);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawSkillEffects()
    {
        EditorGUILayout.PropertyField(skillEffectsProperty, true);
        if (skillEffectsProperty.isExpanded)
        {
            EditorGUI.indentLevel++;

            int effectCount = skillEffectsProperty.arraySize;
            if (effectCount > 0)
            {
                EditorGUILayout.LabelField("Effects:");

                for (int i = 0; i < effectCount; i++)
                {
                    SerializedProperty effectProperty = skillEffectsProperty.GetArrayElementAtIndex(i);
                    SerializedProperty effectTypeProperty = effectProperty.FindPropertyRelative("effectType");

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.PropertyField(effectTypeProperty);

                    SkillEffectType effectType = (SkillEffectType)effectTypeProperty.enumValueIndex;
                    DrawEffectProperties(effectType, effectProperty);

                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                EditorGUILayout.LabelField("No effects added.");
            }

            EditorGUI.indentLevel--;
        }
    }

    private void DrawEffectProperties(SkillEffectType effectType, SerializedProperty effectProperty)
    {
        switch (effectType)
        {
            case SkillEffectType.Damage:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("damageAmount"));
                break;
            case SkillEffectType.Heal:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("healAmount"));
                break;
            case SkillEffectType.Buff:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("buffDuration"));
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("buffMultiplier"));
                break;
            case SkillEffectType.Debuff:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("debuffDuration"));
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("debuffMultiplier"));
                break;
            case SkillEffectType.Stun:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("stunDuration"));
                break;
            case SkillEffectType.Slow:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("slowDuration"));
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("slowPercentage"));
                break;
            case SkillEffectType.Silence:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("silenceDuration"));
                break;
            case SkillEffectType.Knockback:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("knockbackForce"));
                break;
            case SkillEffectType.Knockup:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("knockupForce"));
                break;
            case SkillEffectType.Root:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("rootDuration"));
                break;
            case SkillEffectType.Disarm:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("disarmDuration"));
                break;
            case SkillEffectType.Shield:
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("shieldAmount"));
                EditorGUILayout.PropertyField(effectProperty.FindPropertyRelative("shieldDuration"));
                break;
            default:
                EditorGUILayout.HelpBox("Unsupported Skill Effect Type.", MessageType.Warning);
                break;
        }
    }
}
#endif
