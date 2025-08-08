using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData")]
    public class WaveData : ScriptableObject
    {
        public float delayBetweenWaves;
        public List<WaveDataStruct> subWaves;
    }
    
    [System.Serializable]
    public struct WaveDataStruct
    {
        public List<SubWaveDataStruct> subWaveData;
        public float delayBetweenSubWaves;
    }
    
    [System.Serializable]
    public struct SubWaveDataStruct
    {
        public EnemyType enemyType;
        public int numberOfEnemies;
        public float delayBetweenUnit;
    }
}