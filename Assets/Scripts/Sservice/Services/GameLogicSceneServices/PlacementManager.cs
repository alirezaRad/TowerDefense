using Enums;
using GamePlay;
using Sservice;
using UnityEngine;

namespace Service
{
    public class PlacementManager : MonoBehaviour,IService
    {
        public PlacementState PlacementState { get => _placementState; }
        private TowerType _selectedTowerType;
        private PlacementState _placementState;
        

        public void Start() 
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(PlacementManager),this);
        }

        public void PlacementStart(TowerType towerType)
        {
            _selectedTowerType = towerType;
            _placementState = PlacementState.Placing;
            ServiceLocator.Get<EventManager>().Raise(GameEventType.PlacementStart);
        }

        public void PlacementEnd(Transform placementTarget)
        {
            ServiceLocator.Get<ResourceManager>().ReduceTowerMoney(_selectedTowerType);
            var tower = ServiceLocator.Get<ObjectPool>().GetFromPool(PoolObjectType.Tower);
            tower.transform.position = placementTarget.transform.position;
            tower.transform.parent = placementTarget;
            tower.GetComponent<Tower>().Init(_selectedTowerType);
            _placementState = PlacementState.Idle;
            ServiceLocator.Get<EventManager>().Raise(GameEventType.PlacementEnd);
        }

        public void PlacementCancel()
        {
            _placementState = PlacementState.Idle;
            ServiceLocator.Get<EventManager>().Raise(GameEventType.PlacementCancel);
        }
        

        public void Load()
        {
        }
    }
}