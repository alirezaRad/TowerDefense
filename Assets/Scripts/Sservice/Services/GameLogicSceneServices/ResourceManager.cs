using System.Linq;
using Enums;
using ScriptableObjects;
using Sservice;
using UnityEngine;

namespace Service
{
    public class ResourceManager : MonoBehaviour,IService
    {
        public int life { get { return resouceData.life; }}
        public int money { get { return resouceData.money; }}
        
        private ResourceData resouceData => ServiceLocator.Get<ScriptableObjectsFacade>().resourceData;
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(ResourceManager),this);
        }
        
        public void Load()
        {
        }
        


        public void ReduceTowerMoney(TowerType towerType)
        {
            resouceData.money -= ServiceLocator.Get<TowersDataManager>().TowersDataGetter
                .FirstOrDefault(t => t.towerType == towerType).price;
            
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
        }
    }
}