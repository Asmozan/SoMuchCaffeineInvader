using Invader.General;
using UnityEngine;

namespace Invader.Utility.Events
{
    [RequireComponent(typeof(Health))]
    public class OnDamageTakenEvent : MonoBehaviour
    {
        [SerializeField] private GameEvent _onDamageTakenEvent;
        
        private Health _health;
        
        private void Awake()
        {
            TryGetComponent(out _health);
            _health.OnDamageTaken += OnDamageTaken;
        }
        
        private void OnDamageTaken()
        {
            _onDamageTakenEvent.Raise();
        }
        
        private void OnDestroy()
        {
            _health.OnDamageTaken -= OnDamageTaken;
        }
    }
}
