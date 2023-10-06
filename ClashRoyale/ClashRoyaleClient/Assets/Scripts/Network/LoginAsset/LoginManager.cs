using System;
using UnityEngine;

public struct UserLoginData {
    public string Email;
    public string Password;
    public string ConfirmPassword;
    public string EmailConfirmCode;
}

public class LoginManager : MonoBehaviour {
    private const int EmailConfirmCodeLenght = 5;
    [Header("Managers")]
    [SerializeField] private Registration _registration;

    [SerializeField] private EmailSendler _emailSendler;

    [SerializeField] private AuthorizationDialog _authorizationDialog;
    [SerializeField] private RegistrationDialog _registrationDialog;
    [SerializeField] private EmailConfirmationDialog _emailConfirmationDialog;
    
    public event Action Authorization;

    private UserLoginData _currentUserLoginData = new UserLoginData();

    private void Start() {
        Init();
    }

    public void Init() {
        _registrationDialog.Hide();
        _emailConfirmationDialog.Hide();

        AddListeners();

        _authorizationDialog.Init(_currentUserLoginData);
    }

    private void AddListeners() {
        _authorizationDialog.OnResultChanged += HideAuthorizationDialog;
        _registrationDialog.OnResultChanged += HideRegistrationDialog;
        
        _emailConfirmationDialog.Confirmation += HideEmailConfirmationDialog;
        _registration.Registered += ShowAuthorizationDialog;
    }

    private void ShowEmailConfirmationDialog() {
        _currentUserLoginData.EmailConfirmCode = PasswordGenerator.Generate(EmailConfirmCodeLenght);
        _emailConfirmationDialog.Init(_currentUserLoginData);

        _emailSendler.SendMessageToMail(_currentUserLoginData.Email, _currentUserLoginData.EmailConfirmCode);
    }

    private void HideEmailConfirmationDialog(bool status) {
        if (status) {
            _emailConfirmationDialog.Hide();
            TryRegistration(_currentUserLoginData);
        } 
    }

    private void HideRegistrationDialog(UserLoginData user) {
        if (user.EmailConfirmCode != "") {
            _authorizationDialog.Init(_currentUserLoginData);  
        } else if (user.Email != null) {
            _currentUserLoginData = user;
            ShowEmailConfirmationDialog();   
        }

        _registrationDialog.Hide();
    }

    private void ShowAuthorizationDialog(InfoType type, string text) {
        if (type == InfoType.Error) {
            _registrationDialog.Init(_currentUserLoginData);
            _registrationDialog.ShowInfo(type, text);
        } else {
            _authorizationDialog.Init(_currentUserLoginData);
            _authorizationDialog.ShowInfo(type, text);
        }
    }

    private void HideAuthorizationDialog(UserLoginData user) {
        if (user.Password != null) {
            Authorization?.Invoke();
            Debug.Log("Успешная авторизация!");
        } else {
            _registrationDialog.Init(user);
            _authorizationDialog.Hide();
        }
    }

    private void TryRegistration(UserLoginData user) {
        _registration.SignUp(_currentUserLoginData);
    }

    private void OnDisable() {
        _emailConfirmationDialog.Confirmation -= HideEmailConfirmationDialog;
        _registration.Registered -= ShowAuthorizationDialog;
    }
}
