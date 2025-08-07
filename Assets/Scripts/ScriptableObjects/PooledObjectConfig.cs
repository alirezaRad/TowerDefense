using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PooledObjectConfig", menuName = "ScriptableObjects/Configs/ObjectPoolConfig")]
    public class PooledObjectConfig : ScriptableObject
    {
        public List<PoolItem> items;
    }

    [System.Serializable]
    public class PoolItem
    {
        public PoolObjectType key;
        public GameObject prefab;
        public int initialCount;
    }
}