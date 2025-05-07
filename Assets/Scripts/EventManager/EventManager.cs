using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    [SerializeField] private EventSceneUI eventSceneUI;

    private List<EventCommand> commands;

    public void PlayEvent(EventDataSO eventData) {
        PlayEvent(eventData);
    }

}