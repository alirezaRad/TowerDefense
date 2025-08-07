using System;
using System.Net.NetworkInformation;
using Enums;
using Service;
using UnityEngine;

namespace GamePlay
{
    public class TowerSpot : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,()=>{ReduceAlpha();});
        }

        private void ReduceAlpha()
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }
}