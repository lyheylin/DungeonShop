using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    [SerializeField] private EventSceneUI eventSceneUI;

    private Queue<EventCommand> commandQueue;

    public void PlayEvent(EventDataSO eventData) {
        commandQueue = new Queue<EventCommand>(eventData.commands);
        eventSceneUI.Show();
        StartCoroutine(ProcessCommands());
    }

    private IEnumerator ProcessCommands() {
        while (commandQueue.Count > 0) {
            var cmd = commandQueue.Dequeue();
            yield return eventSceneUI.ExecuteCommand(cmd);
        }

        eventSceneUI.Hide();
        //GameManager.Instance.ResumeAfterEvent(); // or move to next phase
    }
}