
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Sservice;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Service
{
    public class GameUIManager : MonoBehaviour,IService
    {
        [SerializeField] private TextMeshProUGUI lifeText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TowerBuildButton buildTowerButtonTemplate;
        [SerializeField] private GameObject winOrLosePanel;
        [SerializeField] private TextMeshProUGUI messageText;
        
        
        public void Start()
        {
            var eventManager = ServiceLocator.Get<EventManager>();
            eventManager.Subscribe(GameEventType.RegisterGamePlayService,Register);
            eventManager.Subscribe(GameEventType.GameStart,BuildTowerButtonsCreate);
            eventManager.Subscribe(GameEventType.GameStart,UpdateResource);
            eventManager.Subscribe(GameEventType.ResourceChange,UpdateResource);
            eventManager.Subscribe(GameEventType.ResourceChange,UpdateResource);
            eventManager.Subscribe(GameEventType.GameWin,()=> { GameEnd("You Win!"); });
            eventManager.Subscribe(GameEventType.GameOver,()=> {GameEnd("You Lose!"); });
        }
        

        private void GameEnd(string message)
        {
            Time.timeScale = 0;
            winOrLosePanel.SetActive(true);
            messageText.text = message;
            messageText.gameObject.SetActive(true);
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
                temp.Init(item.sprite,item.price,item.towerType);
            }
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
        }

        public void Load()
        {
        }
    }
}