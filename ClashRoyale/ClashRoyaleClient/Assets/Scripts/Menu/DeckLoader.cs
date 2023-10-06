using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour {
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private List<int> _availableCards = new List<int>();
    [SerializeField] private int[] _selectedCards = new int[0];

    public void StartLoad() {
        Network.Instance.Post(URLLibrary.GetDeckInfo,
            new Dictionary<string, string> { { "userID", UserInfo.Instance.ID.ToString() } },
            SuccessLoad,
            ErrorLoad
            );
    }

    private void SuccessLoad(string data) {
        DeckData deckData = JsonUtility.FromJson<DeckData>(data);
        _selectedCards = new int[deckData.selectedIDs.Length];
        for (int i = 0; i < deckData.selectedIDs.Length; i++) {
            int.TryParse(deckData.selectedIDs[i], out _selectedCards[i]);
        }

        for (int i = 0; i < deckData.availableCards.Length; i++) {
            int.TryParse(deckData.availableCards[i].id, out int id);
            _availableCards.Add(id);
        }

        _deckManager.Init(_availableCards, _selectedCards);
    }

    private void ErrorLoad(string error) {
        Debug.LogError(error);
        StartLoad();
    }
}

