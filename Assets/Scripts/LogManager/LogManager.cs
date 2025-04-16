using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {
    public static LogManager Instance { get; private set; }

    [SerializeField] private TMP_Text logText;
    [SerializeField] private int maxLines = 50;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float characterDelay = 0.02f;

    [SerializeField] private AudioSource typeSoundSource;
    [SerializeField] private AudioClip typeSoundClip;

    private readonly Queue<string> logLines = new();
    private Coroutine currentTypingCoroutine;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Log(string message) {
        if (logLines.Count >= maxLines) 
            logLines.Dequeue();

        logLines.Enqueue(message);

        if (currentTypingCoroutine != null) 
            StopCoroutine(currentTypingCoroutine);
        

        currentTypingCoroutine = StartCoroutine(TypeNewLine(message));
    }

    //Retype everything style
    private IEnumerator TypeText() {
        logText.text = "";
        var allText = string.Join("\n", logLines);
        int charIndex = 0;

        while (charIndex < allText.Length) {
            logText.text += allText[charIndex];
            charIndex++;
            yield return new WaitForSeconds(characterDelay);
        }

        currentTypingCoroutine = null;
    }

    private IEnumerator TypeNewLine(string newLine) {
        if (logLines.Count > 1) {
            logText.text = string.Join("\n", logLines).Replace(newLine, "");
        } else {
            logText.text = "";
        }

        string currentText = logText.text;

        for (int i = 0; i < newLine.Length; i++) {
            currentText += newLine[i];
            logText.text = currentText;

            // Play sound for visible characters (skip spaces for subtlety)
            if (typeSoundClip != null && newLine[i] != ' ') {
                typeSoundSource.PlayOneShot(typeSoundClip);
            }

            yield return new WaitForSeconds(characterDelay);
        }

        currentTypingCoroutine = null;
    }

    /*
    public void Log(string message) {
        logLines.Enqueue(message);
        if (logLines.Count > maxLines) {
            logLines.Dequeue();
        }

        logText.text = string.Join("\n", logLines);

        scrollRect.verticalNormalizedPosition = 0f;
    }*/
}
