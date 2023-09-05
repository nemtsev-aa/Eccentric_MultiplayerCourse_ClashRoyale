using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParameters : MonoBehaviour {
    [field: SerializeField] public float StartAttackDistance { get; private set; } = 1f;
    [field: SerializeField] public float StopAttackDistance { get; private set; } = 1f;
    [field: SerializeField] public float Speed { get; private set; } = 4f;
}
