using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();
    public IReadOnlyList<Card> Cards { get { return _cards.ToList(); } }
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }
    
    public event Action<IReadOnlyList<Card>> UpdateCards;
    public event Action<IReadOnlyList<Card>> UpdateAvailableCards;
    public event Action<IReadOnlyList<Card>> UpdateSelectedCards;

    #region Editor
    private void OnValidate() {
        UpdateCards?.Invoke(Cards);
    }
    #endregion

    public void Init(List<int> availableCardIndexes, int[] selectedCardIndexes) {
        for (int i = 0; i < availableCardIndexes.Count; i++) {
            _availableCards.Add(_cards[availableCardIndexes[i]]);
        }

        for (int i = 0; i < selectedCardIndexes.Length; i++) {
            _selectedCards.Add(_cards[selectedCardIndexes[i]]);
        }

        UpdateCards?.Invoke(Cards);
        UpdateAvailableCards?.Invoke(AvailableCards);
        UpdateSelectedCards?.Invoke(SelectedCards);
    }
}