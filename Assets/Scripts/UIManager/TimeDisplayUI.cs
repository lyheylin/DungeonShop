using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplayUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timeText;
    private void Update() {
        if (GameManager.Instance == null) return;

        int day = DayCycleManager.Instance.GetCurrentDay();
        string phase = GameManager.Instance.GetCurrentGameState().ToString();

        timeText.text = $"Day {day} - Phase: {phase}";
    }
}