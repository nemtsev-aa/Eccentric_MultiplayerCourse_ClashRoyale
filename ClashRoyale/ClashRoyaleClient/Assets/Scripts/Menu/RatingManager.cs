using System;
using System.Collections.Generic;
using UnityEngine;

public class RatingManager : MonoBehaviour {
    public event Action<int, int> OnRatingChanged;
    public void Init() {
        string uri = URLLibrary.GetRating;
        var data = new Dictionary<string, string>() {
            {"userID", $"{UserInfo.Instance.ID}" }
        };

        Network.Instance.Post(uri, data, Success, Error);
    }

    private void Success(string message) {
        string[] result = message.Split('|');
        if (result.Length != 3) {
            Error($"Длинна массива != 3 " + message);
            return;
        }

        if (result[0] != "ok") {
            Error($"Отрицательный результат " + message);
            return;
        }

        OnRatingChanged?.Invoke(int.Parse(result[1]), int.Parse(result[2]));
    }

    private void Error(string message) {
        Debug.LogError("Ошибка получения рейтинга: " + message);
    }
}
