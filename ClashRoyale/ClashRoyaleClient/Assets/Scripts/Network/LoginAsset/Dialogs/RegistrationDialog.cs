using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationDialog : MonoBehaviour {
    [Header("Managers")]
    [SerializeField] private Registration _registration;
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private Password_InputField _passwordInput;
    [SerializeField] private Password_InputField _confirmPasswordInput;
    [SerializeField] private Button _confirmEmailButton;
    [SerializeField] private Button _applyButton;
    [SerializeField] private InformationView _informationView;

    public Action<UserLoginData> OnResultChanged;

    private UserLoginData _newUserLoginData;

    public void Init(UserLoginData user) => Show(user); 

    public void Hide() {
        ClearFields();
        gameObject.SetActive(false);
    }

    private void Show(UserLoginData user) {
        _newUserLoginData = user;
        _emailInput.text = user.Email;

        AddListeners();

        _confirmEmailButton.gameObject.SetActive(false);
        _informationView.Init();

        gameObject.SetActive(true);
    }

    private void AddListeners() {
        _emailInput.onEndEdit.AddListener(SetEmail);
        _passwordInput.InputField.onEndEdit.AddListener(SetPassword);
        _confirmPasswordInput.InputField.onEndEdit.AddListener(SetConfirmPassword);

        _applyButton.onClick.AddListener(ApplyButtonClick);
        _confirmEmailButton.onClick.AddListener(ShowEmailConfirmationDialog);
    }

    private void ApplyButtonClick() {
        _applyButton.gameObject.SetActive(false);
        if (CreateUserLoginData()) {
            _informationView.SetInfoValue(InfoType.Information, $"Данные получены, подтвердите Email");
            _confirmEmailButton.gameObject.SetActive(true);
        }
    }
    
    #region Set & Verification UserLoginData
    
    private bool CreateUserLoginData() {
        if (CheckUserLoginData()) {
            _newUserLoginData = new UserLoginData {
                Email = _emailInput.text,
                Password = _passwordInput.InputField.text,
                ConfirmPassword = _confirmPasswordInput.InputField.text,
                EmailConfirmCode = ""
            };
            return true;
        } else {
            return false;
        }
    }

    private bool CheckUserLoginData() {
        if (string.IsNullOrEmpty(_newUserLoginData.Email)) {
            ShowInfo(InfoType.Error, "Поле [Почта] пустое!");
            return false;
        }
        else if (string.IsNullOrEmpty(_newUserLoginData.Password)) {
            ShowInfo(InfoType.Error, "Поле [Пароль] пустое!");
            return false;
        }
        else if (string.IsNullOrEmpty(_newUserLoginData.ConfirmPassword)) {
            ShowInfo(InfoType.Error, "Поле [Подтверждение пароля] пустое!");
            return false;
        }

        if (_newUserLoginData.Password != _newUserLoginData.ConfirmPassword) {
            ShowInfo(InfoType.Error, "Значения в полях [Пароль] и [Подтверждение пароля] не совпадают!");
            return false;
        }

        return true;
    }

    private void SetEmail(string email) {
        _newUserLoginData.Email = email;
    }

    private void SetPassword(string password) {
        _newUserLoginData.Password = password;
    }

    private void SetConfirmPassword(string confirmPassword) {
        _newUserLoginData.ConfirmPassword = confirmPassword;
    }
    #endregion

    public void ShowInfo(InfoType type, string text) {
        _informationView.SetInfoValue(type, text);

        if (type == InfoType.Information) {
            OnResultChanged?.Invoke(_newUserLoginData);
        } else {
            _applyButton.gameObject.SetActive(true);
            _newUserLoginData.EmailConfirmCode = "";
            OnResultChanged?.Invoke(_newUserLoginData);
        }
    }

    private void ClearFields() {
        _emailInput.text = "";
        _passwordInput.InputField.text = "";
        _confirmPasswordInput.InputField.text = "";

        _informationView.ResetView();
    }

    private void ShowEmailConfirmationDialog() {
        _informationView.ResetView();
        OnResultChanged?.Invoke(_newUserLoginData);
    }
}
