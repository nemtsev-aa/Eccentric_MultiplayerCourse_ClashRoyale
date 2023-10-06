using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Password_InputField : MonoBehaviour {
    public TMP_InputField InputField => _password;

    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Toggle _showCharsButton;

    private void Start() {
        _showCharsButton.onValueChanged.AddListener(ShowChapterClick);
    }

    private void ShowChapterClick(bool value) {
        if (value) {
            _password.contentType = TMP_InputField.ContentType.Standard;
        } else {
            _password.contentType = TMP_InputField.ContentType.Password;
        }
        _password.ForceLabelUpdate();
    }
}
