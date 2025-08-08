using System.Collections.Generic;
using Enums;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TowersData", menuName = "ScriptableObjects/TowersData")]
    public class TowersData : ScriptableObject
    {
        public List<TowerDataStruct> towerData = new List<TowerDataStruct>();
    }
    
    [System.Serializable]
    public struct TowerDataStruct
    {
        public Sprite sprite;
        public int price;
    }
    
}