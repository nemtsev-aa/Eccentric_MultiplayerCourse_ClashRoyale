using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailConfirmationDialog : MonoBehaviour {
    [SerializeField] private InformationView _informationView;
    [SerializeField] private TMP_InputField _verificationCodeInput;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _signInButton;

    public Action<bool> Confirmation;
    private UserLoginData _currentUserLoginData;
    private string _verificationCode;

    public void Init(UserLoginData user) => Show(user);

    public void Hide() {
        ClearFields();
        gameObject.SetActive(false);
    }

    private void Show(UserLoginData user) {
        _currentUserLoginData = user;
        _verificationCode = user.EmailConfirmCode;

        AddListeners();
        
        gameObject.SetActive(true);
        _signInButton.gameObject.SetActive(false);

        _informationView.Init();
        Invoke(nameof(ShowInfoMessage), 2f);
    }

    private void AddListeners() {
        _verificationCodeInput.onEndEdit.AddListener(CheckInput);
        _applyButton.onClick.AddListener(CheckCode);
        _signInButton.onClick.AddListener(HideEmailConfirmationDialog);
    }

    private void CheckInput(string code) {
        if (string.IsNullOrEmpty(code)) {
            _informationView.SetInfoValue(InfoType.Error, $"Проверочный код не введён!");
        }
    }

    private void CheckCode() {
        if (_verificationCode == _verificationCodeInput.text) {
            _applyButton.gameObject.SetActive(false);
            _signInButton.gameObject.SetActive(true);

            ShowInfo(InfoType.Information, $"Email успешно подтверждён!");
        } else {
            ShowInfo(InfoType.Error, $"Неверный проверочный код!");
        }
    }

    private void HideEmailConfirmationDialog() {
        Confirmation?.Invoke(true);
    }

    private void ClearFields() {
        _verificationCodeInput.text = "";
        _informationView.ResetView();
    }

    private void ShowInfoMessage() {
        _informationView.SetInfoValue(InfoType.Information, $"Проверочный код отправлен на Email!");
    }

    private void ShowInfo(InfoType type, string text) {
        _informationView.SetInfoValue(type, text);

        if (type == InfoType.Error) {
            _applyButton.gameObject.SetActive(true);
            _verificationCodeInput.text = "";
        } else {
            Confirmation?.Invoke(true);
        }
    }
}
