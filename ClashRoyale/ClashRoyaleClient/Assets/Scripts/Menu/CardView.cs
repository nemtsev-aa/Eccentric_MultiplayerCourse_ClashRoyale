using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CardStateType {
    None = 0,
    Available = 1,
    Selected = 2,
    Locked = 3
}

[RequireComponent(typeof(Button))]
public class CardView : MonoBehaviour {
    public Card Card => _card;

    [SerializeField] private Button _cardViewButton;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _text;
    [Header("Status")]
    [SerializeField] private Image _statusImage;
    [Tooltip("Available = 0, Selected = 1, Locked = 2")]
    [SerializeField] private List<Sprite> _statusSprites = new List<Sprite>();

    public Action<int> OnSelect;

    private Card _card;
    private int _id;
    private CardStateType _currentState = CardStateType.Available;
    
    public void Init(Card card, int id) {
        _card = card;
        _id = id;

        _cardViewButton ??= GetComponent<Button>();
        _cardViewButton.onClick.AddListener(CardViewButtonClick);

        UpdatingAppearance();
        SetState(_currentState);
    }
    
    public void SetState(CardStateType state) {
        _currentState = state;

        switch (state) {
            case CardStateType.Available:
                _statusImage.sprite = _statusSprites[0];
                break;
            case CardStateType.Selected:
                _statusImage.sprite = _statusSprites[1];
                break;
            case CardStateType.Locked:
                _statusImage.sprite = _statusSprites[2];
                break;
            default:
                break;
        }
    }
    
    private void UpdatingAppearance() {
        _iconImage.sprite = _card.Sprite;
        _text.text = _card.Name;
    }

    private void CardViewButtonClick() {
        switch (_currentState) {
            case CardStateType.Available:
                OnSelect?.Invoke(_id);
                SetState(CardStateType.Selected);
                break;
            case CardStateType.Selected:
                
                break;
            case CardStateType.Locked:
                
                break;
            default:
                break;
        }
    }
}
