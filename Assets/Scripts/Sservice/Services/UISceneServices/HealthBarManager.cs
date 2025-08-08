
using System.Collections.Generic;
using Enums;
using Sservice;
using UI;
using UnityEngine;

namespace Service{

    public class HealthBarManager : MonoBehaviour,IService
    {
        public Canvas uiCanvas;
        private ObjectPool _objectPool;

        private Dictionary<Transform, HealthBar> healthBars = new();
        
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
            _objectPool = ServiceLocator.Get<ObjectPool>();
        }
        private void Register()
        {
            ServiceLocator.Register(typeof(HealthBarManager),this);
        }
        void Update()
        {
            List<Transform> toRemove = new();

            foreach (var kvp in healthBars)
            {
                Transform target = kvp.Key;
                HealthBar bar = kvp.Value;

                if (target == null)
                {
                    toRemove.Add(target);
                    Destroy(bar.gameObject);
                    continue;
                }

                Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + Vector3.up * 0.3f);
                bar.transform.position = screenPos;

            }

            foreach (var t in toRemove)
            {
                healthBars.Remove(t);
            }
        }

        public void CreateHealthBar(Transform target,int maxHealth)
        {
            if (healthBars.ContainsKey(target)) return;

            GameObject barGo = _objectPool.GetFromPool(PoolObjectType.HeathBar);
            HealthBar bar = barGo.GetComponent<HealthBar>();
            healthBars.Add(target, bar);
            bar.transform.SetParent(uiCanvas.transform);
            bar.Init(maxHealth);
        }

        public void RemoveHealthBar(Transform target)
        {
            if (healthBars.TryGetValue(target, out var bar))
            {
                healthBars.Remove(target);
                _objectPool.ReturnToPool(PoolObjectType.HeathBar,bar.gameObject);
            }
        }

        public void SetHealth(Transform target, float normalizedHealth)
        {
            if (healthBars.TryGetValue(target, out var bar))
            {
                bar.SetHealth(normalizedHealth);
            }
        }
        


        public void Load()
        {
        }
    }

}