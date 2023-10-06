using System;
using System.Collections.Generic;
using UnityEngine;

public class Registration : MonoBehaviour {
    private const string LOGIN = "login";
    private const string PASSWORD = "password";

    private UserLoginData _user;

    public event Action<InfoType, string> Registered;
    
    public void SignUp(UserLoginData user) {
        Dictionary<string, string> data = new Dictionary<string, string>() {
            {LOGIN, user.Email },
            {PASSWORD, user.Password },
        };

        Network.Instance.Post(URLLibrary.Registration, data, Success, ErrorMessage);
    }

    private void Success(string data) {
        if (!data.Contains("ok")) {
            ErrorMessage("Ответ с сервера: " + data);
            return;
        }

        Registered?.Invoke(InfoType.Information, $"Успешная регистрация!");
    }

    private void ErrorMessage(string error) {
        Debug.LogError(error);
        Registered?.Invoke(InfoType.Error, error);
    }
}
