using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectCardsDialog : MonoBehaviour {
    public SelectionCardsPanel SelectionCardsPanel => _selectionCardsPanel;
    public AvailableDeckPanel AvailableDeckPanel => _availableDeckPanel;

    [SerializeField] private SelectionCardsPanel _selectionCardsPanel;
    [SerializeField] private AvailableDeckPanel _availableDeckPanel;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _cancelButton;

    public event Action OnHideDialog;
    public event Action OnSaveCardsList;

    private CardSelector _cardSelector;
    public void Init(CardSelector cardSelector) {
        _cardSelector = cardSelector;
        _selectionCardsPanel.Init(_cardSelector);
        _availableDeckPanel.Init(_cardSelector);

        _saveButton.onClick.AddListener(SaveCardsList);
        _cancelButton.onClick.AddListener(HideDialog);
    }
    
    public void PrepDialog() {
        _cardSelector.UpdateCardsList();
    }

    private void SaveCardsList() {
        OnSaveCardsList?.Invoke();
    }

    private void HideDialog() {
        OnHideDialog?.Invoke();
    }
}