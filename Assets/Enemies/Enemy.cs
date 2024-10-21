using Invader.General;
using Invader.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Invader.Enemies
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        public UnityAction ReturnToPool { get; set; }
        
        private float _movementSpeed;
        private Movement _movement;
        private Health _health;
        
        private void Awake()
        {
            TryGetComponent(out _movement);
            TryGetComponent(out _health);
            _health.OnDeath += HandleDeath;
        }
        
        public void Initialize(int health, float movementSpeed)
        {
            _health.MaxHealth = health;
            _movement.Speed = movementSpeed;
        }

        private void Update()
        {
            _movement.Move(Vector3.back);
        }
        
        private void OnDestroy()
        {
            _health.OnDeath -= HandleDeath;
        }
        
        private void HandleDeath()
        {
            ReturnToPool?.Invoke();
            Debug.Log($"{gameObject.name} has died.");
        }
    }
}
