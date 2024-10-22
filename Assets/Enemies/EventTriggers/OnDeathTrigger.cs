using Invader.General;
using Invader.Utility.Events;
using UnityEngine;

namespace Invader.Enemies.Triggers
{
    [RequireComponent(typeof(Health))]
    public class OnDeathTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvent _onDeathEvent;
        
        private Health _health;

        private void Awake()
        {
            TryGetComponent(out _health);
            _health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            _onDeathEvent.Raise();
        }
        
        private void OnDestroy()
        {
            _health.OnDeath -= OnDeath;
        }
    }
}
