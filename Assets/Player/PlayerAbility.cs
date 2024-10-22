using Invader.Utility.Events;
using UnityEngine;

namespace Invader.Player
{
    public class PlayerAbility : MonoBehaviour
    {
        [SerializeField] private ExplosiveProjectile _explosiveProjectile;
        [SerializeField] private GameEvent _onAbilityUsed;
        
        private float _cooldownTime = 10.0f;
        private float _nextUseTime = 0.0f;
        private ExplosiveProjectile _activeProjectile;
        
        public void Use()
        {
            if (_activeProjectile != null)
            {
                Detonate();
            }
            
            if (Time.time < _nextUseTime)
            {
                return;
            }
            
            if (!_activeProjectile)
            {
                Fire();
                return;
            }
        }
        
        private void Fire()
        {
            _activeProjectile = Instantiate(_explosiveProjectile, transform.position, Quaternion.identity);

            _nextUseTime = Time.time + _cooldownTime;
            
            _onAbilityUsed.Raise(_cooldownTime);
        }

        private void Detonate()
        {
            _activeProjectile.Detonate();
            _activeProjectile = null;
        }

        public void SetCooldown(float cooldown)
        {
            _cooldownTime = cooldown;
        }
        
        public void SetDamage(int damage)
        {
            _explosiveProjectile.DamageAmount = damage;
        }
        public void SetRadius(float specialAbilityRadius)
        {
            _explosiveProjectile.ExplosionRadius = specialAbilityRadius;
        }
    }
}
