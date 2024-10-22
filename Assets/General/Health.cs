using System;
using UnityEngine;
using UnityEngine.Events;

namespace Invader.General
{
    public class Health : MonoBehaviour, IDamageable
    {
        public int MaxHealth
        {
            private get { return _maxHealth; }
            set
            {
                _maxHealth = value;
                _currentHealth = _maxHealth;
            }
        }

        private int _currentHealth;
        private int _maxHealth;
        
        public UnityAction OnDeath;
        
        public void TakeDamage(int damage)
        {
            Debug.Log($"{gameObject.name} took {damage} damage. Current health: {_currentHealth}");
            _currentHealth -= damage;
            
            _currentHealth = Mathf.Max(_currentHealth, 0);
            
            if (_currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public void HealByMaxPercentage(float percentage)
        {
            int healAmount = (int) (MaxHealth * percentage);
            _currentHealth = Math.Min(_currentHealth + healAmount, MaxHealth);
        }
        
        public void ResetHealth()
        {
            _currentHealth = MaxHealth;
        }
        
        private void HandleDeath()
        {
            OnDeath?.Invoke();
        }
    }
}
