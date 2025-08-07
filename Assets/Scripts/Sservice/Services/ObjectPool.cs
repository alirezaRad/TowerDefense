using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Service
{

    public class ObjectPool : MonoBehaviour
    {
        public PooledObjectConfig config;
  
        private readonly Dictionary<PoolObjectType, Queue<GameObject>> _poolDictionary = new();
        private readonly Dictionary<PoolObjectType, Transform> _parentDictionary = new();

        void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart, CreatePool);
        }

        private void CreatePool()
        {
            foreach (var item in config.items)
            {
                if (!_parentDictionary.ContainsKey(item.key))
                {
                    GameObject parentGo = new GameObject(item.key.ToString() + " Pool");
                    parentGo.transform.SetParent(transform);
                    _parentDictionary[item.key] = parentGo.transform;
                }

                Queue<GameObject> objectQueue = new();

                for (int i = 0; i < item.initialCount; i++)
                {
                    GameObject obj = Instantiate(item.prefab, _parentDictionary[item.key]);
                    obj.SetActive(false);
                    objectQueue.Enqueue(obj);
                }

                _poolDictionary[item.key] = objectQueue;
            }
        }

        public GameObject GetFromPool(PoolObjectType key)
        {
            if (!_poolDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Pool with key {key} doesn't exist!");
                return null;
            }

            GameObject obj;
            obj = _poolDictionary[key].Count > 0 ? _poolDictionary[key].Dequeue() : Instantiate(config.items.Find(x => x.key == key).prefab, _parentDictionary[key]);

            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }

        public void ReturnToPool(PoolObjectType key, GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_parentDictionary[key]);
            _poolDictionary[key].Enqueue(obj);
        }
    }

}