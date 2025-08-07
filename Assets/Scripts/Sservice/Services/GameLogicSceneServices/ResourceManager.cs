using Enums;
using ScriptableObjects;
using Sservice;
using UnityEngine;

namespace Service.Services.GameLogicSceneServices
{
    public class ResourceManager : MonoBehaviour,IService
    {
        [SerializeField] private ResourceData resouceData;
        public void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,Register);
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,FirstResourceSet);
        }

        private void Register()
        {
            ServiceLocator.Register(typeof(ResourceManager),this);
        }

        private void FirstResourceSet()
        {
            ServiceLocator.Get<EventManager>().Raise(GameEventType.ResourceChange);
        }

        public void Load()
        {
        }
        
        public int life { get { return resouceData.life; }}
        public int money { get { return resouceData.money; }}
    }
}