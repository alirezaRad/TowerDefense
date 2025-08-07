using System;
using Enums;
using ScriptableObjects;
using Service.Services.GameLogicSceneServices;
using Sservice;
using TMPro;
using UI;
using UnityEngine;

namespace Service
{
    public class GameUIManager : MonoBehaviour,IService
    {
        [SerializeField] private TextMeshProUGUI lifeText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TowerBuildButton buildTowerButtonTemplate;
        
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,Register);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.ResourceChange,UpdateResource);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(GameUIManager),this);
        }

        private void UpdateResource()
        {
            lifeText.text = ServiceLocator.Get<ResourceManager>().life.ToString();
            moneyText.text = ServiceLocator.Get<ResourceManager>().money.ToString();
        }

        public void Load()
        {
        }
    }
}