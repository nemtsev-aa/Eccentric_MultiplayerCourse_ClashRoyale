using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParameters : MonoBehaviour {
    [field: SerializeField] public float Speed { get; private set; } = 4f;
    [field: SerializeField] public float ModelRadius { get; private set; } = 1f;
    [field: SerializeField] public float StartChaseDistance { get; private set; } = 5f;
    [field: SerializeField] public float StopChaseDistance { get; private set; } = 7f;
    public float StartAttackDistance { get { return ModelRadius + _startAttackDistance; } }
    public float StopAttackDistance { get { return ModelRadius + _stopAttackDistance; } }
    
    [SerializeField] private float _startAttackDistance = 1f;
    [SerializeField] private float _stopAttackDistance = 1.5f;
}
