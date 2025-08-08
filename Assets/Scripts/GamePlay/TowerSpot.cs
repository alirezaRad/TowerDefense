using System;
using System.Net.NetworkInformation;
using Enums;
using Service;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class TowerSpot : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Button _thisButton;
        private bool _isEmpty = true;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _thisButton = GetComponent<Button>();
            _thisButton.onClick.AddListener(ClickOnThis);
        }

        private void ClickOnThis()
        {
            if (ServiceLocator.Get<PlacementManager>().PlacementState == PlacementState.Placing)
            {
                if (!_isEmpty)
                {
                    ServiceLocator.Get<GameUIManager>().ShowMessage("Try Empty Tower Spot",1.5f);
                    return;
                }
                ServiceLocator.Get<AudioManager>().PlaySfx(AudioClipType.ButtonClick);
                _isEmpty = false;
                ServiceLocator.Get<PlacementManager>().PlacementEnd(transform);
            }
        }

        private void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,Deactivate);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementStart,Activate);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementEnd,Deactivate);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementCancel,Deactivate);
        }

        private void Deactivate()
        {
            if (_isEmpty)
            {
                _spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
                _thisButton.interactable = false;
            }

        }
        private void Activate()
        {
            if (_isEmpty)
            {
                _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                _thisButton.interactable = true;
            }
        }
    }
}