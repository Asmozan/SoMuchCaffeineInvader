using UnityEngine;
using UnityEngine.Pool;

namespace Invader.Player.Projectiles
{
    public class ProjectileFactory
    {
        private ObjectPool<BasicProjectile> _projectilePool;
        private BasicProjectile _projectilePrefab;
        private Transform _parentTransform;
        
        public ProjectileFactory(Transform parent, BasicProjectile projectilePrefab, int defaultCapacity = 10, int maxSize = 50)
        {
            _projectilePrefab = projectilePrefab;
            _parentTransform = parent;
            
            _projectilePool = new ObjectPool<BasicProjectile>(
                createFunc: CreateProjectile,
                actionOnGet: OnGetProjectile,
                actionOnRelease: OnReleaseProjectile,
                actionOnDestroy: OnDestroyProjectile,
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );
        }

        private BasicProjectile CreateProjectile()
        {
            BasicProjectile projectile = GameObject.Instantiate(_projectilePrefab, _parentTransform);

            projectile.ReturnToPool = () => _projectilePool.Release(projectile);

            return projectile;
        }

        private void OnGetProjectile(BasicProjectile projectile)
        {
            projectile.gameObject.transform.position = _parentTransform.position;
            projectile.gameObject.SetActive(true);
        }

        private void OnReleaseProjectile(BasicProjectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(BasicProjectile projectile)
        {
            GameObject.Destroy(projectile);
        }

        public BasicProjectile GetProjectile()
        {
            return _projectilePool.Get();
        }
    }
}
