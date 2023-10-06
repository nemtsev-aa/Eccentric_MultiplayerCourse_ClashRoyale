using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CardToggle : MonoBehaviour {
    public Card Card => _card;

    [SerializeField] private Image _cardIcon;

    private Toggle _toggle;
    private Card _card;
    private int _index;

    public Action<int> OnSelect;
    
    public void Init(int index) {
        _index = index;

        _toggle ??= GetComponent<Toggle>();
        _toggle.group = transform.parent.GetComponent<ToggleGroup>();
        
        _toggle.onValueChanged.AddListener(SetStatus);
    }

    public void UpdateCard(Card card) {
        _card = card;
        if (_card.Sprite != null) _cardIcon.sprite = card.Sprite;
    }

    private void SetStatus(bool status) {
        if (status == false) return;
        OnSelect?.Invoke(_index);
    }
}
