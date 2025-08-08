using System.Collections;
using System.Collections.Generic;
using Enums;
using GamePlay;
using Sservice;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Service
{
    public class EnemySpawner : MonoBehaviour,IService
    {
        public List<Enemy> enemies => _enemies;
        
        private List<Enemy> _enemies = new List<Enemy>();

        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(EnemySpawner),this);
        }


        public void SpawnOne(EnemyType enemyType)
        {
            var enemy = ServiceLocator.Get<ObjectPool>().GetFromPool(PoolObjectType.Enemy);
            enemy.GetComponent<Enemy>().Init(enemyType);
            enemy.transform.SetParent(transform);
            _enemies.Add(enemy.GetComponent<Enemy>());
        }
        
        
        public void Load()
        {
        }

        public void RemoveEnemy(Enemy enemy, int givenMoneyOnDie)
        {
            _enemies.Remove(enemy);
            ServiceLocator.Get<ResourceManager>().IncreaseMoney(givenMoneyOnDie);
            ServiceLocator.Get<ObjectPool>().ReturnToPool(PoolObjectType.Enemy, enemy.gameObject);
        }
    }
}

