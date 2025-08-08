using System;
using System.Linq;
using Enums;
using Service;
using UnityEngine;

namespace GamePlay
{
    public class Tower : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void Init(TowerType towerType)
        {
            _spriteRenderer.sprite = 
                ServiceLocator.Get<TowersDataManager>().TowersDataGetter
                    .FirstOrDefault(t => t.towerType == towerType).sprite;
        }
    }
}