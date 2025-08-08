using Enums;
using GamePlay;
using ScriptableObjects;
using Sservice;
using UnityEngine;
using UnityEngine.Serialization;

namespace Service
{
    public class WaveManager : MonoBehaviour,IService
    {
        
        private WaveData waveData => ServiceLocator.Get<ScriptableObjectsFacade>().waveData;
        private WaypointPath _waypointPath;
        public WaypointPath waypointPath {get => _waypointPath;}
    
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(WaveManager),this);
        }

        public void RegisterWaypointPath(WaypointPath waypointPath)
        {
            if(_waypointPath == null)
                _waypointPath = waypointPath;
            else
                Debug.LogError("Already registered waypointPath");
        }
        public void Load()
        {
        }
    }
}