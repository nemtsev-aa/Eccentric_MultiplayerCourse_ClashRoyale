using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    public IReadOnlyList<Card> Cards { get { return _cards.ToList(); } }
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }

    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();

    public event Action<bool> ChangDeckStarted;
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

    public void ResetCardLists() {
        UpdateAvailableCards?.Invoke(AvailableCards);
        UpdateSelectedCards?.Invoke(SelectedCards);
    }

    public void ChangedDeck(IReadOnlyList<Card> selectedCards) {
        ChangDeckStarted?.Invoke(true);

        for (int i = 0; i < _selectedCards.Count; i++) {
            _selectedCards[i] = selectedCards[i];
        }

        int[] IDs = new int[selectedCards.Count];
        for (int i = 0; i < selectedCards.Count; i++) {
            IDs[i] = selectedCards[i].Id;
        }

        string json = JsonUtility.ToJson(new Wrapper(IDs));
        string uri = URLLibrary.SetSelectDeck;
        Dictionary<string, string> data = new Dictionary<string, string> {
            { "userID", $"{UserInfo.Instance.ID}" } , 
            { "json", json } 
        };

        Network.Instance.Post(uri, data, SendSuccess, Error);
    }

    private void SendSuccess(string message) {
        if (!message.Contains("ok")) {
            Error(message);
            return;
        }
        UpdateSelectedCards?.Invoke(_selectedCards);
        ChangDeckStarted?.Invoke(false);
    }

    private void Error(string message) {
        Debug.LogError($"Неудачная попытка отправки новой колоды: " + message);
        ResetCardLists();
        ChangDeckStarted?.Invoke(false);
    }
}