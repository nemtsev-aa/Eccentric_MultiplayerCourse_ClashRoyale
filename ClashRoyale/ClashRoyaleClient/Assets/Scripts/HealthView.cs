using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour {
    private const string _damage = "Damage";
    private const string _healing = "Healing";

    [SerializeField] private GameObject _healthBar;
    [SerializeField] private RectTransform _filledImage;
    [SerializeField] private float _defaultWidth;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _damageValueText;

    private float _pastHealth;
    
    private void OnValidate() {
        _defaultWidth = _filledImage.sizeDelta.x;
    }

    public void Init() {
        _healthBar.SetActive(false);
    }

    public void UpdateHealth(float current, float maxHealth) {
        _healthBar.SetActive(true);

        float percent = current / maxHealth;
        _filledImage.sizeDelta = new Vector2(_defaultWidth * percent, _filledImage.sizeDelta.y);

        float value = 0f;
        if (_pastHealth > current) {
            value = _pastHealth - current;
            if (_damageValueText != null) _damageValueText.text = $"-{value}";           
            _animator.SetTrigger(_damage);
        } else {
            value = current - _pastHealth;
            if (_damageValueText != null) _damageValueText.text = $"+{value}";
            _animator.SetTrigger(_healing);
        }
        _pastHealth = current;
    }
}
