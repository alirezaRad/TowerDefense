using System.Linq;
using Enums;
using ScriptableObjects;
using Sservice;
using UnityEngine;

namespace Service
{
    public class ResourceManager : MonoBehaviour,IService
    {
        public int life
        {
            get { return _life; }
        }
        public int money
        {
            get { return _money; }
        }

        private int _life;
        private int _money;
        
        private ResourceData resouceData => ServiceLocator.Get<ScriptableObjectsFacade>().resourceData;
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Load);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(ResourceManager),this);
        }
        
        public void Load()
        {
            _life = resouceData.life;
            _money = resouceData.money;
        }
        


        public void ReduceTowerMoney(TowerType towerType)
        {
            _money -= ServiceLocator.Get<TowersDataManager>().TowersDataGetter
                .FirstOrDefault(t => t.towerType == towerType).price;
            
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
        }

        public void ReduceLife()
        {
            _life -= 1;
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
            if(_life <= 0)
                ServiceLocator.Get<EventManager>().Raise(GameEventType.GameOver);
                
        }

        public void IncreaseMoney(int givenMoneyOnDie)
        {
            _money += givenMoneyOnDie;
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
        }
    }
}