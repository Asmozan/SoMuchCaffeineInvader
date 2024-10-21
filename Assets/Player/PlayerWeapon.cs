using System.Collections.Generic;
using Invader.Player.Projectiles;
using Unity.VisualScripting;
using UnityEngine;

namespace Invader.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeReference] private BasicProjectile _projectile;
        [SerializeField] private float _fireRate = 0.5f;
        
        private float _nextFire = 0.0f;
        
        public void Fire()
        {
            if (Time.time < _nextFire)
            {
                return;
            }
            Instantiate(_projectile, transform.position, transform.rotation);
            _nextFire = Time.time + _fireRate;
        }
    }
}
