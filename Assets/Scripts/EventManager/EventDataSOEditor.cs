using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(EventDataSO))]
public class EventDataSOEditor : Editor {
    private SerializedProperty eventNameProp;
    private SerializedProperty commandsProp;

    private void OnEnable() {
        eventNameProp = serializedObject.FindProperty("eventName");
        commandsProp = serializedObject.FindProperty("commands");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(eventNameProp);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Event Commands", EditorStyles.boldLabel);

        for (int i = 0; i < commandsProp.arraySize; i++) {
            SerializedProperty commandProp = commandsProp.GetArrayElementAtIndex(i);
            SerializedProperty commandType = commandProp.FindPropertyRelative("commandType");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            commandType.enumValueIndex = (int)(EventCommandType)EditorGUILayout.EnumPopup("Command", (EventCommandType)commandType.enumValueIndex);

            if (GUILayout.Button("X", GUILayout.Width(20))) {
                commandsProp.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            // Draw fields conditionally based on command type
            DrawCommandFields(commandProp);
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add New Command")) {
            commandsProp.InsertArrayElementAtIndex(commandsProp.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCommandFields(SerializedProperty commandProp) {
        EventCommandType type = (EventCommandType)commandProp.FindPropertyRelative("commandType").enumValueIndex;

        switch (type) {
            case EventCommandType.ShowDialogue:
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("characterName"));
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("dialogueText"));
                break;

            case EventCommandType.ShowCharacter:
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("characterName"));
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("characterSprite"));
                break;

            case EventCommandType.HideCharacter:
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("characterName"));
                break;

            case EventCommandType.SetBackground:
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("backgroundImage"));
                break;

            case EventCommandType.WaitForClick:
                EditorGUILayout.PropertyField(commandProp.FindPropertyRelative("waitFrames"));
                break;

            case EventCommandType.BranchChoice:
                SerializedProperty choices = commandProp.FindPropertyRelative("choices");
                EditorGUILayout.LabelField("Choices:");
                for (int i = 0; i < choices.arraySize; i++) {
                    EditorGUILayout.BeginHorizontal();
                    choices.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextField($"Choice {i + 1}", choices.GetArrayElementAtIndex(i).stringValue);
                    if (GUILayout.Button("-", GUILayout.Width(20))) {
                        choices.DeleteArrayElementAtIndex(i);
                        break;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Choice")) {
                    choices.InsertArrayElementAtIndex(choices.arraySize);
                }
                break;
        }
    }
}