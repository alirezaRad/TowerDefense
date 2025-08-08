using ScriptableObjects;
using Sservice;
using UnityEngine;
using UnityEngine.Serialization;

namespace Service
{
    public class ScriptableObjectsFacade : MonoBehaviour, IService
    {
        public ScriptableObjectEntry entry;
        [HideInInspector] public AudioClipsData audioClipData => entry.audioClipData;
        [HideInInspector]public ObjectPoolData objectPoolPool => entry.objectPoolPool; 
        [HideInInspector]public ResourceData resourceData => entry.resourceData;
        [HideInInspector]public TowersData towersData => entry.towersData;
        [HideInInspector]public WaveData waveData => entry.waveData;
        
        
        public void Load()
        {
        }
    }
}