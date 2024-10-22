using System;
using Invader.General;
using Invader.Player;
using Invader.Utility.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Invader.Enemies
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        public UnityAction ReturnToPool { get; set; }
        
        [SerializeField] private GameEvent _addScoreEvent;
        [SerializeField] private GameEvent _addExperienceEvent;
        
        private float _movementSpeed;
        private Movement _movement;
        private Health _health;
        private int _scorePoints;
        private int _experiencePoints;
        
        private void Awake()
        {
            TryGetComponent(out _movement);
            TryGetComponent(out _health);
            _health.OnDeath += HandleDeath;
        }
        
        public void Initialize(int health, float movementSpeed, int scorePoints, int experiencePoints)
        {
            _health.MaxHealth = health;
            _movement.Speed = movementSpeed;
            _scorePoints = scorePoints;
            _experiencePoints = experiencePoints;
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
            _addScoreEvent.Raise(_scorePoints);
            _addExperienceEvent.Raise(_experiencePoints);
            Debug.Log($"{gameObject.name} has died.");
        }
    }
}
