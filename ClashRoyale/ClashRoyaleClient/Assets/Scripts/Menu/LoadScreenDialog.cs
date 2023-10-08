using UnityEngine;
using UnityEngine.UI;

public class LoadScreenDialog : MonoBehaviour {
    [SerializeField] private Image _loadImage;
    [SerializeField] private float _speed;

    private void Update() {
        _loadImage.transform.Rotate(0, 0, _speed * Time.deltaTime);
    }
}