using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionCardsPanel : MonoBehaviour {
    [Header("CardToggleList")]
    [SerializeField] private CardToggle _cardTogglePrefab;
    [SerializeField] private Transform _cardToggleParent;
    [SerializeField] private List<CardToggle> _cardToggles = new List<CardToggle>();
    
    private IReadOnlyList<Card> _cardsList;
    private CardSelector _cardSelector;

    public void Init(CardSelector cardSelector) {
        _cardSelector = cardSelector;
    }

    public void SetCardsList(IReadOnlyList<Card> cardsList) {
        _cardsList = cardsList;
        CreateCardToggles();
        UpdateCardToggles();
    }

    public void UpdateSelectionCardsPanel(IReadOnlyList<Card> availableCardsList, IReadOnlyList<Card> selectedCardsList) {
        _cardsList = selectedCardsList;
        UpdateCardToggles();
    }

    private void CreateCardToggles() {
        if (_cardsList == null || _cardsList.Count() == 0) {
            Debug.LogError($"SelectionCardPanel: cardsList пуст!");
            return;
        }

        ClearCardToggleList();
        CreateCardToggleList();
    }

    private void CreateCardToggleList() {
        for (int i = 0; i < _cardsList.Count; i++) {
            CreateCardToggle(_cardsList[i], i);
        }
    }

    private void CreateCardToggle(Card card, int index) {
        CardToggle newCardToogle = Instantiate(_cardTogglePrefab, _cardToggleParent);
        newCardToogle.Init(index);
        newCardToogle.OnSelect += _cardSelector.SetSelectToogleIndex;

        _cardToggles.Add(newCardToogle);
    }

    private void UpdateCardToggles() {
        if (_cardToggles.Count == 0) CreateCardToggleList();

        for (int i = 0; i < _cardToggles.Count; i++) {
            //if (i <= _cardsList.Count-1) {
                _cardToggles[i].UpdateCard(_cardsList[i]);
            //}  
        }
    }

    private void ClearCardToggleList() {
        if (_cardToggles.Count == 0) return;
        foreach (var iCardToggle in _cardToggles) {
            iCardToggle.OnSelect -= _cardSelector.SetSelectToogleIndex;
            
            Destroy(iCardToggle.gameObject);
        }

        _cardToggles.Clear();
    }
}
