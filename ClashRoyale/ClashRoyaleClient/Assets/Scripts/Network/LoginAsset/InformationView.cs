using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InfoType {
    Information = 0,
    Error = 1,
}

public class InformationView : MonoBehaviour {
    private const float HideTextDelay = 3f;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _icon;
    [SerializeField] private List<Sprite> _icons = new List<Sprite>();

    public void Init() {
        _canvasGroup.alpha = 0f;
    }

    public void SetInfoValue(InfoType type, string text) {
        if (type == InfoType.Information) {
            _icon.sprite = _icons[0];
            _infoText.text = $"{text}";
        } else {
            _icon.sprite = _icons[1];
            _infoText.text = $"Ошибка: {text}";
        }
        StartCoroutine(ShowInformation(0, 1, 0.3f, _canvasGroup));
    }

    public void ResetView() {
        _canvasGroup.alpha = 0f;
        _icon.sprite = null;
        _infoText.text = $"";
    }

    public IEnumerator ShowInformation(float start, float end, float speed, CanvasGroup panelWithAdvice) {
        float time = 0f;
        while (time <= speed) {
            float t = Mathf.SmoothStep(0f, 1f, time / speed);
            panelWithAdvice.alpha = Mathf.Lerp(start, end, t);
            yield return null;
            time += Time.deltaTime;
        }

        yield return new WaitForSeconds(HideTextDelay);
        _canvasGroup.alpha = 0f;
    }
}
