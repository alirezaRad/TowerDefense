using System;
using Enums;
using Service;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CancelBuildButton : MonoBehaviour
    {
        private Button _thisButton;

        private void Awake()
        {
            _thisButton = gameObject.GetComponent<Button>();
            _thisButton.onClick.AddListener((() =>
            {
                ServiceLocator.Get<AudioManager>().PlaySfx(AudioClipType.ButtonClick);
                ServiceLocator.Get<PlacementManager>().PlacementCancel();
            }));
        }

        private void Start()
        {
            gameObject.SetActive(false);
            
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementEnd, () =>
            {
                gameObject.SetActive(false);
            });
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementCancel, () =>
            {
                gameObject.SetActive(false);
            });
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementStart, () =>
            {
                gameObject.SetActive(true);
            });
            gameObject.SetActive(false);
        }
    }
}