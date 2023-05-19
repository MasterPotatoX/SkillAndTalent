using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
/*[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor
{
    private SerializedProperty skillNameProperty;
    private SerializedProperty skillDescriptionProperty;
    private SerializedProperty skillManaCostProperty;
    private SerializedProperty skillCooldownTimeProperty;
    private SerializedProperty skillEffectsProperty;
    private SerializedProperty vfxListProperty;

    private Dictionary<SkillEffectType, List<string>> effectTypeToFieldMap;

    private void OnEnable()
    {
        skillNameProperty = serializedObject.FindProperty("skillName");
        skillDescriptionProperty = serializedObject.FindProperty("skillDescription");
        skillManaCostProperty = serializedObject.FindProperty("manaCost");
        skillCooldownTimeProperty = serializedObject.FindProperty("cooldownTime");
        skillEffectsProperty = serializedObject.FindProperty("skillEffects");
        vfxListProperty = serializedObject.FindProperty("vfxList");

        InitializeEffectTypeToFieldMap();
    }

    private void InitializeEffectTypeToFieldMap()
    {
        effectTypeToFieldMap = new Dictionary<SkillEffectType, List<string>>()
        {
            { SkillEffectType.None, new List<string>() },
            { SkillEffectType.Damage, new List<string>() { "damageAmount" } },
            { SkillEffectType.Heal, new List<string>() { "healAmount" } },
            { SkillEffectType.Buff, new List<string>() { "buffDuration", "buffMultiplier" } },
            { SkillEffectType.Debuff, new List<string>() { "debuffDuration", "debuffMultiplier" } },
            { SkillEffectType.Stun, new List<string>() { "stunDuration" } },
            { SkillEffectType.Slow, new List<string>() { "slowDuration", "slowPercentage" } },
            { SkillEffectType.Silence, new List<string>() { "silenceDuration" } },
            { SkillEffectType.Knockback, new List<string>() { "knockbackForce" } },
            { SkillEffectType.Knockup, new List<string>() { "knockupForce" } },
            { SkillEffectType.Root, new List<string>() { "rootDuration" } },
            { SkillEffectType.Disarm, new List<string>() { "disarmDuration" } },
            { SkillEffectType.Shield, new List<string>() { "shieldAmount", "shieldDuration" } },
            { SkillEffectType.Stealth, new List<string>() { "stealthDuration" } },
            { SkillEffectType.AoE, new List<string>() { "aoeRadius" } },
            { SkillEffectType.Projectile, new List<string>() { "projectilePrefab" } }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(skillNameProperty);
        EditorGUILayout.PropertyField(skillDescriptionProperty);
        EditorGUILayout.PropertyField(skillManaCostProperty);
        EditorGUILayout.PropertyField(skillCooldownTimeProperty);

        EditorGUILayout.PropertyField(skillEffectsProperty, true);
        if (skillEffectsProperty.isExpanded)
        {
            EditorGUI.indentLevel++;
            DrawSkillEffects();
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(vfxListProperty, true);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawSkillEffects()
    {
        for (int i = 0; i < skillEffectsProperty.arraySize; i++)
        {
            SerializedProperty skillEffectProperty = skillEffectsProperty.GetArrayElementAtIndex(i);
            SerializedProperty effectTypeProperty = skillEffectProperty.FindPropertyRelative("effectType");

            EditorGUILayout.PropertyField(effectTypeProperty);
            SkillEffectType selectedEffectType = (SkillEffectType)effectTypeProperty.enumValueIndex;

            List<string> relevantFields;
            if (effectTypeToFieldMap.TryGetValue(selectedEffectType, out relevantFields))
            {
                EditorGUI.indentLevel++;
                foreach (string fieldName in relevantFields)
                {
                    SerializedProperty fieldProperty = skillEffectProperty.FindPropertyRelative(fieldName);
                    EditorGUILayout.PropertyField(fieldProperty);
                }
                EditorGUI.indentLevel--;
            }
        }
    }

    private void ResetFieldToDefault(SerializedProperty property)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = 0;
                break;
            case SerializedPropertyType.Float:
                property.floatValue = 0f;
                break;
            case SerializedPropertyType.Boolean:
                property.boolValue = false;
                break;
            case SerializedPropertyType.String:
                property.stringValue = "";
                break;
            case SerializedPropertyType.ObjectReference:
                property.objectReferenceValue = null;
                break;
            case SerializedPropertyType.Enum:
                property.enumValueIndex = 0;
                break;
            case SerializedPropertyType.Vector2:
                property.vector2Value = Vector2.zero;
                break;
            case SerializedPropertyType.Vector3:
                property.vector3Value = Vector3.zero;
                break;
            case SerializedPropertyType.Vector4:
                property.vector4Value = Vector4.zero;
                break;
            case SerializedPropertyType.Quaternion:
                property.quaternionValue = Quaternion.identity;
                break;
            case SerializedPropertyType.Color:
                property.colorValue = Color.white;
                break;
            case SerializedPropertyType.Bounds:
                property.boundsValue = new Bounds();
                break;
            case SerializedPropertyType.AnimationCurve:
                property.animationCurveValue = AnimationCurve.Linear(0f, 0f, 1f, 1f);
                break;
            case SerializedPropertyType.Rect:
                property.rectValue = new Rect();
                break;
            default:
                
                break;
        }
    }

}*/

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
