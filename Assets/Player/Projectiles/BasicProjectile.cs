using System;
using Invader.General;
using UnityEngine;

namespace Invader.Player.Projectiles
{
    [Serializable]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Collider))]
    public class BasicProjectile : MonoBehaviour
    {
        [SerializeField] private int _damageAmount = 50;
        
        private Movement _movement;

        private void Awake()
        {
            TryGetComponent(out _movement);
        }
        
        private void Start()
        {
            Destroy(gameObject, 5.0f);
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

            Destroy(gameObject);
        }
    }
}
