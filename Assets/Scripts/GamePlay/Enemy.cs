using System;
using System.Collections;
using System.Linq;
using Enums;
using Service;
using UnityEngine;

namespace GamePlay
{ 
    public class Enemy : MonoBehaviour 
    {
        private WaypointPath _path;
        
        private int _currentIndex = 0;
        private float _speed = 3f;
        private float _arriveThreshold = 0.2f;
        private int _health;
        private int _givenMoneyOnDie;
        private SpriteRenderer _spriteRenderer;
        private HealthBarManager _healthBarManager;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (_path.count == 0) return;

            Transform target = _path.GetPoint(_currentIndex);
            if (target == null) return;

            //Move
            Vector3 pos = transform.position;
            Vector3 targetPos = target.position;
            Vector3 newPos = Vector3.MoveTowards(pos, targetPos, _speed * Time.deltaTime);
            transform.position = newPos;

            //rotate
            Vector3 dir = targetPos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward); 
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            

            // Check arrival
            if (Vector3.Distance(transform.position, targetPos) <= _arriveThreshold)
            {
                if (_path.count == 0) return;
                _currentIndex += 1;
                if (_currentIndex >= _path.count)
                {
                    //reduce Life
                    ServiceLocator.Get<ResourceManager>().ReduceLife();
                    ServiceLocator.Get<HealthBarManager>().RemoveHealthBar(transform);
                    ServiceLocator.Get<ObjectPool>().ReturnToPool(PoolObjectType.Enemy, gameObject);
                }
            }
        }

        public void GetShot(int damage)
        {
            _health -= damage;
            _health = Mathf.Clamp(_health, 0, int.MaxValue);
            _healthBarManager.SetHealth(transform,_health);
            if (_health <= 0)
                Die();

        }

        private void Die()
        {
            ServiceLocator.Get<HealthBarManager>().RemoveHealthBar(transform);
            ServiceLocator.Get<EnemySpawner>().RemoveEnemy(this,_givenMoneyOnDie);
        }

        public void Init(EnemyType enemyType)
        {
            _healthBarManager = ServiceLocator.Get<HealthBarManager>();
            
            var enemyData = ServiceLocator.Get<EnemyDataManger>().EnemyDataGetter
                .FirstOrDefault(a => a.enemyType == enemyType);

            _givenMoneyOnDie = enemyData.givenMoneyAfterDeath;
            _health = enemyData.health;
            _speed = enemyData.speed;
            _spriteRenderer.sprite = enemyData.sprite;
            
            
            _healthBarManager.CreateHealthBar(gameObject.transform,_health);
            _path = ServiceLocator.Get<WaveManager>().waypointPath;
            if (_path == null)
            {
                Debug.LogError("Enemy: No WaypointPath assigned.");
                enabled = false;
                return;
            }
            
            _currentIndex = 0;
            var startPoint = _path.GetPoint(_currentIndex);
            if (startPoint != null)
                transform.position = startPoint.position;
        }
    }
}