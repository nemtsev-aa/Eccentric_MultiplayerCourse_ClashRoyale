using System;
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
    
    [SerializeField] private List<Unit> _enemyUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerUnits = new List<Unit>();

    private void Start() {
        SubscribeDestroy(_enemyTowers);
        SubscribeDestroy(_playerTowers);
        SubscribeDestroy(_enemyUnits);
        SubscribeDestroy(_playerUnits);
    }

    public  bool TryGetNearestUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        List<Unit> units = enemy ? _enemyUnits : _playerUnits;
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

    public void RemoveObject(IHealth obj, bool enemy) {       
        if (obj is Tower) {
            List<Tower> towers = enemy ? _enemyTowers : _playerTowers;
            towers.Remove(obj as Tower);
        } else if (obj is Unit) {
            List<Unit> units = enemy ? _enemyUnits : _playerUnits;
            units.Remove(obj as Unit);
        } 
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
