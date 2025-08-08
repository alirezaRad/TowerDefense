using System;
using Enums;
using Service;
using UnityEngine;

namespace GamePlay
{


    public class Bullet : MonoBehaviour
    {
        private Transform _target;
        private float _speed;
        private int _damage;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Transform target,float speed,int damage,Sprite sprite)
        {
            this._target = target;
            this._speed = speed;
            this._damage = damage;
            _spriteRenderer.sprite = sprite;
        }

        void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = _target.position - transform.position;
            float distanceThisFrame = _speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            
            LockOnTarget();
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        }
        void LockOnTarget()
        {
            Vector3 dir = _target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90f; // because default sprite faces up instead of right
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rot;
        }

        void HitTarget()
        {
            ServiceLocator.Get<ObjectPool>().ReturnToPool(PoolObjectType.Bullet,gameObject);
        }
    }

}