using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Sservice;
using UnityEngine;

namespace Service
{
    public class EnemyDataManger: MonoBehaviour,IService
    {
        private EnemyData enemyData => ServiceLocator.Get<ScriptableObjectsFacade>().enemyData;
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(EnemyDataManger),this);
        }
        
        public void Load()
        {
        }

        public List<EnemyDataStruct> EnemyDataGetter => enemyData.enemyData;

    }
}