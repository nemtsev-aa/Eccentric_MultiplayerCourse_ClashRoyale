using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    [SerializeField] private int NumberCardsInDeck = 3;

    private List<Card> _availableCards = new List<Card>();
    private List<Card> _selectedCards = new List<Card>();
    private int _selectToggleIndex; // Индекс выделенного слота в колоде

    public Action<IReadOnlyList<Card>> OnSetSelectedCards;
    public Action<IReadOnlyList<Card>> OnSetAvailableCards;
    public Action<IReadOnlyList<Card>, IReadOnlyList<Card>> OnCardsListChanged;
    
    public void SetSelectedCardsList(IReadOnlyList<Card> selectedCards) {
        _selectedCards.Clear();

        if (NumberCardsInDeck == selectedCards.Count) {
            foreach (var iCard in selectedCards) {
                _selectedCards.Add(iCard);
            }
        } else {
            for (int i = 0; i < NumberCardsInDeck; i++) {
                Card card = new Card();
                if (i <= selectedCards.Count-1) card = selectedCards[i]; 
                _selectedCards.Add(card);
            }
        }

        OnSetSelectedCards?.Invoke(_selectedCards);
    }

    public void SetAvailableCardsList(IReadOnlyList<Card> availableCards) {
        _availableCards.Clear();
        foreach (var iCard in availableCards) {
            _availableCards.Add(iCard);
        }

        OnSetAvailableCards?.Invoke(_availableCards);
    }
    
    public void SetSelectToogleIndex(int index) {
        _selectToggleIndex = index;
    }

    public void AddCardInDeck(int cardID) {
        _selectedCards[_selectToggleIndex] = _availableCards[cardID];
        OnCardsListChanged?.Invoke(_availableCards, _selectedCards);
    }

    public void UpdateCardsList() {
        OnCardsListChanged?.Invoke(_availableCards, _selectedCards);
    }
}
