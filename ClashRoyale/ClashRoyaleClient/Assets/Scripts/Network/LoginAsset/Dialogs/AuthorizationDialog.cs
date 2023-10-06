using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationDialog : MonoBehaviour {
    [Header("Managers")]
    [SerializeField] private Authorization _authorization;
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private Password_InputField _passwordInput;

    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _signUpButton;

    [SerializeField] private InformationView _informationView;

    public Action<UserLoginData> OnResultChanged;
    private UserLoginData _currentUserLoginData = new UserLoginData();

    public void Init(UserLoginData user) => Show(user);

    public void Hide() {
        ClearFields();
        gameObject.SetActive(false);
    }

    private void Show(UserLoginData currentUserLoginData) {
        _currentUserLoginData = currentUserLoginData;
        _emailInput.text = currentUserLoginData.Email;
        //_passwordInput.text = currentUserLoginData.Password;

        AddListeners();

        _signUpButton.gameObject.SetActive(false);
        _informationView.Init();

        gameObject.SetActive(true);
    }

    private void AddListeners() {
        _emailInput.onEndEdit.AddListener(_authorization.SetLogin);
        _passwordInput.InputField.onEndEdit.AddListener(_authorization.SetPassword);
        _signInButton.onClick.AddListener(SignInClick);
        _signUpButton.onClick.AddListener(ShowRegistrationDialog);
        _authorization.OnResult += ShowInfo;
    }

    private void SignInClick() {
        _signInButton.gameObject.SetActive(false);
        _signUpButton.gameObject.SetActive(false);
        _authorization.SignIn();
    }

    public void ShowInfo(InfoType type, string text) {
        if (type == InfoType.Error) {
            if (text.Contains("Login error!") == true) {
                _signInButton.gameObject.SetActive(true);
                _signUpButton.gameObject.SetActive(true);

                _informationView.SetInfoValue(InfoType.Error, $"Неккорректный или незарегистрированный Email");
                //Invoke(nameof(ShowRegistrationDialog), 2f);
                return;
            }

            _informationView.SetInfoValue(InfoType.Error, text);
            _signInButton.gameObject.SetActive(true);
        }

        _informationView.SetInfoValue(type, text);
    }

    private void ClearFields() {
        _emailInput.text = "";
        _passwordInput.InputField.text = "";
        _informationView.ResetView();
    }

    private void ShowRegistrationDialog() {
        _informationView.ResetView();
        _currentUserLoginData.Email = _emailInput.text;
        OnResultChanged?.Invoke(_currentUserLoginData);
    }

    private void OnDisable() {
        _authorization.OnResult -= ShowInfo;
    }
}
