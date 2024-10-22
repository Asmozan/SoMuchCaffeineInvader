using System;
using UnityEngine;
using UnityEngine.Events;

namespace Invader.General
{
    public class Health : MonoBehaviour, IDamageable
    {
        public UnityAction OnDamageTaken;
        
        public int MaxHealth
        {
            get { return _maxHealth; }
            set
            {
                _maxHealth = value;
                _currentHealth = _maxHealth;
            }
        }

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (value >= _maxHealth)
                {
                    _currentHealth = _maxHealth;
                    return;
                }
                if (value <= 0)
                {
                    _currentHealth = 0;
                    HandleDeath();
                    return;
                }
                
                _currentHealth = value;
            }
        }
        
        private int _currentHealth;
        private int _maxHealth;
        
        public UnityAction OnDeath;
        
        public void TakeDamage(int damage)
        {
            OnDamageTaken?.Invoke();
            _currentHealth -= damage;
            
            Debug.Log($"{gameObject.name} took {damage} damage. Current health: {_currentHealth}");

            _currentHealth = Mathf.Max(_currentHealth, 0);
            
            if (_currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public void FullHeal()
        {
            Debug.Log($"{gameObject.name} has been fully healed.");
            _currentHealth = MaxHealth;
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
