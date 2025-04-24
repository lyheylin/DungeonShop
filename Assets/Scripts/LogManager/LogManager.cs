using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {
    public static LogManager Instance { get; private set; }

    [SerializeField] private TMP_Text logText;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private int maxLines = 10;
    [SerializeField] private float characterDelay = 0.02f;
    [SerializeField] private AudioSource typeSoundSource;
    [SerializeField] private AudioClip typeSoundClip;

    private Queue<string> logLines = new();
    private Queue<string> pendingMessages = new();

    private Coroutine typingCoroutine;

    private bool skipRequested = false;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SkipTyping() {
        if(typingCoroutine != null)
            skipRequested = true;
    }

    public void Log(string message) {
        pendingMessages.Enqueue(message);

        if (typingCoroutine == null) {
            typingCoroutine = StartCoroutine(ProcessQueue());
        }

        scrollRect.verticalNormalizedPosition = 0f;
    }

    private IEnumerator ProcessQueue() {
        while (pendingMessages.Count > 0) {
            string message = pendingMessages.Dequeue();

            if (logLines.Count >= maxLines)
                logLines.Dequeue();

            logLines.Enqueue(message);

            // Redraw previous lines without the current one
            var linesToShow = new List<string>(logLines);
            linesToShow.Remove(message);
            logText.text = string.Join("\n", linesToShow) + (linesToShow.Count > 0 ? "\n" : "");

            string currentText = logText.text;

            for (int i = 0; i < message.Length; i++) {
                currentText += message[i];
                logText.text = currentText;

                if (typeSoundClip != null && message[i] != ' ')
                    typeSoundSource.PlayOneShot(typeSoundClip);

                if (skipRequested)
                    break;

                yield return new WaitForSeconds(characterDelay);
            }

            if (skipRequested) {
                currentText = string.Join("\n", logLines);
                logText.text = currentText;
                skipRequested = false;
            }

            yield return new WaitForSeconds(0.2f); // brief pause before next message
        }

        typingCoroutine = null;
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
