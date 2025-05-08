using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;

[CustomEditor(typeof(EventDataSO))]
public class EventDataSOEditor : Editor {
    private SerializedProperty eventNameProp;
    private SerializedProperty commandsProp;

    private ReorderableList commandList;

    private void OnEnable() {
        eventNameProp = serializedObject.FindProperty("eventName");
        commandsProp = serializedObject.FindProperty("commands");

        commandList = new ReorderableList(serializedObject, commandsProp, true, true, true, true);

        commandList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Event Commands");
        };

        commandList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var command = commandsProp.GetArrayElementAtIndex(index);
            var commandType = command.FindPropertyRelative("commandType");

            float y = rect.y + 2f;
            float height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(
                new Rect(rect.x, y, rect.width - 40, height),
                commandType,
                GUIContent.none
            );

            if (GUI.Button(new Rect(rect.x + rect.width - 35, y, 30, height), "X")) {
                commandsProp.DeleteArrayElementAtIndex(index);
                return;
            }

            y += height + 2f;
            EditorGUI.indentLevel++;

            DrawCommandFields(command, ref y, rect, index);

            EditorGUI.indentLevel--;
        };

        commandList.elementHeightCallback = (index) => {
            var command = commandsProp.GetArrayElementAtIndex(index);
            return CalculateElementHeight(command) + 10f;
        };
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(eventNameProp);
        EditorGUILayout.Space();

        commandList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCommandFields(SerializedProperty command, ref float y, Rect rect, int index) {
        float width = rect.width;
        float height = EditorGUIUtility.singleLineHeight;

        EventCommandType type = (EventCommandType)command.FindPropertyRelative("commandType").enumValueIndex;

        switch (type) {
            case EventCommandType.ShowDialogue:
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("characterKey"));
                y += height + 2;
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("dialogueKey"));
                y += height + 2;
                break;

            case EventCommandType.ShowCharacter:
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("characterKey"));
                y += height + 2;
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("characterSprite"));
                y += height + 2;
                break;

            case EventCommandType.HideCharacter:
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("characterKey"));
                y += height + 2;
                break;

            case EventCommandType.SetBackground:
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("backgroundImage"));
                y += height + 2;
                break;

            case EventCommandType.WaitForClick:
                EditorGUI.PropertyField(new Rect(rect.x, y, width, height), command.FindPropertyRelative("waitFrames"));
                y += height + 2;
                break;

            case EventCommandType.BranchChoice:
                SerializedProperty choices = command.FindPropertyRelative("choices");
                EditorGUI.LabelField(new Rect(rect.x, y, width, height), "Choices:");
                y += height + 2;

                for (int i = 0; i < choices.arraySize; i++) {
                    var choiceProp = choices.GetArrayElementAtIndex(i);
                    EditorGUI.PropertyField(new Rect(rect.x + 10, y, width - 40, height), choiceProp, new GUIContent($"Choice {i + 1}"));

                    if (GUI.Button(new Rect(rect.x + width - 30, y, 20, height), "-")) {
                        choices.DeleteArrayElementAtIndex(i);
                        break;
                    }

                    y += height + 2;
                }

                if (GUI.Button(new Rect(rect.x + 10, y, 100, height), "Add Choice")) {
                    choices.InsertArrayElementAtIndex(choices.arraySize);
                    choices.GetArrayElementAtIndex(choices.arraySize - 1).stringValue = "";
                }
                y += height + 2;
                break;
        }
    }

    private float CalculateElementHeight(SerializedProperty command) {
        float lineHeight = EditorGUIUtility.singleLineHeight + 6;
        EventCommandType type = (EventCommandType)command.FindPropertyRelative("commandType").enumValueIndex;

        switch (type) {
            case EventCommandType.ShowDialogue: return lineHeight * 2;
            case EventCommandType.ShowCharacter: return lineHeight * 2;
            case EventCommandType.HideCharacter: return lineHeight;
            case EventCommandType.SetBackground: return lineHeight;
            case EventCommandType.WaitForClick: return lineHeight;
            case EventCommandType.BranchChoice:
                SerializedProperty choices = command.FindPropertyRelative("choices");
                return lineHeight * (choices.arraySize + 2);
            default: return lineHeight;
        }
    }
}