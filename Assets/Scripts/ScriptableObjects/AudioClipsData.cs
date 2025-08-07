using Enums;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioClipsData", menuName = "ScriptableObjects/AudioClipsData")]
    public class AudioClipsData : ScriptableObject
    {
        [System.Serializable]
        public struct AudioClipEntry
        {
            public AudioClipType type;
            public AudioClip clip;
        }

        public AudioClipEntry[] clips;
    }
    
}