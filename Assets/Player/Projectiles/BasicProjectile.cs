using System;
using System.Collections;
using Invader.General;
using UnityEngine;

namespace Invader.Player.Projectiles
{
    [Serializable]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Collider))]
    public class BasicProjectile : MonoBehaviour
    {
        public Action ReturnToPool { get; set; }
        
        [SerializeField] private int _damageAmount = 50;
        
        private Movement _movement;
        private Coroutine _lifetimeCoroutine;
        
        private void Awake()
        {
            TryGetComponent(out _movement);
        }
        
        private void OnEnable()
        {
            _lifetimeCoroutine = StartCoroutine(LifetimeCoroutine());
        }

        private void OnDisable()
        {
            if (_lifetimeCoroutine == null)
            {
                return;
            }
            
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }
        
        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(2.0f);
            ReturnToPool?.Invoke();
        }
        
        private void Update()
        {
            _movement.Move(Vector3.forward);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable == null)
            {
                return;
            }
            
            damageable.TakeDamage(_damageAmount);

            ReturnToPool?.Invoke();
        }
        
        public void SetExitPoint(Vector3 transformPosition)
        {
            transform.position = transformPosition;
        }
        
        public void SetDamage(int damage)
        {
            _damageAmount = damage;
        }
    }
}
