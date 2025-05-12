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

    [SerializeField] private RectTransform leftAnchor;
    [SerializeField] private RectTransform centerAnchor;
    [SerializeField] private RectTransform rightAnchor;

    [SerializeField] private GameObject speakerBoxLeft;
    [SerializeField] private GameObject speakerBoxRight;

    private Dictionary<string, Image> activeCharacters = new();
    private List<EventCommand> commands;
    private int commandIndex = 0;
    private bool waitingForClick = false;
    private bool isTyping = false;
    private bool fastForward = false;
    private bool skipTyping = false;

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
                    yield return ShowDialogue(cmd.characterKey, cmd.dialogueKey, cmd.speakerBoxPosition);
                    break;

                case EventCommandType.ShowCharacter:
                    ShowCharacter(cmd.characterKey, cmd.characterSprite, cmd.characterPosition);
                    break;

                case EventCommandType.HideCharacter:
                    HideCharacter(cmd.characterKey);
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

    public enum SpeakerBoxPosition {
        None,
        Left,
        Right
    }

    private IEnumerator ShowDialogue(string characterKey, string dialogueKey, SpeakerBoxPosition speakerBoxPosition) {
        var localizedDialogueText = new LocalizedString("Dialogue", dialogueKey);
        var localizedCharacterName = new LocalizedString("Character_names", characterKey);

        // Wait for localized value
        var dialogueText = localizedDialogueText.GetLocalizedStringAsync();
        yield return dialogueText;
        string fullText = dialogueText.Result;


        var characterText = localizedCharacterName.GetLocalizedStringAsync();
        yield return dialogueText;
        string characterName = dialogueText.Result;
        SetSpeaker(characterName, speakerBoxPosition);

        this.dialogueText.text = "";
        isTyping = true;
        skipTyping = false;

        foreach (char c in fullText) {
            if (skipTyping) {
                this.dialogueText.text = fullText;
                break;
            }

            this.dialogueText.text += c;
            yield return new WaitForSecondsRealtime(0.02f);
        }

        isTyping = false;
        skipTyping = false;

        // If holding to fast-forward, auto-skip wait
        if (!fastForward) {
            yield return WaitForClick();
        }
    }

    private void SetSpeaker(string characterKey, SpeakerBoxPosition position) {
        speakerBoxLeft.SetActive(false);
        speakerBoxRight.SetActive(false);

        if (position == SpeakerBoxPosition.None) return;

        TMP_Text speakerLabel = position switch {
            SpeakerBoxPosition.Left => speakerBoxLeft.GetComponentInChildren<TMP_Text>(),
            SpeakerBoxPosition.Right => speakerBoxRight.GetComponentInChildren<TMP_Text>(),
            _ => null
        };

        if (speakerLabel != null) {
            speakerLabel.text = characterKey;
            if (position == SpeakerBoxPosition.Left)
                speakerBoxLeft.SetActive(true);
            else
                speakerBoxRight.SetActive(true);
        }
    }

    public enum CharacterPosition {
        Left,
        Center,
        Right
    }

    private void ShowCharacter(string characterKey, Sprite sprite, CharacterPosition position, float offset = 0) {
        if (!activeCharacters.TryGetValue(characterKey, out Image img)) {
            GameObject go = Instantiate(characterPrefab, characterContainer);
            img = go.GetComponent<Image>();
            activeCharacters[characterKey] = img;
        }

        img.sprite = sprite;
        img.gameObject.SetActive(true);

        // Move to specified anchor
        RectTransform anchor = GetAnchor(position);
        img.rectTransform.SetParent(anchor, false);
    }

    private RectTransform GetAnchor(CharacterPosition pos) {
        return pos switch {
            CharacterPosition.Left => leftAnchor,
            CharacterPosition.Center => centerAnchor,
            CharacterPosition.Right => rightAnchor,
            _ => centerAnchor,//Fallthrough
        };
    }

    private void HideCharacter(string characterKey) {
        if (activeCharacters.TryGetValue(characterKey, out Image img)) {
            img.gameObject.SetActive(false);
        }
    }

    private void SetBackground(Sprite sprite) {
        backgroundImage.sprite = sprite;
    }

    private IEnumerator WaitForClick() {
        if (fastForward) yield break;

        waitingForClick = true;
        while (waitingForClick) {
            yield return null;
        }
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

        // Handle skip / fast-forward input
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            if (isTyping) {
                skipTyping = true;
            } else {
                waitingForClick = false;
            }
        }

        fastForward = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
    }
}