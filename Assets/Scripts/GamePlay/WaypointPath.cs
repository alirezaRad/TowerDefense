using System;
using Enums;
using NaughtyAttributes;
using Service;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GamePlay
{
    public class WaypointPath : MonoBehaviour
    {
        private void Start()
        {
            ServiceLocator.Get<EventManager>().Subscribe(GameEventType.GameStart,RegisterThisToWaveManager);
        }

        private void RegisterThisToWaveManager()
        {
            ServiceLocator.Get<WaveManager>().RegisterWaypointPath(this);
        }

        public int count => _points != null ? _points.Length : 0;
        
        [SerializeField] private Transform[] _points;
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private float gizmoSphereRadius = 0.25f;
        
        public void CollectChildrenAsPoints()
        {
            int childCount = transform.childCount;
            _points = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
                _points[i] = transform.GetChild(i);
        }

        [Button]
        private void CreateWaypoint()
        {
            CollectChildrenAsPoints();
        }
        
        public Transform GetPoint(int index)
        {
            if (_points == null || _points.Length == 0) return null;
            index = Mathf.Clamp(index, 0, _points.Length - 1);
            return _points[index];
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos || _points == null || _points.Length == 0) return;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            for (int i = 0; i < _points.Length; i++)
            {
                if (_points[i] == null) continue;
                
                Gizmos.DrawSphere(_points[i].localPosition, gizmoSphereRadius);
                if (i < _points.Length - 1 && _points[i + 1] != null)
                    Gizmos.DrawLine(_points[i].localPosition, _points[i + 1].localPosition);
            }
        }
    }
}