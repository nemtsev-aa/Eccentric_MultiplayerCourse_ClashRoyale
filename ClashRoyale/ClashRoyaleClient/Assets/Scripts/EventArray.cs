using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventArray : MonoBehaviour {
    [SerializeField] private List<UnityEvent> _events;

    public void StartEvent1() {
        _events[0].Invoke();
    }

    public void StartEvent2() {
        _events[1].Invoke();
    }
}
