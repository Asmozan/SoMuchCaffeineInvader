using UnityEngine;

namespace Invader.Player
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(PlayerWeapon))]
    [RequireComponent(typeof(PlayerAbility))]
    public class PlayerController : MonoBehaviour
    {
        public Vector2 Movement { get; set; }
        public bool IsFireButtonPressed { get; set; }
        public bool IsSpecialButtonPressed { get; set; }
        private Movement _movement;
        private void Awake()
        {
            TryGetComponent(out _movement);
        }
        
        private void Update()
        {
            _movement.Move(new Vector3(Movement.x, 0.0f, Movement.y));
        }
    }
}
