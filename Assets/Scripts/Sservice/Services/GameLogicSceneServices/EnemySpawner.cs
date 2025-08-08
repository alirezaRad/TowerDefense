using System.Collections;
using Enums;
using GamePlay;
using Sservice;
using Unity.VisualScripting;
using UnityEngine;

namespace Service
{
    public class EnemySpawner : MonoBehaviour,IService
    {
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
        }
        
        
        public void Load()
        {
        }
    }
}

