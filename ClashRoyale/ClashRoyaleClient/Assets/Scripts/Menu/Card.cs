using UnityEngine;

[System.Serializable]
public class Card {
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
