using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Sservice;
using UnityEngine;

namespace Service
{
    public class TowersDataManager: MonoBehaviour,IService
    {
        private TowersData towersData => ServiceLocator.Get<ScriptableObjectsFacade>().towersData;
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(TowersDataManager),this);
        }
        
        public void Load()
        {
        }

        public List<TowerDataStruct> TowersDataGetter => towersData.towerData;

    }
}