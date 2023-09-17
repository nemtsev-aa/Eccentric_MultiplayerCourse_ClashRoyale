using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventArray : MonoBehaviour {
    [SerializeField] private UnityEvent _event;

    public void StartEvent() {
        _event.Invoke();
    }
}
