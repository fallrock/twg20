using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldBehaviour : MonoBehaviour {
    public InputField inputField;

    public float minValue = Mathf.NegativeInfinity;
    public float maxValue = Mathf.Infinity;

    public string key;
    public float defaultValue;

    public float val { get; private set; }

    void Awake() {
        if (!PlayerPrefs.HasKey(this.key)) {
            PlayerPrefs.SetFloat(this.key, this.defaultValue);
        }

        this.inputField.onValueChanged.AddListener(OnValueChanged);
        this.inputField.onEndEdit.AddListener(OnEndEdit);

        this.val = PlayerPrefs.GetFloat(this.key);
        UpdateInputField(val);
    }

    public void OnValueChanged(string text) {
        if (text == "") {
            return;
        }
        float v;
        if (float.TryParse(text, out v)) {
            UpdateInputField(v);
        } else {
            UpdateInputField(this.val);
        }
    }

    public void OnEndEdit(string text) {
        if (text == "") {
            UpdateInputField(this.val);
        } else {
            float v = Mathf.Clamp(float.Parse(text), this.minValue, this.maxValue);
            UpdateInputField(v);
            this.val = v;
            PlayerPrefs.SetFloat(this.key, v);
        }
    }

    private void UpdateInputField(float v) {
        this.inputField.text = v.ToString();
    }

}
