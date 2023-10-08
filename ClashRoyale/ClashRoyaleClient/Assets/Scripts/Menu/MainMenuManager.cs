using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private RatingManager _ratingManager;
    [SerializeField] private DeckLoader _deckLoader;
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private CardSelector _cardSelector;
    [Header("Dialogs")]
    [SerializeField] private MainMenuDialog _mainMenuDialog;
    [SerializeField] private SelectCardsDialog _selectCardsDialog;
    [SerializeField] private LoadScreenDialog _loadScreenDialog;

    private void Start() {
        _ratingManager.Init();
        
        InitDialogs();

        _deckLoader.StartLoad();
        _cardSelector.Init(_deckManager);
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
        _ratingManager.OnRatingChanged += _mainMenuDialog.PrepDialog;

        _selectCardsDialog.OnHideDialog += ShowMainMenuDialog;
        _selectCardsDialog.OnSaveCardsList += _cardSelector.SaveChanges;

        _deckLoader.OnStartLoad += ShowLoadScreenDialog;
        _deckLoader.OnSuccessLoad += _deckManager.Init;

        _deckManager.ChangDeckStarted += ShowLoadScreenDialog;
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

    private void ShowLoadScreenDialog(bool status) {
        _loadScreenDialog.gameObject.SetActive(status);
    }

    private void OnDestroy() {
        _mainMenuDialog.OnStartGame -= StartGame;
        _mainMenuDialog.OnHideDialog -= ShowSelectCardsDialog;
        _ratingManager.OnRatingChanged -= _mainMenuDialog.PrepDialog;
        _selectCardsDialog.OnHideDialog -= ShowMainMenuDialog;
        _selectCardsDialog.OnSaveCardsList -= _cardSelector.SaveChanges;

        _deckLoader.OnStartLoad -= ShowLoadScreenDialog;
        _deckLoader.OnSuccessLoad -= _deckManager.Init;

        _deckManager.ChangDeckStarted -= ShowLoadScreenDialog;
        _deckManager.UpdateSelectedCards -= _cardSelector.SetSelectedCardsList;
        _deckManager.UpdateAvailableCards -= _cardSelector.SetAvailableCardsList;

        _cardSelector.OnSetSelectedCards -= _mainMenuDialog.SelectionCardsPanel.SetCardsList;
        _cardSelector.OnCardsListChanged -= _mainMenuDialog.SelectionCardsPanel.UpdateSelectionCardsPanel;

        _cardSelector.OnSetSelectedCards -= _selectCardsDialog.SelectionCardsPanel.SetCardsList;
        _cardSelector.OnCardsListChanged -= _selectCardsDialog.SelectionCardsPanel.UpdateSelectionCardsPanel;

        _cardSelector.OnSetAvailableCards -= _selectCardsDialog.AvailableDeckPanel.SetCardsList;
        _cardSelector.OnCardsListChanged -= _selectCardsDialog.AvailableDeckPanel.UpdateAvailableDeckPanel;
    }
}