using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PooledObjectData", menuName = "ScriptableObjects/ObjectPoolData")]
    public class ObjectPoolData : ScriptableObject
    {
        public List<PoolItem> items;
    }

    [System.Serializable]
    public struct PoolItem
    {
        public PoolObjectType key;
        public GameObject prefab;
        public int initialCount;
    }
}