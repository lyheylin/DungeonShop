using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventCommandType {
    ShowDialogue,
    ShowCharacter,
    HideCharacter,
    SetBackground,
    WaitForClick,
    BranchChoice
}

[System.Serializable]
public class EventCommand {
    public EventCommandType commandType;
    public string characterKey;
    public string dialogueKey;
    public Sprite characterSprite;
    public Sprite backgroundImage;
    public int waitFrames;
    public List<string> choices;
}

[CreateAssetMenu(fileName = "NewEvent", menuName = "Event/EventScript")]
public class EventDataSO : ScriptableObject {
    public string eventName;
    public List<EventCommand> commands;
}