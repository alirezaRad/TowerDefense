using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
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
        [ShowAssetPreview]
        public Sprite sprite;
        public int price;
        public TowerType towerType;
        public float range;
        public float turnSpeed;
        public float delayBetweenShoot;
        [ShowAssetPreview]
        public Sprite bulletSprite;
        public int damage;
        public float bulletSpeed;
        public AudioClipType shootSound;
    }
    
}