using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDialog : MonoBehaviour {
    public SelectionCardsPanel SelectionCardsPanel => _selectionCardsPanel;

    [SerializeField] private SelectionCardsPanel _selectionCardsPanel;
    [SerializeField] private TextMeshProUGUI _loginText;
    [SerializeField] private TextMeshProUGUI _ratingText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _selectButton;

    private List<Image> _images = new List<Image>();

    public event Action OnHideDialog;
    public event Action OnStartGame;

    public void Init(CardSelector cardSelector) {
        _selectionCardsPanel.Init(cardSelector);

        _selectButton.onClick.AddListener(SelectButtonClick);
        _startButton.onClick.AddListener(StartButtonClick);
    }

    public void PrepDialog(int winCount, int loseCount) {
        UserInfo info = UserInfo.Instance;
        info.SetRating(winCount, loseCount);

        _loginText.text = info.Login;
        _ratingText.text = $"[<color=blue>{info.Rating.Wins}</color> : <color=red>{info.Rating.Loss}</color=red>]";
    }

    private void StartButtonClick() {
        OnStartGame?.Invoke();
    }

    private void SelectButtonClick() {
        OnHideDialog?.Invoke();
    }
}
