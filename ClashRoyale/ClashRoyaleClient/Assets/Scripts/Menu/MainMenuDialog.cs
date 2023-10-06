using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDialog : MonoBehaviour {
    public SelectionCardsPanel SelectionCardsPanel => _selectionCardsPanel;

    [SerializeField] private SelectionCardsPanel _selectionCardsPanel;
    [SerializeField] private TextMeshProUGUI _loginText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _selectButton;

    private List<Image> _images = new List<Image>();

    public event Action OnHideDialog;
    public event Action OnStartGame;

    public void Init(CardSelector cardSelector) {
        PrepDialog();

        _selectionCardsPanel.Init(cardSelector);

        _selectButton.onClick.AddListener(SelectButtonClick);
        _startButton.onClick.AddListener(StartButtonClick);
    }

    private void StartButtonClick() {
        OnStartGame?.Invoke();
    }

    private void PrepDialog() {
        _loginText.text = UserInfo.Instance.Login;
    }

    private void SelectButtonClick() {
        OnHideDialog?.Invoke();
    }
}
