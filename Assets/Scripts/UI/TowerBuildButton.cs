
using System;
using Enums;
using Service;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI priceText;
        private Button _thisButton;
        private int _price;
        private TowerType _towerType;

        private void Awake()
        {
            _thisButton = GetComponent<Button>();
            _thisButton.interactable = false;
            _thisButton.onClick.AddListener(ClickOnThis);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.ResourceChange,CheckForActivation);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementStart, ()=>
                {gameObject.SetActive(false);});
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementEnd, ()=>
                {gameObject.SetActive(true);});
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.PlacementCancel, ()=>
                {gameObject.SetActive(true);});
        }
        

        public void Init(Sprite sprite,int price,TowerType towerType)
        {
            image.sprite = sprite;
            _price = price;
            priceText.text = price.ToString();
            _towerType = towerType;
        }

        public void CheckForActivation()
        {
            if(ServiceLocator.Get<ResourceManager>().money >= _price)
                _thisButton.interactable = true;
            else
                _thisButton.interactable = false;
        }

        private void ClickOnThis()
        {
            ServiceLocator.Get<AudioManager>().PlaySfx(AudioClipType.ButtonClick);
            ServiceLocator.Get<PlacementManager>().PlacementStart(_towerType);
        }
        
    }
}