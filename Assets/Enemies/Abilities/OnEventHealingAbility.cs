using Invader.General;
using UnityEngine;

namespace Invader.Enemies.Abilities
{
    [RequireComponent(typeof(Health))]
    public class OnEventHealingAbility : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float _healPercentage = 0.10f;
        
        private Health _health;
        
        private void Awake()
        {
            if (!TryGetComponent(out _health))
            {
                Debug.LogError("Health component not found.");
            }
        }
        
        public void Heal()
        {
            _health.HealByMaxPercentage(_healPercentage);
        }
    }
}
