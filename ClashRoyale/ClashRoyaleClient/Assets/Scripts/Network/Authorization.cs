using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Authorization : MonoBehaviour {
    private const string LOGIN = "login";
    private const string PASSWORD = "password";

    private string _login;
    private string _password;

    public event Action<InfoType, string> OnResult;
    public void SetLogin(string login) {
        _login = login;
    }

    public void SetPassword(string password) {
        _password = password;
    }

    public void SignIn() {
        if(string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password)) {
            ErrorMessage("Логин и/или пароль пустые!");
            return;
        }

        Dictionary<string, string> data = new Dictionary<string, string>() {
            {LOGIN, _login },
            {PASSWORD, _password }
        };

        Network.Instance.Post(URLLibrary.Authorization, data, Success, ErrorMessage);
    }

    private void Success(string data) {
        string[] result = data.Split('|');
        if (result.Length < 2 || result[0] != "ok") {
            ErrorMessage("Ответ с сервера: " + data);
            return;
        }

        if (int.TryParse(result[1], out int id)) {
            UserInfo.Instance.SetID(id);
            UserInfo.Instance.SetLogin(_login);

            OnResult?.Invoke(InfoType.Information, $"Успешная авторизация: ID {id}");
            SceneManager.LoadScene(1);
        } else {
            ErrorMessage($"Не удалось получить INT из {result[1]}");
        }
    }

    public void ErrorMessage(string error) {
        Debug.LogError(error);
        OnResult?.Invoke(InfoType.Error, error);
    }
}
