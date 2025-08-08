using NaughtyAttributes;
using UnityEngine;

namespace ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "ScriptableObjectEntry", menuName = "ScriptableObjects/ScriptableObjectEntry")]
    public class ScriptableObjectEntry : ScriptableObject
    {
        [Expandable] public AudioClipsData audioClipData;
        [Expandable] public ObjectPoolData objectPoolPool; 
        [Expandable] public ResourceData resourceData;
        [Expandable] public TowersData towersData;
        [Expandable] public WaveData waveData;
    }
}