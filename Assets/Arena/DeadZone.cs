using Invader.Enemies;
using Invader.Enemies.Abilities;
using Invader.General;
using Invader.Player;
using UnityEngine;
    
namespace Invader.Arena
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private int _damageToPlayer = 25;
        
        [SerializeField] private Utility.Events.GameEvent _healingEvent;
        
        private Health _playerHealth;
        
        private void Awake()
        {
            FindFirstObjectByType<PlayerController>().TryGetComponent(out _playerHealth);
            
            if (_playerHealth == null)
            {
                Debug.LogError("Player Health component not found.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out OnEventHealingAbility healingAbility))
            {
                _healingEvent.Raise();
            }
            
            if (other.TryGetComponent(out Enemy enemy))
            {
                Debug.Log($"{enemy.name} entered the DeadZone.");
                enemy.ReturnToPool?.Invoke();
                DamagePlayer(_damageToPlayer);
            }
        }
        
        private void DamagePlayer(int damageAmount)
        {
            _playerHealth?.TakeDamage(damageAmount);
        }
    }
}
