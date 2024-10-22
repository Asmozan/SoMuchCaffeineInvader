using Invader.General;
using UnityEngine;

namespace Invader.Enemies.Abilities
{
    [RequireComponent(typeof(Health))]
    public class OnEventFullHealAbility : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float _healBelowHealthPercentage = 0.5f;
        private Health _health;
        
        private void Awake()
        {
            TryGetComponent(out _health);
        }

        public void FullHeal()
        {
            Debug.Log("Full heal ability activated.");
            if (_health.CurrentHealth < _health.MaxHealth * _healBelowHealthPercentage)
            {
                _health.FullHeal();
            }
        }
    }
}
