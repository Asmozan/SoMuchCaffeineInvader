using System.Collections;
using Invader.General;
using Invader.Player.Projectiles;
using UnityEngine;

namespace Invader
{
    public class ExplosiveProjectile : BasicProjectile
    {
        public float ExplosionRadius
        {
            get;
            set;
        }
        
        private Coroutine _lifetimeCoroutine;
        
        private void OnEnable()
        {
            _lifetimeCoroutine = StartCoroutine(LifetimeCoroutine());
        }
        
        private void OnDisable()
        {
            if (_lifetimeCoroutine == null)
            {
                return;
            }
            
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }
        
        public void Detonate()
        {
            DealAreaDamage();
            
            ReturnToPool?.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private void DealAreaDamage()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
            foreach (var hitCollider in hitColliders)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(DamageAmount);
                }
            }
        }
        
        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(2.0f);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
