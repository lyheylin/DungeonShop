using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;


public class EventSceneUI : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform characterContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button choiceButtonPrefab;

    private Dictionary<string, Image> activeCharacters = new();
    private List<EventCommand> commands;
    private int commandIndex = 0;
    private bool waitingForClick = false;
    private bool isTyping = false;

    public void PlayEvent(EventDataSO data) {
        commands = data.commands;
        commandIndex = 0;
        panelRoot.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(RunEvent());
    }

    private IEnumerator RunEvent() {
        while (commandIndex < commands.Count) {
            EventCommand cmd = commands[commandIndex];

            switch (cmd.commandType) {
                case EventCommandType.ShowDialogue:
                    yield return ShowDialogue(cmd.characterName, cmd.dialogueKey);
                    break;

                case EventCommandType.ShowCharacter:
                    ShowCharacter(cmd.characterName, cmd.characterSprite);
                    break;

                case EventCommandType.HideCharacter:
                    HideCharacter(cmd.characterName);
                    break;

                case EventCommandType.SetBackground:
                    SetBackground(cmd.backgroundImage);
                    break;

                case EventCommandType.WaitForClick:
                    yield return WaitForClick();
                    break;

                case EventCommandType.BranchChoice:
                    yield return ShowChoices(cmd.choices);
                    break;
            }

            commandIndex++;
        }

        EndEvent();
    }

    private IEnumerator ShowDialogue(string name, string dialogueKey) {
        var text = new LocalizedString("Dialogue", dialogueKey);
        speakerText.text = name;
        dialogueText.text = "";
        isTyping = true;

        foreach (char c in text) {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(0.02f);
        }

        isTyping = false;
        yield return WaitForClick();
    }

    private void ShowCharacter(string name, Sprite sprite) {
        if (!activeCharacters.TryGetValue(name, out Image img)) {
            GameObject go = Instantiate(characterPrefab, characterContainer);
            img = go.GetComponent<Image>();
            activeCharacters[name] = img;
        }
        img.sprite = sprite;
        img.gameObject.SetActive(true);
    }

    private void HideCharacter(string name) {
        if (activeCharacters.TryGetValue(name, out Image img)) {
            img.gameObject.SetActive(false);
        }
    }

    private void SetBackground(Sprite sprite) {
        backgroundImage.sprite = sprite;
    }

    private IEnumerator WaitForClick() {
        waitingForClick = true;
        while (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) {
            yield return null;
        }
        waitingForClick = false;
    }

    private IEnumerator ShowChoices(List<string> choices) {
        choicePanel.SetActive(true);

        foreach (Transform child in choicePanel.transform)
            Destroy(child.gameObject);

        int selected = -1;

        for (int i = 0; i < choices.Count; i++) {
            var button = Instantiate(choiceButtonPrefab, choicePanel.transform);
            button.GetComponentInChildren<TMP_Text>().text = choices[i];
            int captured = i;
            button.onClick.AddListener(() => selected = captured);
        }

        while (selected == -1)
            yield return null;

        choicePanel.SetActive(false);
        // Use `selected` if branching needs to happen (add branching system here)
    }

    private void EndEvent() {
        panelRoot.SetActive(false);
        Time.timeScale = 1f;
        commands = null;
        commandIndex = 0;
    }

    private void Update() {
        if (!panelRoot.activeSelf) return;

        if (waitingForClick && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))) {
            waitingForClick = false;
        }
    }
}