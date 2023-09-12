using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    #region SingletonOneScene
    public static MapInfo Instance { get; private set; }
    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion

    [SerializeField] private List<Tower> _enemyTowers = new List<Tower>();
    [SerializeField] private List<Tower> _playerTowers = new List<Tower>();
    
    [SerializeField] private List<Unit> _enemyWalkingUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerWalkingUnits = new List<Unit>();
    [SerializeField] private List<Unit> _enemyFlyUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerFlyUnits = new List<Unit>();

    private void Start() {
        SubscribeDestroy(_enemyTowers);
        SubscribeDestroy(_playerTowers);
        SubscribeDestroy(_enemyWalkingUnits);
        SubscribeDestroy(_playerWalkingUnits);
        SubscribeDestroy(_enemyFlyUnits);
        SubscribeDestroy(_playerFlyUnits);
    }

    public void AddUnit(Unit unit) {
        List<Unit> list;
        if (unit.IsEnemy) list = unit.Parameters.IsFly ? _enemyFlyUnits : _enemyWalkingUnits;
        else list = unit.Parameters.IsFly ? _playerFlyUnits : _playerWalkingUnits;

        AddObjectToList(list, unit);
    }

    public  bool TryGetNearestAnyUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        TryGetNearestWalkingUnit(currentPosition, enemy, out Unit walkingUnit, out float walkingDistance);
        TryGetNearestFlyUnit(currentPosition, enemy, out Unit flyUnit, out float flyDistance);
        if (flyDistance < walkingDistance) {
            unit = flyUnit;
            distance = flyDistance;
        } else {
            unit = walkingUnit;
            distance = walkingDistance;
        }
        return unit;
    }

    public bool TryGetNearestFlyUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        List<Unit> units = enemy ? _enemyFlyUnits : _playerFlyUnits;
        unit = GetNearest(currentPosition, units, out distance);
        return unit;
    }

    public bool TryGetNearestWalkingUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        List<Unit> units = enemy ? _enemyWalkingUnits : _playerWalkingUnits;
        unit = GetNearest(currentPosition, units, out distance);
        return unit;
    }

    public Tower GetNearestTower(in Vector3 currentPosition, bool enemy) {
        List<Tower> towers = enemy ? _enemyTowers : _playerTowers;
        return GetNearest(currentPosition, towers, out float distance);
    }

    private void SubscribeDestroy<T>(List<T> objects) where T : IDestroyed {
        for (int i = 0; i < objects.Count; i++) {
            T obj = objects[i];

            void RemoveAndUnsubscrabe() {
                RemoveObjectFromList(objects, obj);
                obj.Destroyed -= RemoveAndUnsubscrabe;
            }

            obj.Destroyed += RemoveAndUnsubscrabe; 
        }
    }

    private T GetNearest<T>(in Vector3 currentPosition, List<T> objects, out float distance) where T: MonoBehaviour{
        distance = float.MaxValue;
        if (objects.Count <= 0) return null;

        distance = Vector3.Distance(currentPosition, objects[0].transform.position);
        T nearest = objects[0];

        for (int i = 1; i < objects.Count; i++) {
            float tempDistance = Vector3.Distance(currentPosition, objects[i].transform.position);
            if (tempDistance > distance) continue;

            nearest = objects[i];
            distance = tempDistance;
        }

        return nearest;
    }

    private void AddObjectToList<T>(List<T> objects, T obj) where T : IDestroyed {
        objects.Add(obj);
       
        void RemoveAndUnsubscrabe() {
            RemoveObjectFromList(objects, obj);
            obj.Destroyed -= RemoveAndUnsubscrabe;
        }

        obj.Destroyed += RemoveAndUnsubscrabe;
    }

    public void RemoveObjectFromList<T>(List<T> list, T obj) {
        if (list.Contains(obj)) list.Remove(obj);
    }
}
