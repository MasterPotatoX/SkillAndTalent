using NUnit.Framework;
using System;
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

    private int effectToDelete = -1; //used to keep track of effect to delete
    private int vfxToDelete = -1; //used to keep track of VFX to delete

    private GUIStyle deleteButtonStyle, addButtonStyle;
    private Texture2D deleteTexture, addTexture;

    private void OnEnable()
    {
        skillNameProperty = serializedObject.FindProperty("skillName");
        skillDescriptionProperty = serializedObject.FindProperty("skillDescription");
        skillManaCostProperty = serializedObject.FindProperty("manaCost");
        skillCooldownTimeProperty = serializedObject.FindProperty("cooldownTime");
        skillEffectsProperty = serializedObject.FindProperty("skillEffects");
        vfxListProperty = serializedObject.FindProperty("vfxList");

        deleteTexture = MakeBackgroundTexture(1, 1, Color.red);
        addTexture = MakeBackgroundTexture(1, 1, Color.green);

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        deleteButtonStyle = new GUIStyle();
        deleteButtonStyle.normal.background = deleteTexture;
        deleteButtonStyle.margin = new RectOffset(4, 4, 2, 2);
        deleteButtonStyle.alignment = TextAnchor.MiddleCenter;

        addButtonStyle = new GUIStyle();
        addButtonStyle.normal.background = addTexture;
        addButtonStyle.margin = new RectOffset(4, 4, 2, 2);
        addButtonStyle.alignment = TextAnchor.MiddleCenter;
        addButtonStyle.fontStyle = FontStyle.Bold;


        EditorGUILayout.PropertyField(skillNameProperty);
        EditorGUILayout.PropertyField(skillDescriptionProperty);
        EditorGUILayout.PropertyField(skillManaCostProperty);
        EditorGUILayout.PropertyField(skillCooldownTimeProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Skill Effects", EditorStyles.boldLabel);

        DrawSkillEffects();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("VFX List", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(vfxListProperty, true);
        EditorGUILayout.Space();
        
        DrawVFX();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawVFX()
    {
        EditorGUILayout.PropertyField(vfxListProperty, false);
        EditorGUI.indentLevel++;
        
        int vfxCount = vfxListProperty.arraySize;

        if(vfxCount > 0)
        {
            for (int i = 0; i < vfxCount; i++)
            {
                SerializedProperty vfxProperty = vfxListProperty.GetArrayElementAtIndex(i);
                SerializedProperty vfxNameProperty = vfxProperty.FindPropertyRelative("vfxName");
                SerializedProperty vfxPrefabProperty = vfxProperty.FindPropertyRelative("vfxPrefab");

                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(vfxNameProperty);
                EditorGUILayout.PropertyField(vfxPrefabProperty);
                EditorGUILayout.Space();
                ShowDeleteVFXButton(i, vfxNameProperty.stringValue);
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();

            }
        }
        else
        {
            EditorGUILayout.LabelField("No effects assigned.");
        }

        if (vfxToDelete != -1)
        {
            vfxListProperty.DeleteArrayElementAtIndex(vfxToDelete);
            vfxToDelete = -1;
        }

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.Space();
        ShowAddVFXButton(vfxListProperty);
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

    }

    private void DrawSkillEffects()
    {
        EditorGUILayout.PropertyField(skillEffectsProperty, false);
        
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

                EditorGUILayout.Space();
                ShowDeleteEffectButton(i, effectType.ToString());
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        else
        {
            EditorGUILayout.LabelField("No effects added.");
        }

        if(effectToDelete != -1)
        {
            skillEffectsProperty.DeleteArrayElementAtIndex(effectToDelete);
            effectToDelete = -1;
        }

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.Space();
        ShowAddEffectButton(skillEffectsProperty);
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        EditorGUI.indentLevel--;
      
    }

    private void ShowAddVFXButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add VFX", addButtonStyle))
        {
            list.InsertArrayElementAtIndex(list.arraySize);
            InitVFXValues(list.GetArrayElementAtIndex(list.arraySize - 1));
        }
    }

    private void ShowDeleteVFXButton(int index, string vfxName)
    {
        if (GUILayout.Button("Remove " + vfxName, deleteButtonStyle))
        {
            vfxToDelete = index;
        }
    }

    private void ShowAddEffectButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Effect", addButtonStyle))
        {
            list.InsertArrayElementAtIndex(list.arraySize);
            InitElementValues(list.GetArrayElementAtIndex(list.arraySize-1));
        }
    }

    private void ShowDeleteEffectButton(int index, string effectType)
    {
        
        if (GUILayout.Button("Remove " + effectType, deleteButtonStyle))
        {
            effectToDelete = index;
        }
    }

    private Texture2D MakeBackgroundTexture(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        Texture2D backgroundTexture = new Texture2D(width, height);

        backgroundTexture.SetPixels(pixels);
        backgroundTexture.Apply();

        return backgroundTexture;
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

    private void InitVFXValues(SerializedProperty elementProperty)
    {
        elementProperty.FindPropertyRelative("vfxName").stringValue = "";
        elementProperty.FindPropertyRelative("vfxPrefab").objectReferenceValue = null;

    }
    private void InitElementValues(SerializedProperty elementProperty)
    {
        elementProperty.FindPropertyRelative("effectType").enumValueIndex = 0;
        elementProperty.FindPropertyRelative("damageAmount").intValue = 0;
        elementProperty.FindPropertyRelative("healAmount").intValue = 0;
        elementProperty.FindPropertyRelative("buffDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("buffMultiplier").floatValue = 0f;
        elementProperty.FindPropertyRelative("debuffDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("debuffMultiplier").floatValue = 0f;
        elementProperty.FindPropertyRelative("stunDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("slowDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("slowPercentage").floatValue = 0f;
        elementProperty.FindPropertyRelative("silenceDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("knockbackForce").floatValue = 0f;
        elementProperty.FindPropertyRelative("knockupForce").floatValue = 0f;
        elementProperty.FindPropertyRelative("rootDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("disarmDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("shieldAmount").intValue = 0;
        elementProperty.FindPropertyRelative("shieldDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("stealthDuration").floatValue = 0f;
        elementProperty.FindPropertyRelative("aoeRadius").floatValue = 0f;
    }
}
#endif
