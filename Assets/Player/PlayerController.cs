using Invader.General;
using Invader.Utility.Events;
using UnityEngine;

namespace Invader.Player
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(PlayerWeapon))]
    [RequireComponent(typeof(PlayerAbility))]
    public class PlayerController : MonoBehaviour
    {
        public Vector2 Movement { get; set; }
        public bool IsFireButtonPressed { get; set; }
        public bool IsSpecialButtonPressed { get; set; }
        
        [SerializeField] private int _maxHealth = 1000;
        [SerializeField] private GameEvent _gameOverEvent;
        
        private Health _health;
        private Movement _movement;
        private PlayerWeapon _playerWeapon;
        private PlayerAbility _playerAbility;
        
        private void Awake()
        {
            if (!TryGetComponent(out _health))
            {
                Debug.LogError("Health component not found.");
            }
            if (!TryGetComponent(out _movement))
            {
                Debug.LogError("Movement component not found.");
            }
            if (!TryGetComponent(out _playerWeapon))
            {
                Debug.LogError("PlayerWeapon component not found.");
            }
            if (!TryGetComponent(out _playerAbility))
            {
                Debug.LogError("PlayerAbility component not found.");
            }
            
            _health.OnDeath += OnDeath;
        }

        private void Start()
        {
            _health.MaxHealth = _maxHealth;
        }
        
        private void Update()
        {
            _movement.Move(new Vector3(Movement.x, 0.0f, Movement.y));
            
            if (IsFireButtonPressed)
            {
                _playerWeapon.Fire();
            }

            if (IsSpecialButtonPressed)
            {
                _playerAbility.Use();
                IsSpecialButtonPressed = false;
            }
        }
        
        private void OnDeath()
        {
            _gameOverEvent.Raise();
        }
    }
}
