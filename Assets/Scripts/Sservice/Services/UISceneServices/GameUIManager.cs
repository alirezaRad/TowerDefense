
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
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
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.RegisterGamePlayService,Register);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,BuildTowerButtonsCreate);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,UpdateResource);
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

        private void BuildTowerButtonsCreate()
        {
            List<TowerDataStruct> towersData = ServiceLocator.Get<TowersDataManager>().TowersDataGetter;
            foreach (var item in towersData)
            {
                Transform parent = buildTowerButtonTemplate.transform.parent;
                var temp = Instantiate(buildTowerButtonTemplate, parent);
                temp.gameObject.SetActive(true);
                temp.Init(item.sprite,item.price);
            }
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
        }

        public void Load()
        {
        }
    }
}