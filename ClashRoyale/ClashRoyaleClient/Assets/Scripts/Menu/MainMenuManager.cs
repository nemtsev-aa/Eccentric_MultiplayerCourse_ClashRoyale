using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private DeckLoader _deckLoader;
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private CardSelector _cardSelector;
    [Header("Dialogs")]
    [SerializeField] private MainMenuDialog _mainMenuDialog;
    [SerializeField] private SelectCardsDialog _selectCardsDialog;

    private void Start() {
        InitDialogs();
        
        _deckLoader.StartLoad();
        CreateSubscriptions();
        
        ShowMainMenuDialog();
    }

    private void InitDialogs() {
        _mainMenuDialog.Init(_cardSelector);
        _selectCardsDialog.Init(_cardSelector);
    }

    private void CreateSubscriptions() {
        _mainMenuDialog.OnStartGame += StartGame;
        _mainMenuDialog.OnHideDialog += ShowSelectCardsDialog;
        _selectCardsDialog.OnHideDialog += ShowMainMenuDialog;

        _deckManager.UpdateSelectedCards += _cardSelector.SetSelectedCardsList;
        _deckManager.UpdateAvailableCards += _cardSelector.SetAvailableCardsList;

        _cardSelector.OnSetSelectedCards += _mainMenuDialog.SelectionCardsPanel.SetCardsList;
        _cardSelector.OnCardsListChanged += _mainMenuDialog.SelectionCardsPanel.UpdateSelectionCardsPanel;

        _cardSelector.OnSetSelectedCards += _selectCardsDialog.SelectionCardsPanel.SetCardsList;
        _cardSelector.OnCardsListChanged += _selectCardsDialog.SelectionCardsPanel.UpdateSelectionCardsPanel;

        _cardSelector.OnSetAvailableCards += _selectCardsDialog.AvailableDeckPanel.SetCardsList;
        _cardSelector.OnCardsListChanged += _selectCardsDialog.AvailableDeckPanel.UpdateAvailableDeckPanel;
    }

    private void StartGame() {
      
    }

    private void ShowSelectCardsDialog() {
        _mainMenuDialog.gameObject.SetActive(false);
        _selectCardsDialog.gameObject.SetActive(true);
        _selectCardsDialog.PrepDialog();
    }

    private void ShowMainMenuDialog() {
        _selectCardsDialog.gameObject.SetActive(false);
        _mainMenuDialog.gameObject.SetActive(true);
    }

    private void OnDestroy() {
        _mainMenuDialog.OnStartGame -= StartGame;
        _mainMenuDialog.OnHideDialog -= ShowSelectCardsDialog;
        _selectCardsDialog.OnHideDialog -= ShowMainMenuDialog;

        _deckManager.UpdateSelectedCards -= _mainMenuDialog.SelectionCardsPanel.SetCardsList;
        _deckManager.UpdateSelectedCards -= _selectCardsDialog.SelectionCardsPanel.SetCardsList;
        _deckManager.UpdateCards -= _selectCardsDialog.AvailableDeckPanel.SetCardsList;

        _deckManager.UpdateSelectedCards -= _cardSelector.SetSelectedCardsList;
        _deckManager.UpdateAvailableCards -= _cardSelector.SetAvailableCardsList;

        _cardSelector.OnCardsListChanged -= _mainMenuDialog.SelectionCardsPanel.UpdateSelectionCardsPanel;
        _cardSelector.OnCardsListChanged -= _selectCardsDialog.SelectionCardsPanel.UpdateSelectionCardsPanel;
        _cardSelector.OnCardsListChanged -= _selectCardsDialog.AvailableDeckPanel.UpdateAvailableDeckPanel;
    }
}