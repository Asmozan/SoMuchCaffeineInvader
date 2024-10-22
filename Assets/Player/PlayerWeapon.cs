using Invader.Player.Projectiles;
using UnityEngine;

namespace Invader.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeReference] private BasicProjectile _projectile;
        [SerializeField] private float _fireRate = 0.5f;
        
        private float _nextFire = 0.0f;
        
        private ProjectileFactory _projectileFactory;
        private Transform _projectilePool;

        private void Start()
        {
            GameObject projectilePoolObject = new GameObject("ProjectilePool");
            _projectilePool = projectilePoolObject.transform;
            _projectileFactory = new ProjectileFactory(_projectilePool, _projectile, defaultCapacity: 50, maxSize: 200);
        }
        
        public void Fire()
        {
            if (Time.time < _nextFire)
            {
                return;
            }
            _projectileFactory.GetProjectile().SetExitPoint(transform.position);
            _nextFire = Time.time + _fireRate;
        }
        
        public void SetDamage(int damage)
        {
            _projectile.DamageAmount = damage;
        }
    }
}
