using System;

[Serializable]
public class DeckData {
    public Availablecard[] availableCards;
    public string[] selectedIDs;
}

[Serializable]
public class Availablecard {
    public string name;
    public string id;
}
