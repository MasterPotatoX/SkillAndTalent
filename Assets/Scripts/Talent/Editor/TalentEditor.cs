using System;
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

    private GUIStyle deleteButtonStyle, addButtonStyle;
    private Texture2D deleteTexture, addTexture;

    private int skillToRemove = -1; //used to keep track of skill to remove
    private int vfxToRemove = -1; //used to keep track of skill to remove
    private int talentEffectToDelete = -1; //used to keep track of Talent Effect to delete
    private SerializedProperty talentEffectListRef;
    private SerializedProperty vfxListRef;
    

    private void OnEnable()
    {
        talentNameProp = serializedObject.FindProperty("talentName");
        talentDescriptionProp = serializedObject.FindProperty("talentDescription");
        manaCostModificationTypeProp = serializedObject.FindProperty("manaCostModificationType");
        manaCostModifierProp = serializedObject.FindProperty("manaCostModifier");
        cooldownTimeModificationTypeProp = serializedObject.FindProperty("cooldownTimeModificationType");
        cooldownTimeModifierProp = serializedObject.FindProperty("cooldownTimeModifier");
        skillProp = serializedObject.FindProperty("skill");

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

        EditorGUILayout.PropertyField(talentNameProp);
        EditorGUILayout.PropertyField(talentDescriptionProp);
        EditorGUILayout.PropertyField(manaCostModificationTypeProp);
        EditorGUILayout.PropertyField(manaCostModifierProp);
        EditorGUILayout.PropertyField(cooldownTimeModificationTypeProp);
        EditorGUILayout.PropertyField(cooldownTimeModifierProp);

        //EditorGUILayout.PropertyField(skillProp, true);

        // Loop through skills
        for (int i = 0; i < skillProp.arraySize; i++)
        {
            SerializedProperty skillElementProp = skillProp.GetArrayElementAtIndex(i);
            SerializedProperty talentEffectProp = skillElementProp.FindPropertyRelative("talentEffect");
            SerializedProperty affectedSkillProp = skillElementProp.FindPropertyRelative("affectedSkill");

            SerializedProperty talentVFXProp = skillElementProp.FindPropertyRelative("talentVfxList");

            string skillName = affectedSkillProp.objectReferenceValue != null ? affectedSkillProp.objectReferenceValue.name : "None";
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Skill: " + skillName, EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(affectedSkillProp);
            ShowDeleteSkillButton(i, skillName);

            // Loop through talent effects
            for (int j = 0; j < talentEffectProp.arraySize; j++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
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
                EditorGUILayout.Space();
                ShowDeleteEffectButton(talentEffectProp, j, effectType.ToString());
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();

            }

            if (!skillName.Equals("None"))
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.Space();
                ShowAddTalentButton(talentEffectProp);
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();

            //display vfx
            for (int j = 0; j < talentVFXProp.arraySize; j++)
            {
                EditorGUILayout.LabelField("VFX:", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.Space();
                SerializedProperty talentVfxElementProp = talentVFXProp.GetArrayElementAtIndex(j);
                SerializedProperty vfxNameProp = talentVfxElementProp.FindPropertyRelative("vfxName");
                SerializedProperty vfxPrefabProp = talentVfxElementProp.FindPropertyRelative("vfxPrefab");
                EditorGUILayout.PropertyField(vfxNameProp);
                EditorGUILayout.PropertyField(vfxPrefabProp);
                ShowDeleteVFXButton(talentVFXProp, j, vfxNameProp.stringValue);
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }

            if (!skillName.Equals("None"))
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.Space();
                ShowAddVfxButton(talentVFXProp);
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        }

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.Space();
        ShowAddSkillButton(skillProp);
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        if (talentEffectToDelete != -1)
        {
            talentEffectListRef.DeleteArrayElementAtIndex(talentEffectToDelete);
            talentEffectToDelete = -1;
        }

        if(skillToRemove != -1)
        {
            skillProp.DeleteArrayElementAtIndex(skillToRemove);
            skillToRemove = -1;
        }

        if (vfxToRemove != -1)
        {
            vfxListRef.DeleteArrayElementAtIndex(vfxToRemove);
            vfxToRemove = -1;
        }

        serializedObject.ApplyModifiedProperties();
    }


    private void ShowDeleteEffectButton(SerializedProperty talentEffectProp, int index, string effectType)
    {

        if (GUILayout.Button("Remove " + effectType, deleteButtonStyle))
        {
            talentEffectListRef = talentEffectProp;
            talentEffectToDelete = index;
        }
    }
    private void ShowAddTalentButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Talent Effect", addButtonStyle))
        {
            list.arraySize++;
            SerializedProperty newTalentEffectProp = list.GetArrayElementAtIndex(list.arraySize - 1);
            newTalentEffectProp.FindPropertyRelative("effectType").enumValueIndex = 0; // Set default effect type

        }
    }

    private void ShowAddVfxButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add VFX", addButtonStyle))
        {
            list.arraySize++;
            SerializedProperty newVfxProp = list.GetArrayElementAtIndex(list.arraySize - 1);
            newVfxProp.FindPropertyRelative("vfxPrefab").objectReferenceValue = null; // Set default vfx to empty
            newVfxProp.FindPropertyRelative("vfxName").stringValue = ""; // Set default vfx to empty
        }
    }

    private void ShowDeleteVFXButton(SerializedProperty vfxProp, int index, string vfxName)
    {
        if (GUILayout.Button("Remove " + vfxName, deleteButtonStyle))
        {
            vfxToRemove = index;
            vfxListRef = vfxProp;
        }
    }

    private void ShowDeleteSkillButton(int index, string skillName)
    {
        if (GUILayout.Button("Remove " + skillName, deleteButtonStyle))
        {
            skillToRemove = index;
        }
    }

    private void ShowAddSkillButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Skill", addButtonStyle))
        {
            list.arraySize++;
            
            SerializedProperty newSkillProp = list.GetArrayElementAtIndex(list.arraySize - 1);
            newSkillProp.FindPropertyRelative("affectedSkill").objectReferenceValue = null; // Set default skill type
            SerializedProperty talentEffectProp = newSkillProp.FindPropertyRelative("talentEffect"); //clear all talents by default
            talentEffectProp.ClearArray();

            SerializedProperty talentVFXProp = newSkillProp.FindPropertyRelative("talentVfxList"); //clear all vfx by default
            talentVFXProp.ClearArray();
        }
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

}
