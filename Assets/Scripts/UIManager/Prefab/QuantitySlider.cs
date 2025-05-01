using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class QuantitySelector : MonoBehaviour {
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    public UnityEvent<int> onValueChanged;

    private int value = 0;
    private Coroutine holdCoroutine;

    private void Start() {
        inputField.text = value.ToString();
        inputField.onEndEdit.AddListener(OnInputChanged);
        plusButton.onClick.AddListener(() => ChangeValue(1));
        minusButton.onClick.AddListener(() => ChangeValue(-1));
    }


    private void SetupHoldButton(Button button, UnityAction action) {
        // Initial click
        button.onClick.AddListener(action);

        // Hold behavior
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener((e) => holdCoroutine = StartCoroutine(HoldToRepeat(action)));
        trigger.triggers.Add(pointerDown);

        var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener((e) => StopHold());
        trigger.triggers.Add(pointerUp);

        var pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        pointerExit.callback.AddListener((e) => StopHold());
        trigger.triggers.Add(pointerExit);
    }

    private IEnumerator HoldToRepeat(UnityAction action) {
        yield return new WaitForSeconds(0.5f); // Initial delay
        float repeatRate = 0.2f;

        while (true) {
            action.Invoke();
            yield return new WaitForSeconds(repeatRate);

            // Accelerate
            repeatRate = Mathf.Max(0.05f, repeatRate * 0.9f);
        }
    }

    private void StopHold() {
        if (holdCoroutine != null) {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }
    }


    private void ChangeValue(int delta) {
        value = Mathf.Max(0, value + delta);
        inputField.text = value.ToString();
        onValueChanged?.Invoke(value);
    }

    private void OnInputChanged(string input) {
        if (int.TryParse(input, out int newValue)) {
            value = Mathf.Max(0, newValue);
            onValueChanged?.Invoke(value);
        } else {
            inputField.text = value.ToString();
        }
    }

    public int GetValue() => value;
    public void SetValue(int newValue) {
        value = Mathf.Max(0, newValue);
        inputField.text = value.ToString();
    }
}