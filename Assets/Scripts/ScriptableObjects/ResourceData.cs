using Enums;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ResourceData", menuName = "ScriptableObjects/ResourceData")]
    public class ResourceData : ScriptableObject
    {
        public int life;
        public int money;
    }
    
}