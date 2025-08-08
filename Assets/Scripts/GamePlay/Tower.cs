using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Service;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;       
        private SpriteRenderer _spriteRenderer;
        private float _range = 0;          
        private float _delayBetweenShoot = 0;        
        private float _turnSpeed = 0;
        private GameObject _bulletPrefab;   
        private Transform _target;
        private float _fireCountdown = 0f;
        private EnemySpawner _enemySpawner;
        private ObjectPool _objectPool;
        private AudioManager _audioManager;
        private float _bulletSpeed;
        private int _damage;
        private Sprite _bulletSprite;
        private AudioClipType _shootSound;
        private Vector3 _basePosition;


        private void Awake()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void Init(TowerType towerType)
        {
            
            var towerData = ServiceLocator.Get<TowersDataManager>().TowersDataGetter
                .FirstOrDefault(t => t.towerType == towerType);
            _spriteRenderer.sprite = towerData.sprite;
            _range = towerData.range;
            _delayBetweenShoot = towerData.delayBetweenShoot;
            _turnSpeed = towerData.turnSpeed;
            _objectPool = ServiceLocator.Get<ObjectPool>();
            _audioManager = ServiceLocator.Get<AudioManager>();
            _damage = towerData.damage;
            _bulletSpeed = towerData.bulletSpeed;
            _bulletSprite = towerData.bulletSprite;
            _shootSound = towerData.shootSound;
            var waypointPath = ServiceLocator.Get<WaveManager>().waypointPath;
            _basePosition = waypointPath.GetPoint(waypointPath.count - 1).position;


        }


        void Update()
        {
            FindTarget();

            if (_target == null)
                return;

            LockOnTarget();

            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = _delayBetweenShoot;
            }

            _fireCountdown -= Time.deltaTime;
        }

        void FindTarget()
        {
            List<Enemy> enemies = ServiceLocator.Get<EnemySpawner>().enemies;
            float shortestDistance = Mathf.Infinity;
            Enemy nearestEnemy = null;

            foreach (Enemy enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                float enemyDistanceToBase = Vector3.Distance(_basePosition, enemy.transform.position);
                if (enemyDistanceToBase < shortestDistance && distanceToEnemy <= _range)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
                _target = nearestEnemy.transform;
            else
                _target = null;
        }

        void LockOnTarget()
        {
            Vector3 dir = _target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90f; // because default sprite faces up instead of right
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rot;
        }

        void Shoot()
        {
            GameObject bulletGo = _objectPool.GetFromPool(PoolObjectType.Bullet);
            bulletGo.transform.position = firePoint.position;
            Bullet bullet = bulletGo.GetComponent<Bullet>();
            bullet.Init(_target,_bulletSpeed,_damage,_bulletSprite);
            _audioManager.PlaySfx(_shootSound);
            
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}

