using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvailableDeckPanel : MonoBehaviour {
    [SerializeField] private CardView _cardViewPrefab;
    [SerializeField] private Transform _cardsParent;
    [SerializeField] private List<CardView> _availableCardViews = new List<CardView>();

    private IReadOnlyList<Card> _cardsList;
    private CardSelector _cardSelector;

    public void Init(CardSelector cardSelector) {
        _cardSelector = cardSelector;
    }

    public void SetCardsList(IReadOnlyList<Card> cardsList) {
        _cardsList = cardsList;
        CreateCardViews();
    }

    public void UpdateAvailableDeckPanel(IReadOnlyList<Card> availableCardsList, IReadOnlyList<Card> selectedCardsList) {
        foreach (var iCard in availableCardsList) {
            CardView cardView = _availableCardViews.Find(c => c.Card == iCard);
            cardView.SetState(CardStateType.Available);
        }

        foreach (var iCard in selectedCardsList) {
            if (iCard.Sprite == null) continue;
            CardView cardView = _availableCardViews.Find(c => c.Card == iCard);
            cardView.SetState(CardStateType.Selected);
        }
    }

    private void CreateCardViews() {
        if (_cardsList == null || _cardsList.Count() == 0) {
            Debug.LogError($"AvailableDeckPanel: AvailableCardsList пуст!");
            return;
        }

        ClearCardViewList();
        CreateCardViewList();
    }
    
    private void CreateCardViewList() {
        for (int i = 0; i < _cardsList.Count; i++) {
            CreateCardView(_cardsList[i], i);
        }
    }
    
    private void CreateCardView(Card card, int id) {
        CardView newCardView = Instantiate(_cardViewPrefab, _cardsParent);
        newCardView.Init(card, id);
        newCardView.OnSelect += _cardSelector.AddCardInDeck;

        _availableCardViews.Add(newCardView);
    }

    private void ClearCardViewList() {
        if (_availableCardViews.Count == 0) return;
        foreach (var iCardView in _availableCardViews) {
            iCardView.OnSelect -= _cardSelector.AddCardInDeck;
            
            Destroy(iCardView.gameObject);
        }

        _availableCardViews.Clear();
    }
}
