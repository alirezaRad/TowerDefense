using System.Collections;
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
        private EnemySpawner _enemySpawner;
    
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,StartWaves);
        }

        private void StartWaves()
        {
            _enemySpawner = ServiceLocator.Get<EnemySpawner>();
            StartCoroutine(RunWaves());
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(WaveManager),this);
        }
        
        IEnumerator RunWaves()
        {
            foreach (var wave in waveData.subWaves)
            {
                foreach (var subWave in wave.subWaveData)
                {
                    yield return new WaitForSeconds(wave.delayBetweenSubWaves);
                    for (int i = 0; i < subWave.numberOfEnemies; i++)
                    {
                        _enemySpawner.SpawnOne(subWave.enemyType);
                        yield return new WaitForSeconds(subWave.delayBetweenUnit);
                    }
                }
                
                yield return new WaitForSeconds(waveData.delayBetweenWaves);
            }
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