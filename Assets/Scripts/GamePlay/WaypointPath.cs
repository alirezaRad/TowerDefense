using System;
using Enums;
using NaughtyAttributes;
using Service;
using UnityEngine;
using UnityEngine.Serialization;
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

        public int count => points != null ? points.Length : 0;
        
        [SerializeField] private Transform[] points;
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private float gizmoSphereRadius = 0.25f;
        
        public void CollectChildrenAsPoints()
        {
            int childCount = transform.childCount;
            points = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
                points[i] = transform.GetChild(i);
        }

        [Button]
        private void CreateWaypoint()
        {
            CollectChildrenAsPoints();
        }
        
        public Transform GetPoint(int index)
        {
            if (points == null || points.Length == 0) return null;
            index = Mathf.Clamp(index, 0, points.Length - 1);
            return points[index];
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos || points == null || points.Length == 0) return;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == null) continue;
                
                Gizmos.DrawSphere(points[i].localPosition, gizmoSphereRadius);
                if (i < points.Length - 1 && points[i + 1] != null)
                    Gizmos.DrawLine(points[i].localPosition, points[i + 1].localPosition);
            }
        }
    }
}