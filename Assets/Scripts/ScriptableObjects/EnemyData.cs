using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public List<EnemyDataStruct> enemyData = new List<EnemyDataStruct>();
    }
    
    [System.Serializable]
    public struct EnemyDataStruct
    {
        [ShowAssetPreview]
        public Sprite sprite;
        public int health;
        public float speed;
        public EnemyType enemyType;
    }
    
}